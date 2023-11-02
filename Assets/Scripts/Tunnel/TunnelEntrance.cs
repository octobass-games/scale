using UnityEngine;

public class TunnelEntrance : MonoBehaviour
{
    public Transform GetExit()
    {
        return gameObject.GetComponentInParent<Tunnel>().GetExit(this).transform;
    }
}
