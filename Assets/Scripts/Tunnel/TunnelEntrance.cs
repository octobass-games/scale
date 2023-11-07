using UnityEngine;

public class TunnelEntrance : MonoBehaviour
{
    public Transform Exit;

    public void Navigate(GameObject gameObject)
    {
        gameObject.GetComponent<CharacterController2D>().ForcePosition(Exit.position);
    }
}
