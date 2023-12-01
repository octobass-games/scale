using UnityEngine;
using Cinemachine;

public class ResetConfiner : MonoBehaviour
{
    void Start()
    {
        var confiner = GetComponent<CinemachineConfiner2D>();

        if (confiner != null )
        {
            confiner.InvalidateCache();
        }
    }
}