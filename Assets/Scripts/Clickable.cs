using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    public UnityEvent Event;
    void OnMouseDown()
    {
        Debug.Log("Sprite Clicked");
        Event.Invoke();
    }

}
