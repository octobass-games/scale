﻿using System.Collections.Generic;
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

                DialogueRenderer.ShowDialogue(dialogue.Text, dialogue.Name);
            }
            else
            {
                if (DialogueRenderer.IsOpen())
                {
                    Reset();

                    if (OnEnd != null)
                    {
                        OnEnd.Invoke();
                    }
                }
            }
        }

        //if (DialogueRenderer.IsOpen() && texts.Count == pos)
        //{
        //    if (IsRendererNotRevealingPreviousLine)
        //    {
        //        var a = texts[pos];

        //        IsRendererNotRevealingPreviousLine = DialogueRenderer.ShowDialogue(a.Text, a.Name);

        //        pos++;
        //    }
        //    else
        //    {
        //        Reset();

        //        if (OnEnd != null)
        //        {
        //            OnEnd.Invoke();
        //        }
        //    }
        //}
        //else
        //{
        //    var a = texts[pos];

        //    DialogueRenderer.ShowDialogue(a.Text, a.Name);

        //    if (DialogueRenderer.IsWriting)
        //    {
        //        pos++;
        //    }
        //}
    }

    public void Reset()
    {
        DialogueRenderer.closeDialogue();
        pos = 0;
    }
}