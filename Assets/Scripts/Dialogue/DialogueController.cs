using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController
{
    public List<DialogueItem> texts;
    private int pos = 0;
    public UnityEvent OnEnd;
    public DialogueRenderer DialogueRenderer;

    public DialogueController(List<DialogueItem> texts, UnityEvent onEnd, DialogueRenderer dialogueRenderer)
    {
        this.texts = texts;
        OnEnd = onEnd;
        pos = 0;
        DialogueRenderer = dialogueRenderer;
    }

    public void HandleProgressDialogue()
    {
        if (DialogueRenderer.IsOpen() && texts.Count == pos)
        {
            Reset();
            if (OnEnd != null)
            {
                OnEnd.Invoke();
            }
        }
        else
        {

            var a = texts[pos];
            Debug.Log(a);
            var isRendererNotRevealingPreviousLine = DialogueRenderer.ShowDialogue(a.Text, a.Name);

            if (isRendererNotRevealingPreviousLine)
            {
                pos++;
            }
        }
    }

    public void Reset()
    {
        DialogueRenderer.closeDialogue();
        pos = 0;
    }
}