using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MirrorManager : MonoBehaviour
{
    public Mirror[] Mirrors;

    public void ApplyLights()
    {
        foreach (var mirror in Mirrors)
        {
            mirror.ApplyLights();
        }
    }

}
