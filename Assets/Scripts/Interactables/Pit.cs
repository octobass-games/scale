using UnityEngine;

public class Pit : MonoBehaviour
{
    public Transform RespawnPoint;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            collision.gameObject.GetComponent<CharacterController2D>().ForcePosition(RespawnPoint.position);

        }
    }
}
