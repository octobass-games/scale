using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;

    public bool musicPlaying;
    public bool stopMusic;
    public bool newTrack = true;


    public static MusicManager instance
    {
        get
        {
            if(Instance == null)
            {
                Instance = FindObjectOfType<MusicManager>();
                if(Instance == null)
                {
                    GameObject singltonGO = new GameObject("MusicManager");
                    Instance = singltonGO.AddComponent<MusicManager>();

                    DontDestroyOnLoad(singltonGO);
                }
            }

            return Instance;
        }
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }



}
