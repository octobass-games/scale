using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    public float HorizontalSpeed = 1.0f;

    private float HorizontalDelta;
    private Vector3 PositionOverride = Vector2.zero;

    void Update()
    {
        HorizontalDelta = Input.GetAxisRaw("Horizontal") * HorizontalSpeed;
    }

    void FixedUpdate()
    {
        Vector2 displacement = GetDisplacement();

        Rb2d.MovePosition(Rb2d.position + displacement);

        HorizontalDelta = 0;
    }

    public void ForcePosition(Vector3 position)
    {
        PositionOverride = position;
    }

    private Vector2 GetDisplacement()
    {
        if (PositionOverride == Vector3.zero)
        {
            return new Vector2(HorizontalDelta * Time.fixedDeltaTime, 0);
        }
        else
        {
            var displacement = PositionOverride;

            PositionOverride = Vector2.zero;
            
            return displacement;
        }
    }
}
