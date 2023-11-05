using UnityEngine;

public class Lock : MonoBehaviour
{
    public Gate Gate;
    public bool IsLocked = true;

    public void Open()
    {
        IsLocked = false;
        Gate.TryToOpen();
    }

    public void Close()
    {
        IsLocked = true;
        Gate.TryToOpen();
    }
}
