using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ErrorDialogue : MonoBehaviour
{
    private DialogueRenderer DialogueRenderer;
    private DialogueController GnomeDialogueController;
    private DialogueController GiantDialogueController;
    private DialogueController DefaultDialogueController;
    public List<DialogueItem> GnomeError;
    public List<DialogueItem> GiantError;
    public List<DialogueItem> DefaultError;

    public UnityEvent OnDialogueEnd;

    void Awake()
    {
        DialogueRenderer = FindObjectOfType<DialogueRenderer>();

        GnomeDialogueController = new DialogueController(GnomeError, OnDialogueEnd, DialogueRenderer);
        GiantDialogueController = new DialogueController(GiantError, OnDialogueEnd, DialogueRenderer);
        DefaultDialogueController = new DialogueController(DefaultError, OnDialogueEnd, DialogueRenderer);
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
