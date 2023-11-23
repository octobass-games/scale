using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoxes : MonoBehaviour
{
    public SaveManager SaveManager;
    public List<LevelBox> boxes = new List<LevelBox>();
    private int index = 0;

    void Start()
    {
    }

    public void LoadBoxes()
    {
        StartCoroutine(LoadBox());
    }

    private IEnumerator LoadBox()
    {
        SaveManager = SaveManager != null ? SaveManager : FindObjectOfType<SaveManager>();

        SaveData saveData = SaveManager.Load();
        List<LevelData> levelsData = saveData.LevelData;

        LevelData nextLevel = levelsData.Find(level => !level.IsComplete);

        var levelData = SaveManager.GetLevelData(boxes[index].LevelName);

        if (levelData.IsComplete || nextLevel.Name == boxes[index].LevelName)
        {
            boxes[index].gameObject.SetActive(true);

            yield return new WaitForSeconds(0.25f);

            index += 1;
            if (index < boxes.Count)
            {
                StartCoroutine(LoadBox());    
            }
            else
            {
                index = 0;
            }
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
