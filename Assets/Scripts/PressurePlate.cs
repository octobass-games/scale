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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsBroken)
        {
            SpriteRenderer.sprite = PressurePlateDownSprite;
            OnPressurePlateDown.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsBroken)
        {
            SpriteRenderer.sprite = PressurePlateUpSprite;
            OnPressurePlateUp.Invoke();
        }
    }
}
