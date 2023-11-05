using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Sprite PressurePlateUpSprite;
    public Sprite PressurePlateDownSprite;
    public UnityEvent OnPressurePlateDown;
    public UnityEvent OnPressurePlateUp;

    private bool IsBroken;

    public void BreakPressurePlate()
    {
        IsBroken = true;
    }

    public void ForcePressurePlateDown()
    {
        PressurePlateDown();
        BreakPressurePlate();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PressurePlateDown();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        PressurePlateUp();
    }

    private void PressurePlateDown()
    {
        if (!IsBroken)
        {
            SpriteRenderer.sprite = PressurePlateDownSprite;
            OnPressurePlateDown.Invoke();
        }
    }

    private void PressurePlateUp()
    {
        if (!IsBroken)
        {
            SpriteRenderer.sprite = PressurePlateUpSprite;
            OnPressurePlateUp.Invoke();
        }
    }
}
