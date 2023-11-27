using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSwappersReadyCondition : Condition
{
    public PositionSwapper PositionSwapper;

    public override bool Evaluate(GameObject interacter)
    {
        return PositionSwapper.IsReadyToSwap();
    }
}
