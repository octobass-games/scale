using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ReflectingLight : MonoBehaviour
{
    public Mirror FiredFrom;
    public Mirror FiredTo;
    void OnEnable()
    {
        FiredTo?.SetRecievingLight(true);
    }

    void OnDisable()
    {
        FiredTo?.SetRecievingLight(false);
    }

}
