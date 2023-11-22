using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public ChangeLevelSceneData NextLevel;
    public bool CollectableFound;

    private SceneManager SceneManager;
    private SaveManager SaveManager;
    private string SceneName;
    private bool IsGnomeInProximity;
    private bool IsGiantInProximity;

    public void CollectCollectable()
    {
        CollectableFound = true;
    }

    public void MoveToNextLevel()
    {
        SaveManager.SaveLevelProgress(SceneName, CollectableFound);
        SceneManager.ChangeLevelScene(NextLevel);
    }

    void Awake()
    {
        SceneManager = FindObjectOfType<SceneManager>();
        SaveManager = FindObjectOfType<SaveManager>();

        SceneName = SceneManager.GetCurrentSceneName();

        var levelData = SaveManager.GetLevelData(SceneName);

        CollectableFound = levelData.CollectableFound;
    }

    void Update()
    {
        if (IsGnomeInProximity && IsGiantInProximity)
        {
            MoveToNextLevel();
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
