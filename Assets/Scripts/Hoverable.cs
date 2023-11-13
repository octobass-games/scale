using UnityEngine;
using UnityEngine.Events;

public class Hoverable : MonoBehaviour
{
    public UnityEvent OnHover;
    public UnityEvent OnExit;

    void OnMouseEnter()
    {
        FindObjectOfType<Cursor>().SetCursorPointer(); 
        OnHover.Invoke();
    }

    void OnMouseExit()
    {
        FindObjectOfType<Cursor>().SetCursorNeutral(); 
        OnExit.Invoke();
    }

}
