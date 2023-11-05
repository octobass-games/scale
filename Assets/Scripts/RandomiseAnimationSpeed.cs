using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomiseAnimationSpeed : MonoBehaviour
{
    public float MinSpeed = 0.1f;
    public float MaxSpeed = 2f;
    void Start()
    {
        this.GetComponent<Animator>().speed = Random.Range(MinSpeed, MaxSpeed);
    }

}
