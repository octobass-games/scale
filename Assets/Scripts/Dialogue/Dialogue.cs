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
    public List<Animator> Animators;

    void Awake()
    {
        init();

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
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && interactable) {
           DialogueController.HandleProgressDialogue();
        }
    }

    public void StartDialogue()
    {
        init();
        interactable = true;
        DialogueController.HandleProgressDialogue();
    }

    public void StopDialogue()
    {
        interactable = false;
    }

    private void init()
    {
        if (texts.Count == 0)
        {
            texts = text.ParseToDialogue();
        }
        dialogueRenderer = FindObjectOfType<DialogueRenderer>();
        DialogueController = new DialogueController(texts, OnEnd, dialogueRenderer, Animators);
        CharacterSwitcher = FindObjectOfType<CharacterSwitcher>();
    }
}
