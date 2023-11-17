using UnityEngine;

public class CharacterCondition : Condition
{
    public GameObject RequiredCharacter;

    public override bool Evaluate(GameObject interacter)
    {
        return interacter == RequiredCharacter;
    }
}
