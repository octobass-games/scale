using UnityEngine;

public static class Vector3Extension
{
    public static bool Approximately(this Vector3 source, Vector3 other)
    {
        return Mathf.Approximately(source.x, other.x) && Mathf.Approximately(source.y, other.y) && Mathf.Approximately(source.z, other.z);
    }
}
