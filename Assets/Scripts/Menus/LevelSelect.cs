using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public SaveManager SaveManager;
    public List<GameObject> Levels;

    void Start()
    {
        SaveData saveData = SaveManager.Load();
        List<LevelData> levelsData = saveData.LevelData;

        LevelData nextLevel = levelsData.Find(level => !level.IsComplete);

        for (int i = 0; i < Levels.Count; i++)
        {
            GameObject level = Levels[i];

            LevelData levelData = levelsData.Find(ld => ld.Name == level.name);
            
            if (levelData != null && levelData.IsComplete || levelData == nextLevel)
            {
                level.SetActive(true);
            }
        }
    }
}
