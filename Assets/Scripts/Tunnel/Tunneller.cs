using UnityEngine;

public class Tunneller : MonoBehaviour
{
    private Transform TunnelExit;

    void Update()
    {
        if (TunnelExit != null && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<CharacterController2D>().ForcePosition(TunnelExit.position);
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
