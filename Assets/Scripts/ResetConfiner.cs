using UnityEngine;
using Cinemachine;

public class ResetConfiner : MonoBehaviour
{
    void Start() => GetComponent<CinemachineConfiner2D>().InvalidateCache();
}