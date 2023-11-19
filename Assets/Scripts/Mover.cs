using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform PositionA;
    public Transform PositionB;
    public Transform PassengerPosition;
    public Rigidbody2D Rb2d;
    public float Speed;

    private GameObject Passenger;
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
                var displacement = Vector3.MoveTowards(transform.position, TargetPositionVector, Speed * Time.fixedDeltaTime);
                var displacementa = (TargetPositionVector - transform.position).normalized * Speed * (Time.fixedDeltaTime);
                Rb2d.position = (displacement);
                Debug.Log(displacementa);
                Passenger.GetComponent<CharacterController2D>().ApplyExternalDisplacement(displacementa);
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
