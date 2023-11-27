using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    public List<string> ValidInteracterTags;
    public CharacterSwitcher CharacterSwitcher;
    public string InteractionAnimationTrigger;
    public float InteractionAnimationLength;
    public UnityEvent<GameObject> OnValidInteraction;
    public UnityEvent<GameObject> OnInvalidInteraction;
    public List<Condition> Conditions;
    public InteractableDialogue BeforeValidInteractionDialogue;

    private List<CharacterController2D> Interacters = new List<CharacterController2D>();
    private DisplayIconOnEnter Highlight;
    private bool IsInInteractableDialogue;

    void Start()
    {
        Highlight = GetComponent<DisplayIconOnEnter>();
    }

    void Update()
    {
        if (Interacters.Count != 0)
        {
            var activeInteracter = Interacters.Find(interacter => interacter.tag == GetCharacterSwitcher().ActiveCharacterTag);

            if (activeInteracter != null)
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                {
                    if ((!activeInteracter.IsFrozen() || IsInInteractableDialogue) && (ValidInteracterTags.Count == 0 || ValidInteracterTags.Contains(activeInteracter.tag)))
                    {
                        if (Conditions.All(condition => condition.Evaluate(activeInteracter.gameObject)))
                        {
                            if (BeforeValidInteractionDialogue != null)
                            {
                                IsInInteractableDialogue = true;

                                var finished = BeforeValidInteractionDialogue.Speak();

                                if (finished)
                                {
                                    IsInInteractableDialogue = false;
                                    StartCoroutine(Interact(activeInteracter));
                                }
                            }
                            else
                            {
                                IsInInteractableDialogue = false;
                                StartCoroutine(Interact(activeInteracter));
                            }
                        }
                        else
                        {
                            OnInvalidInteraction.Invoke(activeInteracter.gameObject);
                        }
                    }
                    else
                    {
                        OnInvalidInteraction.Invoke(activeInteracter.gameObject);
                    }
                }
                else
                {
                    ShowHighlight();
                }
            }
            else
            {
                HideHighlight();
            }
        }
        else
        {
            HideHighlight();
        }
    }

    IEnumerator Interact(CharacterController2D interacter)
    {
        interacter.Freeze();

        if (InteractionAnimationTrigger != null && InteractionAnimationLength != 0)
        {
            var animator = interacter.GetComponentInChildren<Animator>();

            animator.SetTrigger(InteractionAnimationTrigger);

            yield return new WaitForSeconds(InteractionAnimationLength);
        }

        OnValidInteraction.Invoke(interacter.gameObject);

        interacter.Thaw();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            Interacters.Add(collision.gameObject.GetComponent<CharacterController2D>());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            var characterController = collision.gameObject.GetComponent<CharacterController2D>();

            if (characterController != null)
            {
                Interacters.Remove(characterController);
            }
        }
    }

    private CharacterSwitcher GetCharacterSwitcher()
    {
        if (CharacterSwitcher == null)
        {
            CharacterSwitcher = FindObjectOfType<CharacterSwitcher>();
            return CharacterSwitcher;
        } else
        {
            return CharacterSwitcher;
        }
    }

    private void ShowHighlight()
    {
        if (Highlight != null && !Highlight.IsProximityBased)
        {
            Highlight.Show();
        }
    }

    private void HideHighlight()
    {
        if (Highlight != null && !Highlight.IsProximityBased)
        {
            Highlight.Hide();
        }
    }
}
