using UnityEngine;

public class Zipline : MonoBehaviour
{
    public Transform ZiplineEnd;

    private Zipliner Zipliner;
    private bool Riding;

    public void Ride(Zipliner zipliner)
    {
        Zipliner = zipliner;
    }

    void FixedUpdate()
    {
        if (Zipliner != null)
        {
            if (!Riding)
            {
                Riding = true;
                Zipliner.LockIntoZipline();
            }
            else if (!Mathf.Approximately(Zipliner.transform.position.x, ZiplineEnd.position.x) && !Mathf.Approximately(Zipliner.transform.position.y, ZiplineEnd.position.y))
            {
                var newPosition = Vector3.MoveTowards(Zipliner.transform.position, ZiplineEnd.position, 5f);
                Zipliner.Move(newPosition);
            }
            else
            {
                Riding = false;
                Zipliner.UnlockFromZipline();
                Zipliner = null;
            }
        }
    }
}
