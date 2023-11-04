using UnityEngine;
using UnityEngine.Events;

public class AutomaticInteractable : MonoBehaviour
{
    public UnityEvent OnInteract;
    public UnityEvent OnUninteract;

    public bool Interactable = true;

    public void Interact()
    {
        if (Interactable)
        {
            OnInteract.Invoke();
        }
    }

    public void Uninteract()
    {
        if (Interactable)
        {
            OnUninteract.Invoke();
        }
    }

    public void SetInteractable(bool interactable)
    {
        Interactable = interactable;
    }
}
