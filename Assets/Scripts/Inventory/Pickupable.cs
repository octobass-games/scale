using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private Inventory InventoryInProximity;

    void Update()
    {
        if (InventoryInProximity != null && Input.GetKeyDown(KeyCode.E))
        {
            InventoryInProximity.AddItem(this.gameObject);
        }   
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        InventoryInProximity = collision.GetComponent<Inventory>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        InventoryInProximity = null;
    }
}
