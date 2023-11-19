using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public SceneManager SceneManager;
    public SaveManager SaveManager;
    public ChangeLevelSceneData NextLevel;
    public bool CollectableFound;

    private string SceneName;
    private bool IsGnomeInProximity;
    private bool IsGiantInProximity;

    public void CollectCollectable()
    {
        CollectableFound = true;
    }

    void Awake()
    {
        SceneName = SceneManager.GetCurrentSceneName();

        var levelData = SaveManager.GetLevelData(SceneName);

        CollectableFound = levelData.CollectableFound;
    }

    void Update()
    {
        if (IsGnomeInProximity && IsGiantInProximity)
        {
            SaveManager.SaveLevelProgress(SceneName, CollectableFound);
            SceneManager.ChangeLevelScene(NextLevel);
        }    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Giant")
        {
            IsGiantInProximity = true;
        }
        else if (collision.gameObject.tag == "Gnome")
        {
            IsGnomeInProximity = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Giant")
        {
            IsGiantInProximity = false;
        }
        else if (collision.gameObject.tag == "Gnome")
        {
            IsGnomeInProximity = false;
        }
    }
}
