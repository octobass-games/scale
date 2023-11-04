using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;

    private bool IsPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!IsPaused)
            {
                IsPaused = true;
                PauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                IsPaused = false;
                PauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
