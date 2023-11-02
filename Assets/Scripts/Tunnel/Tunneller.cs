using UnityEngine;

public class Tunneller : MonoBehaviour
{
    private Transform TunnelExit;

    void Update()
    {
        if (TunnelExit != null && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<CharacterController>().ForcePosition(TunnelExit.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var tunnelEntrance = collider.GetComponent<TunnelEntrance>();

        if (tunnelEntrance != null)
        {
            TunnelExit = tunnelEntrance.GetExit();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        TunnelExit = null;
    }
}
