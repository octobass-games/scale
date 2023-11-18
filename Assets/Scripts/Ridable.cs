using UnityEngine;

public class Ridable : MonoBehaviour
{
    public GameObject RidableGameObject;

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.SetParent(RidableGameObject.transform);   
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.transform.SetParent(null);    
    }
}
