using System.Collections.Generic;
using UnityEngine;

public class ErrorDialogue : MonoBehaviour
{
    private DialogueRenderer DialogueRenderer;
    private DialogueController GnomeDialogueController;
    private DialogueController GiantDialogueController;
    private DialogueController DefaultDialogueController;
    public List<DialogueItem> GnomeError;
    public List<DialogueItem> GiantError;
    public List<DialogueItem> DefaultError;
    void Awake()
    {
        DialogueRenderer = FindObjectOfType<DialogueRenderer>();

        GnomeDialogueController = new DialogueController(GnomeError, null, DialogueRenderer);
        GiantDialogueController = new DialogueController(GiantError, null, DialogueRenderer);
        DefaultDialogueController = new DialogueController(DefaultError, null, DialogueRenderer);
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

    public void ShowNoUseDialogue()
    {
        DefaultDialogueController.HandleProgressDialogue();

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
