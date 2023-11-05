using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Sprite LeftSprite;
    public Sprite RightSprite;
    public UnityEvent OnLeft;
    public UnityEvent OnRight;

    private bool isLeft = true;

    public bool IsGiantLever;
    private bool interactable;
    private string currentTag = null;
    private DialogueRenderer dialogueRenderer;
    private DialogueController GnomeDialogueController;
    private DialogueController GiantDialogueController;
    private CharacterSwitcher CharacterSwitcher;

    void Awake()
    {
        dialogueRenderer = FindObjectOfType<DialogueRenderer>();
        CharacterSwitcher = FindObjectOfType<CharacterSwitcher>();
        DialogueItem gnome = new DialogueItem("Gnome", "Too Heavy");
        DialogueItem giant = new DialogueItem("Giant", "Too Small");

        GnomeDialogueController = new DialogueController(new System.Collections.Generic.List<DialogueItem> { { gnome } }, null, dialogueRenderer);
        GiantDialogueController = new DialogueController(new System.Collections.Generic.List<DialogueItem> { { giant } }, null, dialogueRenderer);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        currentTag = null;

        if (TagComparer.IsPlayer(col) && col.CompareTag(CharacterSwitcher.ActiveCharacterTag))
        {
            currentTag = col.tag;

            if (IsGiantLever)
            {
                interactable = TagComparer.IsGiant(col);
            }
            else
            {
                interactable = TagComparer.IsGnome(col);
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        interactable = false;
        currentTag = null;
        GnomeDialogueController.Reset();
        GiantDialogueController.Reset();
    }

    public void PullLeverRight()
    {
        if (interactable)
        {
            OnRight.Invoke();
            SpriteRenderer.sprite = RightSprite;
        }
    }

    public void PullLeverLeft()
    {
        if (interactable)
        {
            OnLeft.Invoke();
            SpriteRenderer.sprite = LeftSprite;
        }

    }


    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)))
        {
            if (interactable)
            {
                ToggleLever();
            }
            else if (currentTag != null)
            {
                ShowNoUseDialogue();
            }
        }
    }

    private void ShowNoUseDialogue()
    {
        if (TagComparer.IsGnome(currentTag))
        {
            ShowGnomeNoUseDialogue();
        }else if (TagComparer.IsGiant(currentTag))
        {
            ShowGiantNoUseDialogue();
        }

    }

    private void ShowGnomeNoUseDialogue()
    {
       GnomeDialogueController.HandleProgressDialogue();
    }

    private void ShowGiantNoUseDialogue()
    {
        GiantDialogueController.HandleProgressDialogue();
    }


    public void ToggleLever()
    {
        if (isLeft)
        {
            PullLeverRight();
        }
        else
        {
            PullLeverLeft();

        }

        isLeft = !isLeft;
    }
}
