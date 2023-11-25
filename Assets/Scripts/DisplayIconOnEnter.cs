using UnityEngine;

public class DisplayIconOnEnter : MonoBehaviour
{
    public SpriteRenderer Icon;
    public SpriteRenderer InteractingSprite;
    public Material Lit;
    public Material Unlit;
    public bool IsProximityBased = true;

    public void Show()
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

    public void Hide()
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


    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsProximityBased && TagComparer.IsPlayer(collision.tag))
        {
            Show();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (IsProximityBased && TagComparer.IsPlayer(collision.tag))
        {
            Hide();
        }
    }
}
