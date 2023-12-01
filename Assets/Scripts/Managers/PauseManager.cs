using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject ControlsMenu;
    public GameObject ResetButton;
    public GameObject SkipButton;
    public GameObject QuitButton;
    public bool AllowSkip = true;
    public bool AllowReset = true;
    public bool IsPaused;

    private SceneManager SceneManager;

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

    public void CloseControls()
    {
        ControlsMenu.SetActive(false);
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

        if (!AllowReset)
        {
            ResetButton.SetActive(false);
        }

        if (!AllowSkip)
        {
            SkipButton.SetActive(false);
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

        var characterSwitcher = FindObjectOfType<CharacterSwitcher>();

        if (characterSwitcher != null)
        {
            characterSwitcher.SetEnableSwithing(false);
            characterSwitcher.Pause();
        }
    }

    private void Unpause()
    {
        IsPaused = false;
        CloseControls();
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;

        var characterSwitcher = FindObjectOfType<CharacterSwitcher>();
        
        if (characterSwitcher != null)
        {
            characterSwitcher.SetEnableSwithing(true);
            characterSwitcher.Unpause();
        }
    }
}
