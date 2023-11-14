using UnityEngine;

public class Transporter : MonoBehaviour
{
    public Transform PositionA;
    public Transform PositionB;
    public Transform TargetPosition;
    public Rigidbody2D Rb2d;
    public float Speed;

    private GameObject Passenger;
    private bool IsMoving;

    void FixedUpdate()
    {
        if (IsMoving)
        {
            if (transform.position.Approximately(TargetPosition.position))
            {
                IsMoving = false;
            }
            else
            {
                Rb2d.MovePosition(Vector3.MoveTowards(transform.position, TargetPosition.position, Speed * Time.deltaTime));
            }
        }
    }

    public void Transport()
    {
        TargetPosition = TargetPosition == PositionA ? PositionB : PositionA;
        IsMoving = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Passenger = collision.gameObject;
        Passenger.transform.SetParent(this.transform);    
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Passenger.transform.SetParent(null);
        Passenger = null;
    }
}
