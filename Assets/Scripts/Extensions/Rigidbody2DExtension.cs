using UnityEngine;

public static class Rigidbody2DExtension
{
    public static int SafeMove(this Rigidbody2D source, Vector2 direction, float distance, RaycastHit2D[] RaycastResults, ContactFilter2D filter)
    {
        float displacement = Mathf.Abs(distance);
        int count = source.Cast(direction.normalized, filter, RaycastResults, displacement + 0.01f);

        for (int i = 0; i < count; i++)
        {
            RaycastHit2D hit = RaycastResults[i];
            float hitDistance = hit.distance - 0.01f;

            displacement = Mathf.Min(displacement, hitDistance);
        }

        source.position += displacement * direction.normalized;

        return count;
    }
}
