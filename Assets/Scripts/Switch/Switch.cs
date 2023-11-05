using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public SpriteRenderer SwitchSpriteRenderer;
    public Sprite FirstSwitchStateSprite;
    public Sprite SecondSwitchStateSprite;
    public UnityEvent OnFirstSwitchState;
    public UnityEvent OnSecondSwitchState;
    public bool IsToggleable = true;

    private bool IsInFirstState = true;
    private bool HasBeenSwitched = false;
    public bool IsBroken = false;

    public void Break()
    {
        IsBroken = true;
    }

    public void ToggleState()
    {
        if ((IsToggleable || !HasBeenSwitched) && !IsBroken)
        {
            if (IsInFirstState)
            {
                EnterSecondState();
            }
            else
            {
                EnterFirstState();
            }
        }

        HasBeenSwitched = true;
    }

    private void EnterFirstState()
    {
        IsInFirstState = true;
        SwitchSpriteRenderer.sprite = FirstSwitchStateSprite;
        OnFirstSwitchState.Invoke();
    }

    private void EnterSecondState()
    {
        IsInFirstState = false;
        SwitchSpriteRenderer.sprite = SecondSwitchStateSprite;
        OnSecondSwitchState.Invoke();
    }
}
