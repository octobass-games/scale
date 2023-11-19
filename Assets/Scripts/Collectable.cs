using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Collectable : MonoBehaviour
{
    private LevelExit LevelExit;

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
        LevelExit.CollectCollectable();
    }
}
