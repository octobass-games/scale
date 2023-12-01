using UnityEngine;

public class Credits : MonoBehaviour
{
    public SceneManager SceneManager;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.ChangeScene("MainMenu");
        }
    }
}
