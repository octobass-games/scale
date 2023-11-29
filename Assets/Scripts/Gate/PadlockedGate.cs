using UnityEngine;
using UnityEngine.Events;

public class PadlockedGate : MonoBehaviour
{
    public GameObject PadlockKey;
    public UnityEvent OnOpen;

    private Inventory InventoryInProximity;

    void Update()
    {
        if (InventoryInProximity != null && Input.GetKeyDown(KeyCode.E))
        {
            var key = InventoryInProximity.TakeItem(PadlockKey);

            if (key != null)
            {
                Destroy(key.gameObject);
                OnOpen.Invoke();
            }
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
