using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public List<string> texts;
    public string CharacterName;
    private bool interactable = false;


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
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && interactable)
        {
            if (dialogueRenderer.IsOpen())
            {
                dialogueRenderer.closeDialogue();
            }
            else
            {
                dialogueRenderer.ShowDialogue(texts.PickRandom(), CharacterName);
            }
        }

    }
}
