using System.Collections.Generic;
using UnityEngine;

public class ProximitySwitch : MonoBehaviour
{
    public Switch Switch;
    public List<string> ValidUserTags = new List<string>();

    private List<GameObject> ActorsInProximity = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsValidUser(collision.gameObject.tag))
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
        if (IsValidUser(collision.gameObject.tag))
        {
            ActorsInProximity.Remove(collision.gameObject);

            if (ActorsInProximity.Count == 0)
            {
                Switch.ToggleState();
            }
        }
    }

    private bool IsValidUser(string userTag)
    {
        return ValidUserTags.Count == 0 || ValidUserTags.Contains(userTag);
    }
}
