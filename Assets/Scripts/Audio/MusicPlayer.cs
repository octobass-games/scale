using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public string fmodEvent;

    private FMOD.Studio.EventInstance musicInstance;


    private void Awake()
    {
        if (MusicManager.instance.newTrack == true)
        {
            StopMusic();
        }
    }

    private void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);

        if(MusicManager.instance.musicPlaying == false && MusicManager.instance.newTrack == true)
        {
            musicInstance.start();
        }

        MusicManager.instance.musicPlaying = true;
    }

    private void StopMusic()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        //MusicManager.instance.musicPlaying = false;
    }
}
