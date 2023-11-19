using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public SceneManager SceneManager;
    public SaveManager SaveManager;
    public ChangeLevelSceneData NextLevel;

    private bool IsGnomeInProximity;
    private bool IsGiantInProximity;
    private bool CollectableFound;

    public void CollectCollectable()
    {
        CollectableFound = true;
    }

    void Update()
    {
        if (IsGnomeInProximity && IsGiantInProximity)
        {
            SaveManager.SaveLevelProgress(SceneManager.GetCurrentSceneName(), CollectableFound);
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
