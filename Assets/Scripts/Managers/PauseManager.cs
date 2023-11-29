using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject ControlsMenu;
    public GameObject QuitButton;

    private SceneManager SceneManager;
    private bool IsPaused;

    public void Close()
    {
        Unpause();
    }

    public void Reset()
    {
        Time.timeScale = 1.0f;
        SceneManager.ReloadScene();
    }

    public void SkipLevel()
    {
        Time.timeScale = 1.0f;
        FindObjectOfType<LevelExit>().MoveToNextLevel();
    }

    public void Controls()
    {
        ControlsMenu.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.ChangeScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Awake()
    {
        SceneManager = FindObjectOfType<SceneManager>();

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            QuitButton.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!IsPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    private void Pause()
    {
        IsPaused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        FindObjectOfType<CharacterSwitcher>()?.SetEnableSwithing(false);
    }

    private void Unpause()
    {
        IsPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        FindObjectOfType<CharacterSwitcher>()?.SetEnableSwithing(true);
    }
}
