using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject Item;

    public GameObject GetItem()
    {
        return Item;
    }

    public void SetItem(GameObject item)
    {
        item.GetComponent<Pickupable>().enabled = false;
        item.GetComponent<SpriteRenderer>().enabled = false;
        item.GetComponent<BoxCollider2D>().enabled = false;

        Item = item;
    }

    public bool Contains(GameObject item)
    {
        return Item == item;
    }

    public GameObject RemoveItem(GameObject itemToRemove)
    {
        if (Item == itemToRemove)
        {
            Item = null;

            return itemToRemove;
        }

        return null;
    }
}
