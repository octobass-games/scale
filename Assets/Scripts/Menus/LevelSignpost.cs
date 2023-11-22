using System.Collections;
using UnityEngine;

public class LevelSignpost : MonoBehaviour
{
    public string LevelNamePrefix;
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
