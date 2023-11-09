using UnityEngine;

public class DisplayIconOnEnter : MonoBehaviour
{
    public SpriteRenderer Icon;
    public SpriteRenderer InteractingSprite;
    public Material Lit;
    public Material Unlit;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            if (Icon != null)
            {
                Icon.gameObject.SetActive(true);
            }

            if (InteractingSprite != null)
            {
                InteractingSprite.material = Unlit;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            if (Icon != null)
            {
                Icon.gameObject.SetActive(false);
            }

            if (InteractingSprite != null)
            {
                InteractingSprite.material = Lit;
            }
        }
    }
}
