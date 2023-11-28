using UnityEngine;

public static class Rigidbody2DExtension
{
    public static int SafeMove(this Rigidbody2D source, Vector2 direction, float distance, RaycastHit2D[] RaycastResults, ContactFilter2D filter, float stepHeight)
    {
        float displacement = Mathf.Abs(distance);
        int count = source.Cast(direction.normalized, filter, RaycastResults, displacement + 0.01f);
        bool stepping = false;

        for (int i = 0; i < count; i++)
        {
            RaycastHit2D hit = RaycastResults[i];
            float hitDistance = hit.distance - 0.01f;

            if (hit.normal.x != 0 && hit.collider.bounds.size.y <= stepHeight)
            {
                var mover = hit.collider.gameObject.GetComponentInParent<Mover>();

                if (mover == null || !mover.IsMoving)
                {
                    Vector3 closestPosition = hit.collider.bounds.ClosestPoint(hit.point + Vector2.up);

                    source.position = closestPosition + new Vector3(0, source.GetComponent<Collider2D>().bounds.size.y / 2, 0);
        
                    stepping = true;
                }
            }
            else
            {
                displacement = Mathf.Min(displacement, hitDistance);
            }
        }

        if (!stepping)
        {
            source.position += displacement * direction.normalized;
        }

        return count;
    }
}
