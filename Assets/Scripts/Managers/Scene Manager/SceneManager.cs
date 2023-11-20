using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private ChangeLevelSceneData ChangeLevelSceneData;

    public void ReloadScene()
    {
        ChangeScene(GetCurrentSceneName());
    }

    public string GetCurrentSceneName()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ChangeLevelScene(ChangeLevelSceneData changeLevelSceneData)
    {
        ChangeLevelSceneData = changeLevelSceneData;

        ChangeScene(ChangeLevelSceneData.LevelName);
    }

    void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (ChangeLevelSceneData != null)
        {
            if (ChangeLevelSceneData.PositionGiant)
            {
                var giant = GameObject.FindGameObjectWithTag("Giant");
                giant.GetComponent<CharacterController2D>().ForcePosition(ChangeLevelSceneData.GiantPosition);
            }

            if (ChangeLevelSceneData.PositionGnome)
            {
                var gnome = GameObject.FindGameObjectWithTag("Gnome");
                gnome.GetComponent<CharacterController2D>().ForcePosition(ChangeLevelSceneData.GnomePosition);
            }

            ChangeLevelSceneData = null;
        }
    }
}
