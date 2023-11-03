using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public ChangeLevelInfo ChangeLevelInfo;

    public void ChangeScene()
    {
        GameManager.Instance.ChangeLevel(ChangeLevelInfo);
    }
}
