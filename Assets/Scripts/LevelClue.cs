using UnityEngine;

public class LevelClue : MonoBehaviour
{
    public Clue Clue;
    public LevelExit LevelExit;

    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Clue.Sprite;

        LevelExit = FindObjectOfType<LevelExit>();

        if (LevelExit.ClueFound)
        {
            var color = spriteRenderer.color;

            spriteRenderer.color = new Color(color.r, color.g, color.b, 0.5f);
        }
    }

    public void Collect()
    {
        var levelExit = FindObjectOfType<LevelExit>();

        levelExit.CollectClue();
    }
}
