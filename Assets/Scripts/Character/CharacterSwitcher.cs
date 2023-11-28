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
    public CameraDirector CameraDirector;

    private CharacterController2D GiantCharacterController;
    private CharacterController2D GnomeCharacterController;
    private bool IsFrozenForDialogue;

    public GameObject gnomeAudio;
    public GameObject giantAudio;

    public void SetEnableSwithing(bool e)
    {
        EnableSwitching = e;
    }

    void Start()
    {
        GiantCharacterController = Giant.GetComponent<CharacterController2D>();
        GnomeCharacterController = Gnome.GetComponent<CharacterController2D>();
        giantAudio = giantAudio == null ? GameObject.Find("GiantAudioListener") : giantAudio;
        gnomeAudio = gnomeAudio == null ? GameObject.Find("GnomeAudioListener") : gnomeAudio;

        if (FirstActiveCharacter == Giant)
        {
            GnomeCharacterController.Freeze();
            GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
            GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
            ActiveCharacterTag = TagComparer.GIANT;
            Gnome.GetComponentInChildren<SpriteRenderer>().material = LitMaterial;
            Giant.GetComponentInChildren<SpriteRenderer>().material = UnlitMaterial;
            giantAudio.SetActive(true);
            gnomeAudio.SetActive(false);

        }
        else
        {
            GiantCharacterController.Freeze();
            GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
            GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
            ActiveCharacterTag = TagComparer.GNOME;
            Gnome.GetComponentInChildren<SpriteRenderer>().material = UnlitMaterial;
            Giant.GetComponentInChildren<SpriteRenderer>().material = LitMaterial;
            gnomeAudio.SetActive(true);
            giantAudio.SetActive(false);
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

        if (CameraDirector != null)
        {
            CameraDirector.Watch(Gnome);
        }
        else
        {
            CinemachineVirtualCamera.Follow = Gnome.transform;
        }

        GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
        GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
        ActiveCharacterTag = TagComparer.GNOME;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/gnome voices/gnome switch");
        Gnome.GetComponentInChildren<SpriteRenderer>().material = UnlitMaterial;
        Giant.GetComponentInChildren<SpriteRenderer>().material = LitMaterial;
        ToggleAudio();
    }

    public void SelectGiant()
    {
        GnomeCharacterController.Freeze();
        GiantCharacterController.Thaw();

        if (CameraDirector != null)
        {
            CameraDirector.Watch(Giant);
        }
        else
        {
            CinemachineVirtualCamera.Follow = Giant.transform;
        }

        GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
        GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
        ActiveCharacterTag = TagComparer.GIANT;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/giant voice/giant switcher");
        Gnome.GetComponentInChildren<SpriteRenderer>().material = LitMaterial;
        Giant.GetComponentInChildren<SpriteRenderer>().material = UnlitMaterial;
        ToggleAudio();
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

    public void ToggleAudio()
    {
        if (giantAudio.activeSelf == true)
        {
            giantAudio.SetActive(false);
            gnomeAudio.SetActive(true);
        }
        else if (gnomeAudio.activeSelf == true)
        {
            giantAudio.SetActive(true);
            gnomeAudio.SetActive(false);
        }
    }
}
