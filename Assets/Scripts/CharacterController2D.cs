using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    public float HorizontalSpeed = 1.0f;
    public LayerMask LayerMask;
    public float CoyoteTime;
    public float JumpSpeed = 5.0f;
    public Animator Animator;

    private float HorizontalMovement;
    private bool Jumping;
    private bool IsGrounded;
    private Vector2 Velocity;
    private RaycastHit2D[] RaycastResults = new RaycastHit2D[10];
    private ContactFilter2D ContactFilter = new ContactFilter2D();
    private Vector3 PositionOverride = Vector2.zero;
    private float CoyoteTimer;

    public void ForcePosition(Vector3 position)
    {
        PositionOverride = position;
    }

    void Awake()
    {
        ContactFilter.layerMask = LayerMask;
        CoyoteTimer = CoyoteTime;
    }

    void Update()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        Jumping = Input.GetAxisRaw("Vertical") != 0;
    }

    void FixedUpdate()
    {
        DecrementCoyoteTimer();
        UpdateVelocity();
        UpdateAnimations();
        UpdatePosition();
        ResetVelocity();
    }

    private void DecrementCoyoteTimer()
    {
        CoyoteTimer -= Time.fixedDeltaTime;
    }

    private void UpdateVelocity()
    {
        Velocity.x = HorizontalMovement * HorizontalSpeed;

        if (Jumping)
        {
            if (IsGrounded || CoyoteTimer >= 0)
            {
                Jumping = false;
                IsGrounded = false;
                Velocity.y = JumpSpeed;
                CoyoteTimer = -1;
                Animator.SetTrigger("jump");
            }
        }

        if (Velocity.y < 0)
        {
            Velocity *= new Vector2(1, 1.05f);
        }

        Velocity += Physics2D.gravity * Time.fixedDeltaTime;
    }

    private void UpdateAnimations()
    {
        Animator.SetBool("moving", Velocity.x != 0);
        Animator.SetBool("left", Velocity.x < 0);
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
                    CoyoteTimer = CoyoteTime;
                    Velocity.y = 0;
                    Animator.SetTrigger("land");
                }
            }

            if (RaycastResultCount == 0)
            {
                IsGrounded = false;
            }
        }
    }

    private void ResetVelocity()
    {
        Velocity.x = 0;
    }
}
