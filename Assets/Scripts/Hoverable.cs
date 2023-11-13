using UnityEngine;
using UnityEngine.Events;

public class Hoverable : MonoBehaviour
{
    public bool ChangeCursor;
    public UnityEvent OnHover;
    public UnityEvent OnExit;

    void Awake()
    {
        
    }
    void OnMouseOver()
    {
        OnHover.Invoke();
    }
    void OnMouseExit()
    {
        OnExit.Invoke();
    }

}
