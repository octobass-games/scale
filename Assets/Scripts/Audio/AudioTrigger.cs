using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    public string fmodEvent;

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
    }

    public void FmodOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(fmodEvent);
    }

    public void PlayFmodEvent()
    {
        instance.start();
    }

    public void StopFmodEvent()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        // instance.release();
    }
}
