using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera CinemachineVirtualCamera;
    public GameObject Giant;
    public GameObject Gnome;
    public Image GiantImage;
    public Image GnomeImage;
    public GameObject FirstActiveCharacter;
    public Material LitMaterial;
    public Material UnlitMaterial;
    public string ActiveCharacterTag = TagComparer.GNOME;
    public bool EnableSwitching = true;

    private CharacterController2D GiantCharacterController;
    private CharacterController2D GnomeCharacterController;
    private bool IsFrozenForDialogue;

    public void SetEnableSwithing(bool e)
    {
        EnableSwitching = e;
    }
    void Start()
    {
        GiantCharacterController = Giant.GetComponent<CharacterController2D>();
        GnomeCharacterController = Gnome.GetComponent<CharacterController2D>();

        if (FirstActiveCharacter == Giant)
        {
            GnomeCharacterController.Freeze();
            GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
            GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
            ActiveCharacterTag = TagComparer.GIANT;
            Gnome.GetComponentInChildren<SpriteRenderer>().material = LitMaterial;
            Giant.GetComponentInChildren<SpriteRenderer>().material = UnlitMaterial;
        }
        else
        {
            GiantCharacterController.Freeze();
            GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
            GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
            ActiveCharacterTag = TagComparer.GNOME;
            Gnome.GetComponentInChildren<SpriteRenderer>().material = UnlitMaterial;
            Giant.GetComponentInChildren<SpriteRenderer>().material = LitMaterial;
        }
    }

    void Update()
    {
        if (EnableSwitching && Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCharacter();
        }
    }

    public void SwitchCharacter()
    {
        if (ActiveCharacterTag == TagComparer.GIANT)
        {
            SelectGnome();
        }
        else
        {
            SelectGiant();
        }
    }

    public void SelectGnome()
    {
        GnomeCharacterController.Thaw();
        GiantCharacterController.Freeze();
        CinemachineVirtualCamera.Follow = Gnome.transform;
        GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
        GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
        ActiveCharacterTag = TagComparer.GNOME;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/gnome voices/gnome switch");
        Gnome.GetComponentInChildren<SpriteRenderer>().material = UnlitMaterial;
        Giant.GetComponentInChildren<SpriteRenderer>().material = LitMaterial;
    }

    public void SelectGiant()
    {
        GnomeCharacterController.Freeze();
        GiantCharacterController.Thaw();
        CinemachineVirtualCamera.Follow = Giant.transform;
        GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
        GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
        ActiveCharacterTag = TagComparer.GIANT;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/giant voice/giant switcher");
        Gnome.GetComponentInChildren<SpriteRenderer>().material = LitMaterial;
        Giant.GetComponentInChildren<SpriteRenderer>().material = UnlitMaterial;
    }

    public void FreezeForDialogue()
    {
        if (!IsFrozenForDialogue)
        {
            GiantCharacterController.Freeze();
            GnomeCharacterController.Freeze();
            IsFrozenForDialogue = true;
        }
    }

    public void ThawForDialogue()
    {
        if (IsFrozenForDialogue)
        {
            GiantCharacterController.Thaw();
            GnomeCharacterController.Thaw();
            IsFrozenForDialogue = false;
        }
    }

    public GameObject GetActiveCharacter()
    {
        if (ActiveCharacterTag == TagComparer.GIANT)
        {
            return Giant;
        }
        else
        {
            return Gnome;
        }
    }
}
