using UnityEngine;
using UnityEngine.Events;

public class EventInvoker : MonoBehaviour
{
    public UnityEvent Events;

    public void Invoke()
    {
        Events.Invoke();
    }
}
