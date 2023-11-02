using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    public float HorizontalSpeed = 1.0f;
    public LayerMask LayerMask;

    private float HorizontalDirection;
    private float HorizontalDelta;
    private Vector3 PositionOverride = Vector2.zero;

    private float horizontalAlter;
    private float verticalAlter;
    private bool Jumping;
    private float verticalSpeed;
    private bool IsGrounded;

    void Update()
    {
        HorizontalDirection = Input.GetAxisRaw("Horizontal");
        HorizontalDelta = HorizontalDirection * HorizontalSpeed;
        Jumping = Input.GetAxisRaw("Vertical") != 0;
    }

    void FixedUpdate()
    {
        Vector2 displacement = GetDisplacement();

        float gravityImpact;

        if (Jumping && IsGrounded)
        {
            Jumping = false;

            verticalSpeed = 0.25f - 1 * Time.fixedDeltaTime;
        }
        else
        {
            verticalSpeed = verticalSpeed - 1 * Time.fixedDeltaTime;
        }

        GetHorizontalCollisions();
        GetVerticalCollisions(verticalSpeed);

        Debug.Log(verticalSpeed + verticalAlter);

        Rb2d.MovePosition(Rb2d.position + displacement + new Vector2(horizontalAlter, verticalSpeed + verticalAlter));

        HorizontalDelta = 0;
        horizontalAlter = 0;
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

    private void GetHorizontalCollisions()
    {
        var collider = GetComponent<BoxCollider2D>();
        var size = collider.size;

        var hit = Physics2D.Raycast(transform.position, new Vector2(HorizontalDirection, 0), size.x / 2 + 0.5f, LayerMask);

        if (hit.collider != null)
        {
            var a = (size.x / 2 + 0.5f) - hit.distance;
            horizontalAlter = HorizontalDirection == -1 ? a : -a;
        }
    }

    private void GetVerticalCollisions(float verticalTravel)
    {
        var collider = GetComponent<BoxCollider2D>();
        var size = collider.size;

        var hit = Physics2D.Raycast(transform.position, new Vector2(0, Mathf.Sign(verticalTravel)), size.y / 2 + Mathf.Abs(verticalTravel) + 0.5f, LayerMask);

        if (hit.collider != null)
        {
            var a = (size.y / 2 + 0.5f) - hit.distance;
            verticalAlter = a;
            verticalSpeed = 0;
            IsGrounded = true;
        }
    }
}
