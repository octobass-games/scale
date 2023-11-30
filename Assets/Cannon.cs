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
    public CharacterSwitcher CharacterSwitcher;
    public ErrorDialogue ErrorDialogue;

    private GameObject Interacter;
    private bool IsFiring;
    private bool IsFiringGnome;


    public string fmodEvent;
    

    public void Land()
    {
        if (IsFiringGnome)
        {

            Interacter.GetComponent<CharacterController2D>().Thaw();
            Interacter.GetComponentInChildren<SpriteRenderer>().enabled = true;
            CharacterSwitcher.SetEnableSwithing(true);

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

        IsFiring = false;
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && Interacter != null && Interacter.tag == CharacterSwitcher.ActiveCharacterTag && !IsFiring)
        {
            if (TagComparer.IsGnome(Interacter.tag))
            {
                FMODUnity.RuntimeManager.PlayOneShot(fmodEvent);

                Interacter.GetComponent<CharacterController2D>().ForcePosition(EndPosition.position);
                Interacter.GetComponent<CharacterController2D>().Freeze();
                Interacter.GetComponentInChildren<SpriteRenderer>().enabled = false;
                CharacterSwitcher.SetEnableSwithing(false);

                ProjectileSpriteRenderer.sprite = GnomeProjectileSprite;
                ProjectileAnimator.enabled = true;

                IsFiring = true;
                IsFiringGnome = true;

                VirtualCamera.Follow = Projectile.transform;
            }
            else
            {
                var hasCannonball = Interacter.GetComponent<Inventory>().Contains(Cannonball);

                if (hasCannonball)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(fmodEvent);

                    Interacter.GetComponent<Inventory>().TakeItem(Cannonball);

                    ProjectileSpriteRenderer.sprite = CannonballProjectileSprite;
                    ProjectileAnimator.enabled = true;

                    IsFiring = true;
                    IsFiringGnome = false;
                }
                else
                {
                    ErrorDialogue.ShowNoUseDialogue(TagComparer.GIANT);
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
