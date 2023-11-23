using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    public List<DialogueItem> texts;
    [TextArea]
    public string text;
    private bool interactable = false;
    public UnityEvent OnEnd;
    private DialogueController DialogueController;
    private DialogueRenderer dialogueRenderer;
    private CharacterSwitcher CharacterSwitcher;

    void Awake()
    {
        if (texts.Count == 0)
        {
            texts = text.ParseToDialogue();
        }
        dialogueRenderer = FindObjectOfType<DialogueRenderer>();
        DialogueController = new DialogueController(texts, OnEnd, dialogueRenderer);
        CharacterSwitcher = FindObjectOfType<CharacterSwitcher>();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (TagComparer.IsPlayer(col) && col.CompareTag(CharacterSwitcher.ActiveCharacterTag))
        {
            interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        interactable = false;
        DialogueController.Reset();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && interactable) {
           DialogueController.HandleProgressDialogue();
        }
    }
}
