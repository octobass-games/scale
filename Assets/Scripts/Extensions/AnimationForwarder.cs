using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationForwarder : MonoBehaviour
{
    public UnityEvent Event;

    public void RunEvent()
    {
        Event.Invoke();
    }
}
