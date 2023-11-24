using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController
{
    public List<DialogueItem> texts;
    private int pos = 0;
    public UnityEvent OnEnd;
    public DialogueRenderer DialogueRenderer;
    private bool IsRendererNotRevealingPreviousLine;

    public DialogueController(List<DialogueItem> texts, UnityEvent onEnd, DialogueRenderer dialogueRenderer)
    {
        this.texts = texts;
        OnEnd = onEnd;
        pos = -1;
        DialogueRenderer = dialogueRenderer;
    }

    public void HandleProgressDialogue()
    {
        if (DialogueRenderer.IsWriting)
        {
            DialogueRenderer.WriteFull();
        }
        else
        {
            pos++;

            if (pos <  texts.Count)
            {
                var dialogue = texts[pos];

                DialogueRenderer.ShowDialogue(dialogue.Text, dialogue.Name, End);
            }
            else
            {
                if (DialogueRenderer.IsOpen())
                {
                    End();
                }
            }
        }
    }

    public void Reset()
    {
        DialogueRenderer.closeDialogue();
        pos = -1;
    }

    private void End()
    {
        Reset();

        if (OnEnd != null)
        {
            OnEnd.Invoke();

        }
    }
}