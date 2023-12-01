using UnityEngine;

public class Ending : MonoBehaviour
{
    public SceneManager SceneManager;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.ChangeScene("Credits");
        }
    }
}
