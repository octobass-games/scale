using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    public float HorizontalSpeed = 1.0f;
    public LayerMask LayerMask;

    private float HorizontalMovement;
    private float HorizontalDelta;
    private Vector3 PositionOverride = Vector2.zero;

    private float horizontalAlter;
    private float verticalAlter;
    private bool Jumping;
    private float verticalSpeed;
    private bool IsGrounded;
    private Vector2 Velocity;

    void Update()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        Jumping = Input.GetAxisRaw("Vertical") != 0;
    }

    void FixedUpdate()
    {
        Velocity.x = HorizontalMovement * HorizontalSpeed;

        if (Jumping && IsGrounded)
        {
            Jumping = false;
            IsGrounded = false;
            Velocity.y = 5f;
        }

        if (Velocity.y < 0)
        {
            Velocity *= new Vector2(1, 1.05f);
        }

        Velocity += Physics2D.gravity * Time.fixedDeltaTime;

        var displacement = Velocity * Time.fixedDeltaTime;
        RaycastHit2D[] RaycastResults = new RaycastHit2D[15];
        var contactFilter = new ContactFilter2D();
        contactFilter.layerMask = LayerMask;
        Rb2d.SafeMove(Vector2.right * displacement.x, displacement.x, RaycastResults, contactFilter);
        int RaycastResultCount = Rb2d.SafeMove(Vector2.up * displacement.y, displacement.y, RaycastResults, contactFilter);

        for ( int i = 0; i < RaycastResultCount; i++ )
        {
            if (Mathf.Approximately(RaycastResults[i].normal.y, 1))
            {
                IsGrounded = true;
                Velocity.y = 0;
            }
        }

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

        var hit = Physics2D.Raycast(transform.position, new Vector2(HorizontalMovement, 0), size.x / 2 + 0.5f, LayerMask);

        if (hit.collider != null)
        {
            var a = (size.x / 2 + 0.5f) - hit.distance;
            horizontalAlter = HorizontalMovement == -1 ? a : -a;
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
