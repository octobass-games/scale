using UnityEngine;

public class LevelBox : MonoBehaviour
{
    public string LevelName;
    public SaveManager SaveManager;
    public GameObject LevelCompleteTick;
    public GameObject CollectableFoundTick;
    public bool IsHoverable;

    void Start()
    {
        if (!IsHoverable)
        {
            Destroy(this.GetComponent<Hoverable>());
        }
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
