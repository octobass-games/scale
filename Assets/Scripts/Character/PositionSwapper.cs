using UnityEngine;

public class PositionSwapper : MonoBehaviour
{
    public GameObject Gnome;
    public GameObject Giant;
    public LayerMask LayerMask;

    private bool Swap;
    private ContactFilter2D ContactFilter = new ContactFilter2D();
    private RaycastHit2D[] Hits = new RaycastHit2D[1];

    void Start()
    {
        ContactFilter.layerMask = LayerMask;
        ContactFilter.useLayerMask = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            Swap = true;
        }
    }

    void FixedUpdate()
    {
        if (Swap)
        {
            var gnomeCurrentPosition = Gnome.transform.position;

            var gnomeCollider  = Gnome.GetComponent<Collider2D>();
            var gnomeExtents = gnomeCollider.bounds.extents;
            var gnomeFeetPosition = Gnome.transform.position.y - gnomeCollider.bounds.extents.y;

            var giantCollider = Giant.GetComponent<Collider2D>();
            var giantExtents = giantCollider.bounds.extents;
            var giantFeetPosition = Giant.transform.position.y - giantCollider.bounds.extents.y;

            float giantXOffset;

            if (IsGnomeTouchingLeftWall())
            {
                giantXOffset = giantExtents.x;
            }
            else if (IsGnomeTouchingRightWall())
            {
                giantXOffset = -giantExtents.x;
            }
            else
            {
                giantXOffset = 0;
            }

            Gnome.GetComponent<CharacterController2D>().ForcePosition(new Vector3(Giant.transform.position.x, giantFeetPosition + gnomeExtents.y, 0));
            Giant.GetComponent<CharacterController2D>().ForcePosition(new Vector3(gnomeCurrentPosition.x + giantXOffset, gnomeFeetPosition + giantExtents.y, 0));

            Swap = false;
        }
    }

    private bool IsGnomeTouchingRightWall()
    {
        return IsGnomeTouchingWall(Vector2.right);
    }

    private bool IsGnomeTouchingLeftWall()
    {
        return IsGnomeTouchingWall(Vector2.left);
    }

    private bool IsGnomeTouchingWall(Vector2 direction)
    {
        return Gnome.GetComponent<Rigidbody2D>().Cast(direction, ContactFilter, Hits, 0.5f) > 0;
    }
}
