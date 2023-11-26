using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public void PickUp(GameObject interacter)
    {
        interacter.GetComponent<Inventory>().SetItem(gameObject);
    }
}
