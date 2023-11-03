using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;

    public void Interact()
    {
        if (OnInteract != null)
        {
            Debug.Log("Here");
            OnInteract.Invoke();
        }
    }
}
