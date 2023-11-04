using UnityEngine;

public class Zipliner : MonoBehaviour
{
    public CharacterController2D CharacterController;

    private Zipline Zipeline;

    public void LockIntoZipline()
    {
        CharacterController.Freeze();
        CharacterController.GravityModifier = 0f;
    }

    public void UnlockFromZipline()
    {
        CharacterController.Thaw();
        CharacterController.GravityModifier = 1f;
    }

    public void Move(Vector3 position)
    {
        CharacterController.ForcePosition(position);
    }

    void Update()
    {
        if (Zipeline != null && Input.GetKeyDown(KeyCode.E))
        {
            Zipeline.Ride(this);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Zipeline = collider.GetComponent<Zipline>();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Zipeline = null;
    }
}
