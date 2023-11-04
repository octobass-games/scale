using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunEventOnAwake : MonoBehaviour
{
    public UnityEvent EventOnAwake;

    void Awake()
    {
        if (EventOnAwake != null)
        {
            EventOnAwake.Invoke();
        }
    }

}
