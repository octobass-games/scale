using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    

    private static MusicManager Instance;

    public bool musicPlaying;
    public bool newTrack = true;

    private FMOD.Studio.EventInstance musicInstance;



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

    public void MusicStarter()
    {

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(FindObjectOfType<MusicPlayer>().CheckFmodEvent());

        if (musicPlaying == false && newTrack == true)
        {
            musicInstance.start();
        }

        musicPlaying = true;
    }

    public void StopMusic()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        newTrack = true;
        musicPlaying = false;
    }


}
