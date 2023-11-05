using UnityEngine;
using UnityEngine.Events;

public class ActionSwitch : MonoBehaviour
{
    public Switch Switch;
    public SwitchTagChecker SwitchTagChecker;
    public UnityEvent OnInvalidUser;

    private GameObject ActorInActionProximity;

    void Update()
    {
        if (ActorInActionProximity != null && Input.GetKeyDown(KeyCode.E))
        {
            if (SwitchTagChecker != null && !SwitchTagChecker.IsValidUser(ActorInActionProximity.tag))
            {
                OnInvalidUser.Invoke();
            }
            else
            {
                Switch.ToggleState();
            }
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
