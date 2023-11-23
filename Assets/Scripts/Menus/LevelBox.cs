using UnityEngine;

public class LevelBox : MonoBehaviour
{
    public string LevelName;
    public SaveManager SaveManager;
    public GameObject LevelCompleteTick;
    public GameObject CollectableFoundTick;

    void Start()
    {
        SaveManager = SaveManager != null ? SaveManager : FindObjectOfType<SaveManager>();
        var levelData = SaveManager.GetLevelData(LevelName);

        if (levelData.IsComplete)
        {
            LevelCompleteTick.SetActive(true);
        }

        if (levelData.CollectableFound)
        {
            CollectableFoundTick.SetActive(true);
        }
    }
}
