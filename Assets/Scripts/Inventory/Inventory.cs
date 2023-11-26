using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        item.GetComponent<Pickupable>().enabled = false;
        item.GetComponent<SpriteRenderer>().enabled = false;
        item.GetComponent<BoxCollider2D>().enabled = false;

        Items.Add(item);
    }

    public bool Contains(GameObject item)
    {
        return Items.Contains(item);
    }

    public GameObject RemoveItem(GameObject itemToRemove)
    {
        GameObject item = Items.Find(item => item == itemToRemove);

        if (item != null)
        {
            Items.Remove(item);
        }

        return item;
    }
}
