using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public Transform Exit;
    public bool IsDoor;
    public Transform GiantDoorExit;
    public Transform GnomeDoorExit;

    public void Navigate(GameObject gameObject)
    {
        var characterController = gameObject.GetComponent<CharacterController2D>();

        if (IsDoor)
        {
            if (TagComparer.IsGnome(gameObject.tag))
            {
                if (GnomeDoorExit != null)
                {
                    characterController.ForcePosition(GnomeDoorExit.position);
                }
                else
                {
                    characterController.ForcePosition(Exit.position);
                }
            }
            else
            {
                if (GiantDoorExit != null)
                {
                    characterController.ForcePosition(GiantDoorExit.position);
                }
                else
                {
                    characterController.ForcePosition(Exit.position);
                }
            }
        }
        else
        {
            characterController.ForcePosition(Exit.position);
        }
    }
}
