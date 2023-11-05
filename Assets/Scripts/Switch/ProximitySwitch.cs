using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProximitySwitch : MonoBehaviour
{
    public Switch Switch;
    public SwitchTagChecker SwitchTagChecker;

    private List<GameObject> ActorsInProximity = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (SwitchTagChecker == null || SwitchTagChecker.IsValidUser(collision.gameObject.tag))
        {
            if (ActorsInProximity.Count == 0)
            {
                Switch.ToggleState();
            }

            ActorsInProximity.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (SwitchTagChecker == null || SwitchTagChecker.IsValidUser(collision.gameObject.tag))
        {
            ActorsInProximity.Remove(collision.gameObject);

            if (ActorsInProximity.Count == 0)
            {
                Switch.ToggleState();
            }
        }
    }
}
