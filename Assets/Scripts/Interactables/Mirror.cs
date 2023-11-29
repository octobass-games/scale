using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Mirror : MonoBehaviour
{
    public SpriteRenderer MirrorSprite;
    public ReflectingLight[] Lights;

    public bool On = true;
    public bool RecievingLight = true;


    private MirrorManager manager;
    void Start()
    {
        manager = FindObjectOfType<MirrorManager>();
        manager.ApplyLights();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableMirror()
    {
        On = true;
        manager.ApplyLights();
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/reflector enable");
    }
    public void DisableMirror()
    {
        On = false;
        manager.ApplyLights();
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/reflector disable");
    }

    private void UpdateMirrorSprite()
    {
        if (On)
        {
            MirrorSprite.color = Color.white;
        }else
        {
            MirrorSprite.color = Color.black;
        }
    }


    public void ApplyLights()
    {
        UpdateMirrorSprite();
        bool foundMirror = false;
        bool SendLightToOtherMirrors = On && RecievingLight;

        foreach (var light in Lights)
        {
            Debug.Log(light.name);
            Debug.Log(light.FiredTo?.On);
            if (!foundMirror && light.FiredTo?.On == true)
            {
                light.gameObject.SetActive(SendLightToOtherMirrors);
                light.FiredTo?.SetRecievingLight(SendLightToOtherMirrors);
                foundMirror = true;
            }else
            {
                light.FiredTo?.SetRecievingLight(false);
                light.gameObject.SetActive(false);
            }
        }

        if (foundMirror == false && Lights.Length > 0) {
            Lights[Lights.Length - 1].gameObject.SetActive(SendLightToOtherMirrors);
            Lights[Lights.Length - 1].FiredTo?.SetRecievingLight(SendLightToOtherMirrors);
        }
    }

    public void SetRecievingLight(bool light)
    {
        RecievingLight = light;
        ApplyLights();
    }
}
