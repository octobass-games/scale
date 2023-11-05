using UnityEngine;

public class SwitchForcer : MonoBehaviour
{
    public Switch Switch;

    public void ForceSecondSwitchState()
    {
        Switch.ForceSecondSwitchState();
        Switch.Break();
    }
}
