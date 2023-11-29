using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform PositionA;
    public Transform PositionB;
    public Rigidbody2D Rb2d;
    public float Speed;
    public Collider2D SafetyChecker;
    public StudioEventEmitter SoundEmitter;
    public bool IsMoving;
    public Collider2D Base;

    public GameObject Passenger;
    private Vector3 PositionAVector;
    private Vector3 PositionBVector;
    private Vector3 TargetPositionVector;
    private Collider2D[] BaseCollisions = new Collider2D[5];

    public void UpdatePositionA(Transform transform)
    {
        var previousPositionAVector = PositionAVector;
        
        PositionAVector = transform.position;

        if (TargetPositionVector.Approximately(previousPositionAVector))
        {
            TargetPositionVector = PositionAVector;    
        }   
    }

    public void Transport()
    {
        TargetPositionVector = TargetPositionVector == PositionAVector ? PositionBVector : PositionAVector;
        IsMoving = true;
        SoundEmitter.Play();
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
                SoundEmitter.Stop();
            }
            else
            {
                var results = new Collider2D[10];

                var count = SafetyChecker.OverlapCollider(new ContactFilter2D(), results);

                bool isPlayerSafe = true;

                for (int i = 0; i < count; i++)
                {
                    if (TagComparer.IsPlayer(results[i].tag))
                    {
                        isPlayerSafe = false;
                    }
                }

                if (isPlayerSafe)
                {
                    var nextPosition = Vector3.MoveTowards(transform.position, TargetPositionVector, Speed * Time.fixedDeltaTime);
                    Rb2d.position = nextPosition;
                
                    var displacement = (TargetPositionVector - transform.position).normalized * Speed * (Time.fixedDeltaTime);

                    if (Passenger != null && displacement.y < 0 || (displacement.y == 0 && displacement.x != 0))
                    {
                        Passenger.GetComponent<CharacterController2D>().ApplyExternalDisplacement(displacement);
                    }
                }
                else
                {
                    var baseCollisionCount = Base.OverlapCollider(new ContactFilter2D(), BaseCollisions);

                    for (int i = 0; i < baseCollisionCount; i++)
                    {
                        if (TagComparer.IsPlayer(BaseCollisions[i].tag))
                        {
                            BaseCollisions[i].GetComponent<CharacterController2D>().ForcePosition(transform.position);
                        }
                    }
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
