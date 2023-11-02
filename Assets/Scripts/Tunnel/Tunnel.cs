using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public TunnelEntrance FirstEntrance;
    public TunnelEntrance SecondEntrance;

    public TunnelEntrance GetExit(TunnelEntrance teleporterEntrance)
    {
        return teleporterEntrance == FirstEntrance ? SecondEntrance : FirstEntrance;
    }
}
