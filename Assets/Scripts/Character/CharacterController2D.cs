using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    public float HorizontalSpeed = 1.0f;
    public LayerMask LayerMask;
    public float CoyoteTime;
    public float TimeToReachJumpApex = 0.25f;
    public float JumpHeight = 100;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public bool CanJump;

    private float HorizontalMovement;
    private bool Jumping;
    private bool IsGrounded = true;
    private Vector2 Velocity;
    private RaycastHit2D[] RaycastResults = new RaycastHit2D[10];
    private ContactFilter2D ContactFilter = new ContactFilter2D();
    private Vector3 PositionOverride = Vector2.zero;
    private float CoyoteTimer;
    private float Gravity;
    private float JumpSpeed;

    public void ForcePosition(Vector3 position)
    {
        PositionOverride = position;
    }

    void Awake()
    {
        ContactFilter.layerMask = LayerMask;
        ContactFilter.useLayerMask = true;
        CoyoteTimer = CoyoteTime;

        Gravity = 2 * JumpHeight / (Mathf.Pow(TimeToReachJumpApex, 2));
        JumpSpeed = Gravity * TimeToReachJumpApex;
    }

    void Update()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        Jumping = CanJump && (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Jump") != 0);
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
            }
        }

        Velocity -= new Vector2(0, Gravity) * Time.fixedDeltaTime;
    }

    private void UpdateAnimations()
    {
        if (Animator != null && SpriteRenderer != null) { 
            Animator.SetBool("moving", Velocity.x != 0);
            Animator.SetBool("isGrounded", IsGrounded);

            if (Velocity.x != 0 && SpriteRenderer != null)
            {
                SpriteRenderer.flipX = Velocity.x < 0;
            }
        }
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
