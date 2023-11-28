using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCam : MonoBehaviour
{
    public CameraDirector CameraDirector;
    public CharacterSwitcher CharacterSwitcher;
    public GameObject CinemachineVirtualCamera;

    public bool IsGameObjectInView(GameObject gameObject)
    {
        var contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        Debug.Log(gameObject.name);
        return GetComponent<BoxCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>(), contactFilter);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagComparer.IsPlayer(collision.tag) && CharacterSwitcher.ActiveCharacterTag == collision.tag)
        {
            Debug.Log($"{this.gameObject.name}");
            CameraDirector.Watch(this, collision.gameObject);
        }
    }
}
