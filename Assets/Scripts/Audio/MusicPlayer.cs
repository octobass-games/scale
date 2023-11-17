using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public string fmodEvent;

    public string CheckFmodEvent()
    {
        return fmodEvent;
    }

    void Start()
    {
        if (fmodEvent != MusicManager.instance.fmodEventChecker)
        {
            MusicManager.instance.newTrack = true;
            MusicManager.instance.StopMusic();
            MusicManager.instance.MusicStarter();
        }

        else if (fmodEvent == MusicManager.instance.fmodEventChecker)
        {
            Debug.Log("Same Music");
        }
    } 
}
