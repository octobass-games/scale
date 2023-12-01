using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public Transform Exit;
    public bool IsDoor;
    public Transform GnomeDoorExit;

    public void Navigate(GameObject gameObject)
    {
        if (IsDoor)
        {
            if (TagComparer.IsGnome(gameObject.tag) && GnomeDoorExit != null)
            {
                gameObject.GetComponent<CharacterController2D>().ForcePosition(GnomeDoorExit.position);
            }
            else
            {
                gameObject.GetComponent<CharacterController2D>().ForcePosition(Exit.position);
            }
        }
        else
        {
            gameObject.GetComponent<CharacterController2D>().ForcePosition(Exit.position);
        }
    }
}
