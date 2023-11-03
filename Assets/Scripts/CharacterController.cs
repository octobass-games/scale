using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    public float HorizontalSpeed = 1.0f;
    public LayerMask LayerMask;

    private float HorizontalMovement;
    private bool Jumping;
    private bool IsGrounded;
    private Vector2 Velocity;
    private RaycastHit2D[] RaycastResults = new RaycastHit2D[10];
    private ContactFilter2D ContactFilter = new ContactFilter2D();
    private Vector3 PositionOverride = Vector2.zero;

    public void ForcePosition(Vector3 position)
    {
        PositionOverride = position;
    }

    void Awake()
    {
        ContactFilter.layerMask = LayerMask;
    }

    void Update()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        Jumping = Input.GetAxisRaw("Vertical") != 0;
    }

    void FixedUpdate()
    {
        UpdateVelocity();
        UpdatePosition();
        ResetVelocity();
    }

    private void UpdateVelocity()
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
    }

    private void UpdatePosition()
    {
        if (PositionOverride != Vector3.zero)
        {
            Rb2d.position = PositionOverride;
            PositionOverride = Vector3.zero;
        }
        else
        {
            var displacement = Velocity * Time.fixedDeltaTime;
            
            Rb2d.SafeMove(Vector2.right * displacement.x, displacement.x, RaycastResults, ContactFilter);
            int RaycastResultCount = Rb2d.SafeMove(Vector2.up * displacement.y, displacement.y, RaycastResults, ContactFilter);

            for (int i = 0; i < RaycastResultCount; i++)
            {
                if (Mathf.Approximately(RaycastResults[i].normal.y, 1))
                {
                    IsGrounded = true;
                    Velocity.y = 0;
                }
            }
        }
    }

    private void ResetVelocity()
    {
        Velocity.x = 0;
    }
}
