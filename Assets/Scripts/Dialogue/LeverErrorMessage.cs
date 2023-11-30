using System.Collections.Generic;
using UnityEngine;

public class LeverErrorMessage : MonoBehaviour
{
    private DialogueRenderer DialogueRenderer;
    private DialogueController GnomeDialogueController;
    private DialogueController GiantDialogueController;

    void Awake()
    {
        DialogueRenderer = FindObjectOfType<DialogueRenderer>();
        DialogueItem gnome = new DialogueItem("Gnome", "I can't move this, I'll do my back in!");
        DialogueItem giant = new DialogueItem("Giant", "I better not touch this... I wouldn't want to break it");

        GnomeDialogueController = new DialogueController(new List<DialogueItem> { { gnome } }, null, DialogueRenderer);
        GiantDialogueController = new DialogueController(new List<DialogueItem> { { giant } }, null, DialogueRenderer);
    }

    public void ShowNoUseDialogue(string tag)
    {
        if (TagComparer.IsGnome(tag))
        {
            ShowGnomeNoUseDialogue();
        }
        else if (TagComparer.IsGiant(tag))
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
}
