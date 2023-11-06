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

    private CharacterController2D GiantCharacterController;
    private CharacterController2D GnomeCharacterController;
    public string ActiveCharacterTag = TagComparer.GNOME;
    public bool EnableSwitching = true;

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
            GiantCharacterController.Thaw();
            GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
            GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
            ActiveCharacterTag = TagComparer.GIANT;
        }
        else
        {
            GnomeCharacterController.Thaw();
            GiantCharacterController.Freeze();
            GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
            GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
            ActiveCharacterTag = TagComparer.GNOME;
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
        if (GiantCharacterController.IsFrozen())
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
    }

    public void SelectGiant()
    {
        GnomeCharacterController.Freeze();
        GiantCharacterController.Thaw();
        CinemachineVirtualCamera.Follow = Giant.transform;
        GnomeImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
        GiantImage.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
        ActiveCharacterTag = TagComparer.GIANT;
    }

}
