using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnHover;
    public UnityEvent OnExit;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<Cursor>().SetCursorPointer();
        OnHover.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetCursor();
        OnExit.Invoke();
    }

    public void ResetCursor()
    {
        FindObjectOfType<Cursor>().SetCursorNeutral();
    }
}
