using UnityEngine;

public class InventoryCondition : Condition
{
    public GameObject RequiredItem;

    public override bool Evaluate(GameObject interacter)
    {
        return interacter.GetComponent<Inventory>()?.Contains(RequiredItem) == true;
    }
}
