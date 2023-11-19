using UnityEngine;

public class Zipline : MonoBehaviour
{
    public Transform ZiplineLeftEnd;
    public Transform ZiplineRightEnd;
    public Transform ZiplineLine;
    public float ZiplineSpeed = 5f;

    private Transform TargetEnd;
    private CharacterController2D RiderCharacterController;
    private Animator RiderAnimator;
    private bool Riding;

    public void Ride(GameObject rider)
    {
        RiderCharacterController = rider.GetComponent<CharacterController2D>();
        RiderAnimator = rider.GetComponentInChildren<Animator>();

        TargetEnd = RiderCharacterController.IsTravellingRight() ? ZiplineRightEnd : ZiplineLeftEnd;
    }

    void FixedUpdate()
    {
        if (RiderCharacterController != null)
        {
            if (!Riding)
            {
                LockRiderIntoZipline();
            }
            else if (!RiderCharacterController.transform.position.Approximately(TargetEnd.position))
            {
                MoveRider();
            }
            else
            {
                UnlockRiderFromZipline();
            }
        }
    }

    private void LockRiderIntoZipline()
    {
        Riding = true;
        RiderAnimator.SetBool("isZipping", true);
        RiderCharacterController.Freeze();
        RiderCharacterController.GravityModifier = 0f;
        RiderCharacterController.ForcePosition(new Vector3(RiderCharacterController.transform.position.x, ZiplineLine.position.y, RiderCharacterController.transform.position.z));
    }

    private void UnlockRiderFromZipline()
    {
        Riding = false;
        RiderCharacterController.Thaw();
        RiderCharacterController.GravityModifier = 1f;
        RiderAnimator.SetBool("isZipping", false);
        RiderCharacterController = null;
    }

    private void MoveRider()
    {
        RiderCharacterController.ForcePosition(Vector3.MoveTowards(RiderCharacterController.transform.position, TargetEnd.position, ZiplineSpeed));
    }
}
