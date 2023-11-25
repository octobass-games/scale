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

    private List<CharacterController2D> Interacters = new List<CharacterController2D>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Interacters.Count != 0)
        {
            var interacter = Interacters.Find(interacter => interacter.tag == GetCharacterSwitcher().ActiveCharacterTag);

            if (interacter != null)
            {   
                if (!interacter.IsFrozen() && ValidInteracterTags.Count == 0 || ValidInteracterTags.Contains(interacter.tag))
                {
                    if (Conditions.All(condition => condition.Evaluate(interacter.gameObject)))
                    {
                        StartCoroutine(Interact(interacter));
                    }
                }
                else
                {
                    OnInvalidInteraction.Invoke(interacter.gameObject);
                }
            }
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

            var highlight = GetComponent<DisplayIconOnEnter>();

            if (highlight && !highlight.IsProximityBased)
            {
                highlight.Show();
            }
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

            if (Interacters.Count == 0)
            {
                var highlight = GetComponent<DisplayIconOnEnter>();

                if (highlight && !highlight.IsProximityBased)
                {
                    highlight.Hide();
                }
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
}
