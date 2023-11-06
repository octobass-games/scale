using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public SaveManager SaveManager;

    void Start()
    {
        if (!SaveManager.HasSaveData())
        {
            this.gameObject.SetActive(false);
        }
    }
}
