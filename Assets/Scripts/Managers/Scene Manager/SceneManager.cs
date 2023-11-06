using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private ChangeLevelInfo ChangeLevelInfo;

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ChangeLevelScene(ChangeLevelInfo changeLevelInfo)
    {
        ChangeLevelInfo = changeLevelInfo;

        ChangeScene(ChangeLevelInfo.LevelName);
    }

    void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (ChangeLevelInfo != null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                player.GetComponent<CharacterController2D>().ForcePosition(ChangeLevelInfo.PositionToSpawn);
            }

            ChangeLevelInfo = null;
        }
    }
}
