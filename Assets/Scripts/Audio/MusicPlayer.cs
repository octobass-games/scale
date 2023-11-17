using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public string fmodEvent;

    public string CheckFmodEvent()
    {
        return fmodEvent;
    }


    private void Start()
    {
        MusicManager.instance.newTrack = true;
        MusicManager.instance.MusicStarter();
    } 
}
