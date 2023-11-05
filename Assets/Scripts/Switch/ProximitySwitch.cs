using System.Collections.Generic;
using UnityEngine;

public class ProximitySwitch : MonoBehaviour
{
    public Switch Switch;

    private List<GameObject> ActorsInProximity = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (ActorsInProximity.Count == 0)
        {
            Switch.ToggleState();
        }

        ActorsInProximity.Add(collision.gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        ActorsInProximity.Remove(collision.gameObject);

        if (ActorsInProximity.Count == 0)
        {
            Switch.ToggleState();    
        }
    }
}
