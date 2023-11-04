using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public List<DialogueItem> texts;
    private bool interactable = false;
    private int pos = 0;


    private DialogueRenderer dialogueRenderer;

    void Awake()
    {
        dialogueRenderer = FindObjectOfType<DialogueRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        interactable = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        interactable = false;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && interactable) {
            if (dialogueRenderer.IsOpen() && texts.Count == pos)
            {
                dialogueRenderer.closeDialogue();
                pos = 0;
            }
            else
            {
                var a = texts[pos];
                dialogueRenderer.ShowDialogue(a.Text, a.Name);
                pos++;

            }

        }


    }
}
