using UnityEngine;

public class Interacter : MonoBehaviour
{
    private Interactable Interactable;
    
    void Update()
    {
        if (Interactable != null && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)))
        {
            Interactable.Interact();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable = collision.gameObject.GetComponent<Interactable>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Interactable = null;   
    }
}
