using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private ChangeLevelInfo ChangeLevelInfo;
    
    public void ChangeLevel(ChangeLevelInfo changeLevelInfo)
    {
        ChangeLevelInfo = changeLevelInfo;

        SceneManager.LoadScene(ChangeLevelInfo.LevelName);
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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (ChangeLevelInfo != null)
        {
            ChangeLevelInfo = null;
        }
    }
}
