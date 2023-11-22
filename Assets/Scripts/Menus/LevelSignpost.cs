using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSignpost : MonoBehaviour
{
    public LevelBoxes LevelBoxes;
  
    public void Pick()
    {
        StartCoroutine(LoadBoxes());
    }


    private IEnumerator LoadBoxes()
    {
        yield return new WaitForSeconds(0.5f);
        LevelBoxes.gameObject.SetActive(true);  
        LevelBoxes.LoadBoxes();

    }
}
