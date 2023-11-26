using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDialogue : MonoBehaviour
{
    public string Speaker;
    public string Dialogue;
    public DialogueRenderer DialogueRenderer;

    private DialogueController DialogueController;
    private bool IsDialogueFinished;

    void Awake()
    {
        DialogueController = new DialogueController(new List<DialogueItem> { new DialogueItem(Speaker, Dialogue) }, null, DialogueRenderer);
    }

    public bool IsNotComplete()
    {
        return !IsDialogueFinished;
    }

    public bool Speak()
    {
        return DialogueController.HandleProgressDialogue();
    }
}
