using UnityEngine;
using UnityEngine.SceneManagement;

// DEPRECATED
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private ChangeLevelSceneData ChangeLevelInfo;
    
    public void ChangeLevel(ChangeLevelSceneData changeLevelInfo)
    {
        ChangeLevelInfo = changeLevelInfo;

        UnityEngine.SceneManagement.SceneManager.LoadScene(ChangeLevelInfo.LevelName);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (ChangeLevelInfo != null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                player.GetComponent<CharacterController2D>().ForcePosition(ChangeLevelInfo.GnomePosition);
            }

            ChangeLevelInfo = null;
        }
    }
}
