using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public SubLevel CurrentLevel;
    public bool CollectableFound;
    public bool ClueFound;

    private SceneManager SceneManager;
    private SaveManager SaveManager;
    private string SceneName;
    private bool IsGnomeInProximity;
    private bool IsGiantInProximity;
    public SpriteRenderer GiantMarker;
    public SpriteRenderer GnomeMarker;

    public void CollectCollectable()
    {
        CollectableFound = true;
    }

    public void CollectClue()
    {
        ClueFound = true;
    }

    public void MoveToNextLevel()
    {
        SaveManager.SaveLevelProgress(SceneName, CollectableFound, ClueFound);
        SceneManager.ChangeLevelScene(CurrentLevel.NextLevel);
    }

    void Awake()
    {
        SceneManager = FindObjectOfType<SceneManager>();
        SaveManager = FindObjectOfType<SaveManager>();

        SceneName = SceneManager.GetCurrentSceneName();

        var levelData = SaveManager.GetLevelData(SceneName);
        if (levelData != null )
        {
            CollectableFound = levelData.CollectableFound;
            ClueFound = levelData.ClueFound;
        }

    }

    void Update()
    {
        if (IsGnomeInProximity && IsGiantInProximity)
        {
            MoveToNextLevel();
        }

        GnomeMarker.gameObject.SetActive(IsGnomeInProximity);
        GiantMarker.gameObject.SetActive(IsGiantInProximity);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagComparer.IsGiant(collision))
        {
            IsGiantInProximity = true;
        }
        else if (TagComparer.IsGnome(collision))
        {
            IsGnomeInProximity = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (TagComparer.IsGiant(collision))
        {
            IsGiantInProximity = false;
        }
        else if (TagComparer.IsGnome(collision))
        {
            IsGnomeInProximity = false;
        }
    }
}
