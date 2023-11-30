using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour
{
    public float Delay = 0f;
    public UnityEvent Event;
     
    void Start()
    {
        StartCoroutine(RunEvent());
    }

    IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(Delay);


        Event.Invoke();
    }

}
