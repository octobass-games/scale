using UnityEngine;

public class TeleporterEntrance : MonoBehaviour
{
    public Transform ExitPoint;

    void OnTriggerStay2D(Collider2D collider)
    {
        gameObject.GetComponentInParent<Teleporter>().Teleport(this, collider);
    }
}
