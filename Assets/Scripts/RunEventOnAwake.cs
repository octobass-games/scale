using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunEventOnAwake : MonoBehaviour
{
    public UnityEvent EventOnAwake;
    public bool runOnEnable;
    public UnityEvent EventOnDisable;

    void Awake()
    {
        if (EventOnAwake != null)
        {
            EventOnAwake.Invoke();
        }
    }


    void OnEnable()
    {
        if (runOnEnable)
        {
            if (EventOnAwake != null)
            {
                EventOnAwake.Invoke();
            }
        }
    }

    void OnDisable()
    {
        if (EventOnDisable != null)
        {
            EventOnDisable.Invoke();
        }
    }
}
