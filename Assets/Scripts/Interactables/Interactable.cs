using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public List<string> ValidInteracterTags;
    public CharacterSwitcher CharacterSwitcher;
    public string InteractionAnimationTrigger;
    public float InteractionAnimationLength;
    public UnityEvent<GameObject> OnValidInteraction;
    public UnityEvent<GameObject> OnInvalidInteraction;
    public List<Condition> Conditions;

    private GameObject Interacter;
    private CharacterController2D InteracterCharacterController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Interacter != null && Interacter.tag == CharacterSwitcher.ActiveCharacterTag && !InteracterCharacterController.IsFrozen())
        {
            if (ValidInteracterTags.Count == 0 || ValidInteracterTags.Contains(Interacter.tag))
            {
                if (Conditions.All(condition => condition.Evaluate(Interacter)))
                {
                    StartCoroutine(Interact());
                }
            }
            else
            {
                OnInvalidInteraction.Invoke(Interacter);
            }
        }
    }

    IEnumerator Interact()
    {
        var characterController = Interacter.GetComponent<CharacterController2D>();

        characterController.Freeze();

        if (InteractionAnimationTrigger != null && InteractionAnimationLength != 0)
        {
            var animator = Interacter.GetComponentInChildren<Animator>();

            animator.SetTrigger(InteractionAnimationTrigger);

            yield return new WaitForSeconds(InteractionAnimationLength);
        }

        OnValidInteraction.Invoke(Interacter);

        characterController.Thaw();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            Interacter = collision.gameObject;
            InteracterCharacterController = Interacter.GetComponent<CharacterController2D>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag) && collision.gameObject == Interacter)
        {
            Interacter = null;
            InteracterCharacterController = null;
        }
    }
}
