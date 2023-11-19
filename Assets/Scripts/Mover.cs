using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform PositionA;
    public Transform PositionB;
    public Rigidbody2D Rb2d;
    public float Speed;

    public GameObject Passenger;
    private Vector3 PositionAVector;
    private Vector3 PositionBVector;
    private Vector3 TargetPositionVector;
    private bool IsMoving;

    public void Transport()
    {
        TargetPositionVector = TargetPositionVector == PositionAVector ? PositionBVector : PositionAVector;
        IsMoving = true;
    }

    public void StopTransport()
    {
        IsMoving = false;
    }

    void Awake()
    {
        PositionAVector = PositionA.position;
        PositionBVector = PositionB.position;
        TargetPositionVector = PositionAVector;
    }

    void FixedUpdate()
    {
        if (IsMoving)
        {
            if (transform.position.Approximately(TargetPositionVector))
            {
                IsMoving = false;
            }
            else
            {
                var nextPosition = Vector3.MoveTowards(transform.position, TargetPositionVector, Speed * Time.fixedDeltaTime);
                Rb2d.position = nextPosition;
                
                var displacement = (TargetPositionVector - transform.position).normalized * Speed * (Time.fixedDeltaTime);

                if (Passenger != null && displacement.y < 0 || (displacement.y == 0 && displacement.x != 0))
                {
                    Passenger.GetComponent<CharacterController2D>().ApplyExternalDisplacement(displacement);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            Passenger = collision.gameObject;
        }    
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            Passenger = null;
        }
    }
}
