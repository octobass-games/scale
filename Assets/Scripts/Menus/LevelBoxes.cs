using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoxes : MonoBehaviour
{
    public List<LevelBox> boxes = new List<LevelBox>();
    private int index = 0;

    public void LoadBoxes()
    {
        StartCoroutine(LoadBox());
    }

    private IEnumerator LoadBox()
    {
        boxes[index].gameObject.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        index += 1;
        if (index < boxes.Count)
        {
            StartCoroutine(LoadBox());    
        }else
        {
            index = 0;
        }
    }

    public void OnBack()
    {
        StopCoroutine(LoadBox());
        boxes.ForEach(box  => box.gameObject.SetActive(false));
        index = 0;
        gameObject.SetActive(false);
    }
}
