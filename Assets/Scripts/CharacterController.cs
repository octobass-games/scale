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
        if (PositionOverride != Vector3.zero)
        {
            Rb2d.MovePosition(Rb2d.position + new Vector2(PositionOverride.x, PositionOverride.y));
            PositionOverride = Vector2.zero;
        }
        else
        {
            Rb2d.MovePosition(Rb2d.position + new Vector2(HorizontalDelta * Time.fixedDeltaTime, 0));
        }

        HorizontalDelta = 0;
    }

    public void ForcePosition(Vector3 position)
    {
        PositionOverride = position;
    }
}
