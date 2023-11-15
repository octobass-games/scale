using UnityEngine;

public class Transporter : MonoBehaviour
{
    public Transform PositionA;
    public Transform PositionAExit;
    public Transform PositionB;
    public Transform PositionBExit;
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

    public void TogglePassenger(GameObject passenger)
    {
        if (!IsMoving)
        {
            if (Passenger == null)
            {
                PickUpPassenger(passenger);
            }
            else if (passenger == Passenger)
            {
                DropOffPassenger();
            }
        }
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
            if (Passenger != null && Passenger.transform.parent != transform)
            {
                Passenger.transform.SetParent(transform);
            }
            else if (transform.position.Approximately(TargetPositionVector))
            {
                IsMoving = false;
            }
            else
            {
                Rb2d.MovePosition(Vector3.MoveTowards(transform.position, TargetPositionVector, Speed * Time.fixedDeltaTime));
            }
        }
    }

    private void PickUpPassenger(GameObject passenger)
    {
        if (Passenger == null)
        {
            Passenger = passenger;

            var characterController = Passenger.GetComponent<CharacterController2D>();
            characterController.ForcePosition(PassengerPosition.position);
        }
    }

    private void DropOffPassenger()
    {
        var exit = TargetPositionVector == PositionAVector ? PositionAExit : PositionBExit;

        var characterController = Passenger.GetComponent<CharacterController2D>();
        characterController.ForcePosition(exit.position);

        Passenger.transform.SetParent(null);

        Passenger = null;
    }
}
