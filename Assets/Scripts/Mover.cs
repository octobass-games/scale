using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform PositionA;
    public Transform PositionB;
    public Rigidbody2D Rb2d;
    public float Speed;
    public Collider2D SafetyChecker;

    public GameObject Passenger;
    private Vector3 PositionAVector;
    private Vector3 PositionBVector;
    private Vector3 TargetPositionVector;
    private bool IsMoving;

    private FMOD.Studio.EventInstance instance;
    public string fmodEvent;


    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<BoxCollider2D>());
    }

    public void Transport()
    {
        TargetPositionVector = TargetPositionVector == PositionAVector ? PositionBVector : PositionAVector;
        IsMoving = true;
        instance.start();
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
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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

    void OnDestroy()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.release();    
    }
}
