using UnityEngine;

public class ActionSwitch : MonoBehaviour
{
    public Switch Switch;

    private GameObject ActorInActionProximity;

    void Update()
    {
        if (ActorInActionProximity != null && Input.GetKeyDown(KeyCode.E))
        {
            Switch.ToggleState();
        }    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ActorInActionProximity = collision.gameObject;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        ActorInActionProximity = null;
    }
}
