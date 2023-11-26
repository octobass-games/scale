using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private SubLevel SubLevel;

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


    public void ChangeLevelScene(SubLevel subLevel)
    {
        SubLevel = subLevel;

        ChangeScene(subLevel.Scene);
    }

    void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (SubLevel != null)
        {
            if (SubLevel.PositionGiant)
            {
                var giant = GameObject.FindGameObjectWithTag("Giant");
                giant.GetComponent<CharacterController2D>().ForcePosition(SubLevel.GiantPosition);
            }

            if (SubLevel.PositionGnome)
            {
                var gnome = GameObject.FindGameObjectWithTag("Gnome");
                gnome.GetComponent<CharacterController2D>().ForcePosition(SubLevel.GnomePosition);
            }

            SubLevel = null;
        }
    }
}
