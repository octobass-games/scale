using Cinemachine;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Animator ProjectileAnimator;
    public SpriteRenderer ProjectileSpriteRenderer;
    public Sprite GnomeProjectileSprite;
    public Sprite CannonballProjectileSprite;
    public Transform EndPosition;
    public CinemachineVirtualCamera VirtualCamera;
    public GameObject Projectile;
    public GameObject Cannonball;
    public GameObject InteractableCannonball;

    private GameObject Interacter;
    private bool IsFiringGnome;

    public void Land()
    {
        if (IsFiringGnome)
        {
            Interacter.GetComponent<CharacterController2D>().Thaw();
            Interacter.GetComponentInChildren<SpriteRenderer>().enabled = true;

            ProjectileSpriteRenderer.sprite = null;
            ProjectileAnimator.enabled = false;

            IsFiringGnome = false;
            VirtualCamera.Follow = Interacter.transform;
            
            Interacter = null;
        }
        else
        {
            InteractableCannonball.gameObject.SetActive(true);

            ProjectileSpriteRenderer.sprite = null;
            ProjectileAnimator.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Interacter != null)
        {
            if (TagComparer.IsGnome(Interacter.tag))
            {

                Interacter.GetComponent<CharacterController2D>().ForcePosition(EndPosition.position);
                Interacter.GetComponent<CharacterController2D>().Freeze();
                Interacter.GetComponentInChildren<SpriteRenderer>().enabled = false;

                ProjectileSpriteRenderer.sprite = GnomeProjectileSprite;
                ProjectileAnimator.enabled = true;

                IsFiringGnome = true;

                VirtualCamera.Follow = Projectile.transform;
            }
            else
            {
                var hasCannonball = Interacter.GetComponent<Inventory>().Contains(Cannonball);

                if (hasCannonball)
                {
                    Interacter.GetComponent<Inventory>().RemoveItem(Cannonball);

                    ProjectileSpriteRenderer.sprite = CannonballProjectileSprite;
                    ProjectileAnimator.enabled = true;

                    IsFiringGnome = false;
                }
                else
                {
                    Debug.Log("No cannonball");
                }
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
        if (TagComparer.IsPlayer(collision.tag) && !IsFiringGnome)
        {
            Interacter = null;
        }
    }
}
