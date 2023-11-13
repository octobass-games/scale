using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Texture2D CursorNeutralTexture;
    public Vector2 CursorNeutralHotspot = Vector2.zero;
    public Texture2D CursorPointerTexture;
    public Vector2 CursorPointerHotspot = Vector2.zero;

    void Awake() 
    {
        SetCursorNeutral();
    }
    
    public void SetCursorNeutral()
    {
        SetCursor(CursorNeutralTexture, CursorNeutralHotspot);
    }

    public void SetCursorPointer()
    {
        SetCursor(CursorPointerTexture, CursorPointerHotspot);
    }

    private void SetCursor(Texture2D texture, Vector2 hotspot) {
        UnityEngine.Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
    }
}
