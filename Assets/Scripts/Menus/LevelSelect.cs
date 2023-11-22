using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public SaveManager SaveManager;
    public List<LevelSignpost> Signposts;

    void Start()
    {
        SaveData saveData = SaveManager.Load();
        List<LevelData> levelsData = saveData.LevelData;

        LevelData nextLevel = levelsData.Find(level => !level.IsComplete);

        for (int i = 0; i < Signposts.Count; i++)
        {
            LevelSignpost signpost = Signposts[i];

            bool isSublevelComplete = levelsData.Find(ld => ld.Name.StartsWith(signpost.LevelNamePrefix) && ld.IsComplete) != null;
            bool isNextLevel = nextLevel.Name.StartsWith(signpost.LevelNamePrefix);
            
            if (isSublevelComplete || isNextLevel)
            {
                signpost.gameObject.SetActive(true);
            }
        }
    }
}
