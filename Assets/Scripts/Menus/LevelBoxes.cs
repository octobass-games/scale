using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBoxes : MonoBehaviour
{
    public SaveManager SaveManager;
    public List<LevelBox> boxes = new List<LevelBox>();
    private int index = 0;
    private bool ShowNext = false;
    public Clickable NextButton;
    public GameObject CluesHeading;
    public List<GameObject> Clues;
    public List<GameObject> ClueTicks;

    public void LoadBoxes()
    {
        StartCoroutine(LoadBox());
    }

    public void LoadBoxes(bool showNext, Action OnNext)
    {
        this.ShowNext = showNext;
        NextButton.Event.RemoveAllListeners();
        NextButton.Event.AddListener(() => OnNext.Invoke());
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
            boxes[index].IsHoverable = !ShowNext;
            boxes[index].gameObject.SetActive(true);

            yield return new WaitForSeconds(0.25f);

            index += 1;
            if (index < boxes.Count)
            {
                StartCoroutine(LoadBox());    
            }
            else
            {
                if (ShowNext)
                {
                    NextButton.gameObject.SetActive(true);
                }
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

    void Start()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            var levelName = boxes[i].LevelName;
            var levelData = SaveManager.GetLevelData(levelName);

            if (levelData.Clue != "")
            {
                var clue = Clues.Find(clue => !clue.activeSelf);

                if (clue != null)
                {
                    CluesHeading.SetActive(true);
                    clue.gameObject.SetActive(true);
                }

                if (levelData.ClueFound)
                {
                    var clueTick = ClueTicks.Find(clueTick => !clueTick.activeSelf);

                    if (clueTick != null)
                    {
                        clueTick.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
