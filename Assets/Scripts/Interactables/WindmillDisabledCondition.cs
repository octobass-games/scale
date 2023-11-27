using UnityEngine;

public class WindmillDisabledCondition : Condition
{
    public Windmill Windmill;

    public override bool Evaluate(GameObject interacter)
    {
        return !Windmill.On;
    }
}
