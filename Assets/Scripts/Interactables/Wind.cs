using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public CharacterController2D Gnome;
    private GameObject Interacter;
    public Vector2 ForcedVelocity;
    public bool InWind;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagComparer.IsGnome(collision.tag))
        {

            Interacter = collision.gameObject;
            Gnome.VelocityModifier = ForcedVelocity;
            InWind= true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (TagComparer.IsGnome(collision.tag) && collision.gameObject == Interacter)
        {
            Interacter = null;
            InWind = false;

            Gnome.VelocityModifier = new Vector2(0, 0);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Gnome = GameObject.FindWithTag(TagComparer.GNOME).GetComponent<CharacterController2D>();


    }

    // Update is called once per frame
    void Update()
    {
    }
}
