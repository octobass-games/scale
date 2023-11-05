using UnityEngine;

public class PressurePlateForcer : MonoBehaviour
{
    public PressurePlate PressurePlate;

    public void ForcePressurePlateDown()
    {
        PressurePlate.ForcePressurePlateDown();
    }
}
