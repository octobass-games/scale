using UnityEngine;

public class CollectableBackgroundItem : MonoBehaviour
{
    public CollectableScriptable collectable;
    public SpriteRenderer Outline;
    public SpriteRenderer Main;
    void Start()
    {
        Outline.sprite = collectable.ItemBigOutline;
        Main.sprite = collectable.ItemBig;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
