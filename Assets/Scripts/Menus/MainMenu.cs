using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SaveManager SaveManager;
    public SceneManager SceneManager;
    public GameObject ContinueButton;
    public Animator BackgroundAnimator;

    public void NewGame()
    {
        SaveManager.DeleteSaveData();
        SceneManager.ChangeScene("GnomeHouse");
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        SaveData saveData = SaveManager.Load();

        if (saveData != null)
        {
            var lastLevelName = saveData.LevelData.FindLast(level => level.IsComplete)?.Name;

            if (lastLevelName?.StartsWith("Level3") == true)
            {
                BackgroundAnimator.SetBool("friends", true);
            }
            else if (lastLevelName == "Level5-4")
            {
                BackgroundAnimator.SetBool("everyone", true);
            }
        }
        else
        {
            ContinueButton.SetActive(false);
        }
    }
}
