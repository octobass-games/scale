using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SaveManager SaveManager;
    public SceneManager SceneManager;
    public GameObject ContinueButton;

    public void NewGame()
    {
        SaveManager.DeleteSaveData();
        SceneManager.ChangeScene("Level1Village");
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        if (!SaveManager.HasSaveData())
        {
            ContinueButton.SetActive(false);
        }    
    }
}
