using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class AutoInteracter : MonoBehaviour
{
    private Interactable Interactable;
    void Awake()
    {
        Interactable = GetComponent<Interactable>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hi");
       if (TagComparer.IsPlayer(collision))
        {
            Interactable.Interact();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Interactable = null;   
    }
}
