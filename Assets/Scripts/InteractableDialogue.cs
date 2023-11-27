using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDialogue : MonoBehaviour
{
    public DialogueRenderer DialogueRenderer;
    public string GnomeDialogue;
    public string GiantDialogue;

    private DialogueController GnomeDialogueController;
    private DialogueController GiantDialogueController;
    private bool IsDialogueFinished;

    void Awake()
    {
        GnomeDialogueController = new DialogueController(new List<DialogueItem> { new DialogueItem("Gnome", GnomeDialogue) }, null, DialogueRenderer);
        GiantDialogueController = new DialogueController(new List<DialogueItem> { new DialogueItem("Giant", GiantDialogue) }, null, DialogueRenderer);
    }

    public bool IsNotComplete()
    {
        return !IsDialogueFinished;
    }

    public bool Speak()
    {
        if (TagComparer.IsGnome(FindObjectOfType<CharacterSwitcher>().ActiveCharacterTag))
        {
            return GnomeDialogueController.HandleProgressDialogue();
        }
        else
        {
            return GiantDialogueController.HandleProgressDialogue();
        }
    }
}
