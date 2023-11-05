using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    public List<Lock> Locks;
    public UnityEvent OnOpen;
    public UnityEvent OnClose;

    public void CheckLocks()
    {
        if (IsUnlocked())
        {
            OnOpen.Invoke();
        }
        else
        {
            OnClose.Invoke();
        }
    }

    public void ForceOpen()
    {
        OnOpen.Invoke();
    }

    private bool IsUnlocked()
    {
        for (int i = 0; i < Locks.Count; i++)
        {
            if (Locks[i].IsLocked)
            {
                return false;
            }
        }

        return true;
    }
}
