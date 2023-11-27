using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimationExtender : MonoBehaviour
{

    public void SetBoolToTrue(string boolName)
    {
        GetComponent<Animator>().SetBool(boolName, true);
    }

    public void SetBoolToFalse(string boolName)
    {
        GetComponent<Animator>().SetBool(boolName, false);
    }
}
