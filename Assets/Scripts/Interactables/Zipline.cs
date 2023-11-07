using UnityEngine;

public class Zipline : MonoBehaviour
{
    public Transform ZiplineEnd;
    public float ZiplineSpeed = 5f;

    private CharacterController2D Rider;
    private bool Riding;

    public void Ride(GameObject rider)
    {
        Rider = rider.GetComponent<CharacterController2D>();
    }

    void FixedUpdate()
    {
        if (Rider != null)
        {
            if (!Riding)
            {
                Riding = true;
                LockRiderIntoZipline();
            }
            else if (!Rider.transform.position.Approximately(ZiplineEnd.position))
            {
                var newPosition = Vector3.MoveTowards(Rider.transform.position, ZiplineEnd.position, ZiplineSpeed);
                MoveRider(newPosition);
            }
            else
            {
                Riding = false;
                UnlockRiderFromZipline();
                Rider = null;
            }
        }
    }
    private void LockRiderIntoZipline()
    {
        Rider.Freeze();
        Rider.GravityModifier = 0f;
    }
    private void UnlockRiderFromZipline()
    {
        Rider.Thaw();
        Rider.GravityModifier = 1f;
    }

    private void MoveRider(Vector3 position)
    {
        Rider.ForcePosition(position);
    }
}
