using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
public class Collectable : MonoBehaviour
{
    private LevelExit LevelExit;
    public GameObject Canvas;
    public CollectableScriptable Scriptable;
    public Image CollectableImage;
    public Image CollectableOutline;
    public Button Button;
    public SpriteRenderer CollectableRenderer;
    public SpriteRenderer IconRenderer;

    void Start()
    {
        LevelExit = FindObjectOfType<LevelExit>();

        if (LevelExit.CollectableFound)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            
            var color = spriteRenderer.color;

            spriteRenderer.color = new Color(color.r, color.g, color.b, 0.5f);
        }
    }

    public void Collect()
    {
        CollectableRenderer.enabled = false;
        IconRenderer.enabled = false;
        CollectableImage.sprite = Scriptable.ItemBig;
        CollectableOutline.sprite = Scriptable.ItemBigOutline;
        Canvas.SetActive(true);
        LevelExit.CollectCollectable();

        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
