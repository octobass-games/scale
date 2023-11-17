using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool newTrack;
    public string fmodEventChecker;

    private bool musicPlaying;
    private FMOD.Studio.EventInstance musicInstance;

    private static MusicManager Instance;

    public static MusicManager instance
    {
        get
        {
            if(Instance == null)
            {
                Instance = FindObjectOfType<MusicManager>();

                if(Instance == null)
                {
                    GameObject singletonGO = new GameObject("MusicManager");

                    Instance = singletonGO.AddComponent<MusicManager>();

                    DontDestroyOnLoad(singletonGO);
                }
            }

            return Instance;
        }
    }

    public void MusicStarter()
    {
        fmodEventChecker = FindObjectOfType<MusicPlayer>().CheckFmodEvent();

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEventChecker);

        if (!musicPlaying && newTrack)
        {
            musicInstance.start();
        }

        musicPlaying = true;
    }

    public void StopMusic()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        musicPlaying = false;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
