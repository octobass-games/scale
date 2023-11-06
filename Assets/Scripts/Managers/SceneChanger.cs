using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public ChangeLevelSceneData ChangeLevelInfo;

    public void ChangeScene()
    {
        GameManager.Instance.ChangeLevel(ChangeLevelInfo);
    }
}
