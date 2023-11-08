using UnityEngine;

public class PositionSwapper : MonoBehaviour
{
    public GameObject Gnome;
    public GameObject Giant;
    public LayerMask LayerMask;

    private bool Swap;
    private ContactFilter2D ContactFilter = new ContactFilter2D();

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
            var gnomeFeetPosition = Gnome.transform.position.y - gnomeCollider.bounds.extents.y;

            var giantCollider = Giant.GetComponent<Collider2D>();
            var giantFeetPosition = Giant.transform.position.y - giantCollider.bounds.extents.y;

            float giantXOffset;

            if (IsGnomeTouchingLeftWall())
            {
                giantXOffset = giantCollider.bounds.extents.x;
            }
            else if (IsGnomeTouchingRightWall())
            {
                giantXOffset = -giantCollider.bounds.extents.x;
            }
            else
            {
                giantXOffset = 0;
            }

            Gnome.transform.position = new Vector3(Giant.transform.position.x, giantFeetPosition + gnomeCollider.bounds.extents.y, 0);
            Giant.transform.position = new Vector3(gnomeCurrentPosition.x + giantXOffset, gnomeFeetPosition + giantCollider.bounds.extents.y, 0);

            Swap = false;
        }
    }

    private bool IsGnomeTouchingRightWall()
    {
        RaycastHit2D[] results = new RaycastHit2D[10];

        int resultCount = Gnome.GetComponent<Rigidbody2D>().Cast(Vector2.right, ContactFilter, results, 0.5f);

        return resultCount > 0;
    }

    private bool IsGnomeTouchingLeftWall()
    {
        RaycastHit2D[] results = new RaycastHit2D[10];

        int resultCount = Gnome.GetComponent<Rigidbody2D>().Cast(Vector2.left, ContactFilter, results, 0.5f);

        return resultCount > 0;
    }
}
