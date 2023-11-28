using FMODUnity;
using UnityEngine;

public class Zipline : MonoBehaviour
{
    public Transform ZiplineLeftEnd;
    public Transform ZiplineRightEnd;
    public Transform ZiplineLine;
    public float ZiplineSpeed = 5f;
    public bool IsHorizontal = true;
    public StudioEventEmitter SoundEmitter;

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
        SetYAttachmentPoint();
        SoundEmitter.Play();
    }

    private void UnlockRiderFromZipline()
    {
        Riding = false;
        RiderCharacterController.Thaw();
        RiderCharacterController.GravityModifier = 1f;
        RiderAnimator.SetBool("isZipping", false);
        RiderCharacterController = null;
        SoundEmitter.Stop();
    }

    private void MoveRider()
    {
        RiderCharacterController.ForcePosition(Vector3.MoveTowards(RiderCharacterController.transform.position, TargetEnd.position, ZiplineSpeed));
    }

    private void SetYAttachmentPoint()
    {
        if (IsHorizontal)
        {
            RiderCharacterController.ForcePosition(new Vector3(RiderCharacterController.transform.position.x, ZiplineLine.position.y, RiderCharacterController.transform.position.z));
        }
        else
        {
            var rb2d = RiderCharacterController.GetComponent<BoxCollider2D>();

            Collider2D[] results = new Collider2D[10];

            var contactFilter = new ContactFilter2D();
            contactFilter.useTriggers = true;

            int count = rb2d.OverlapCollider(contactFilter, results);

            for (int i = 0; i < count; i++)
            {
                if (results[i].name == "Line")
                {
                    var distance = rb2d.Distance(results[i]);

                    var position = RiderCharacterController.GetComponent<Rigidbody2D>().position;
                    var separation = distance.normal * distance.distance;

                    if (distance.normal.x < 0)
                    {
                        var newPosition = new Vector3(position.x, position.y) + new Vector3(separation.x, separation.y - 35);
                        RiderCharacterController.ForcePosition(newPosition);
                    }
                    else
                    {
                        var newPosition = new Vector3(position.x, position.y) + new Vector3(separation.x, separation.y + 5);
                        RiderCharacterController.ForcePosition(newPosition);
                    }
                }
            }
        }
    }
}
