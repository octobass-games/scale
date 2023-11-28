using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDialogue : MonoBehaviour
{
    public DialogueRenderer DialogueRenderer;
    public string GnomeDialogue;
    public string GiantDialogue;
    public string ActorName;
    public string ActorDialogue;
    public bool IsActorDialogue;

    private DialogueController GnomeDialogueController;
    private DialogueController GiantDialogueController;
    private DialogueController ActorDialogueController;
    private bool IsDialogueFinished;

    void Awake()
    {
        GnomeDialogueController = new DialogueController(new List<DialogueItem> { new DialogueItem("Gnome", GnomeDialogue) }, null, DialogueRenderer);
        GiantDialogueController = new DialogueController(new List<DialogueItem> { new DialogueItem("Giant", GiantDialogue) }, null, DialogueRenderer);
        ActorDialogueController = new DialogueController(new List<DialogueItem> { new DialogueItem(ActorName, ActorDialogue) }, null, DialogueRenderer);
    }

    public bool IsNotComplete()
    {
        return !IsDialogueFinished;
    }

    public bool Speak()
    {
        if (IsActorDialogue)
        {
            return ActorDialogueController.HandleProgressDialogue();
        }
        else if (TagComparer.IsGnome(FindObjectOfType<CharacterSwitcher>().ActiveCharacterTag))
        {
            return GnomeDialogueController.HandleProgressDialogue();
        }
        else
        {
            return GiantDialogueController.HandleProgressDialogue();
        }
    }
}
