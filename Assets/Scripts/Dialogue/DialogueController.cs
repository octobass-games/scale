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
    private List<Animator> Animators = new List<Animator>();

    public DialogueController(List<DialogueItem> texts, UnityEvent onEnd, DialogueRenderer dialogueRenderer, List<Animator> animator = null)
    {
        this.texts = texts;
        OnEnd = onEnd;
        pos = -1;
        DialogueRenderer = dialogueRenderer;
        Animators = animator;
    }

    public bool HandleProgressDialogue()
    {
        if (DialogueRenderer.IsWriting)
        {
            DialogueRenderer.WriteFull();

            return false;
        }
        else
        {
            pos++;

            if (pos <  texts.Count)
            {
                var dialogue = texts[pos];

                DialogueRenderer.ShowDialogue(dialogue.Text, dialogue.Name, End);
                if ( dialogue.animatorTrigger != null && dialogue.animatorTrigger != "")
                {
                    Animators.ForEach(a => a.SetTrigger(dialogue.animatorTrigger));
                }

                return false;
            }
            else
            {
                if (DialogueRenderer.IsOpen())
                {
                    End();

                    return true;
                }

                return false;
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