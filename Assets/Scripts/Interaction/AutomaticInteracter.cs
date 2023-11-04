using UnityEngine;

public class AutomaticInteracter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<AutomaticInteractable>().Interact();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<AutomaticInteractable>().Uninteract();
    }
}
