using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public List<string> ValidInteracterTags;
    public CharacterSwitcher CharacterSwitcher;
    public UnityEvent<GameObject> OnValidInteraction;
    public UnityEvent<GameObject> OnInvalidInteraction;

    private GameObject Interacter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Interacter != null && Interacter.tag == CharacterSwitcher.ActiveCharacterTag)
        {
            if (ValidInteracterTags.Count == 0 || ValidInteracterTags.Contains(Interacter.tag))
            {
                OnValidInteraction.Invoke(Interacter);
            }
            else
            {
                OnInvalidInteraction.Invoke(Interacter);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            Interacter = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag))
        {
            Interacter = null;
        }
    }
}
