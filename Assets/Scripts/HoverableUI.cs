using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnHover;
    public UnityEvent OnExit;
    private Cursor Cursor;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        GetCursor()?.SetCursorPointer();
        OnHover.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetCursor();
        OnExit.Invoke();
    }

    public void ResetCursor()
    {
        GetCursor()?.SetCursorNeutral();
    }

    private Cursor GetCursor()
    {
        if (Cursor == null)
        {
            Cursor = FindObjectOfType<Cursor>();
            return Cursor;
        }
        else
        {
            return Cursor;
        }
    }
}
