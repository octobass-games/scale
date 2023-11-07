using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public Transform Exit;

    public void Navigate(GameObject gameObject)
    {
        gameObject.GetComponent<CharacterController2D>().ForcePosition(Exit.position);
    }
}
