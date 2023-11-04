using Cinemachine;
using Unity.VisualScripting;
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

    void Start()
    {
        GiantCharacterController = Giant.GetComponent<CharacterController2D>();
        GnomeCharacterController = Gnome.GetComponent<CharacterController2D>();

        if (FirstActiveCharacter == Giant)
        {
            GnomeCharacterController.Freeze();
            GiantCharacterController.Thaw();
            GnomeImage.color = Color.white.WithAlpha(0.5f);
            GiantImage.color = Color.white.WithAlpha(1f);
        }
        else
        {
            GnomeCharacterController.Thaw();
            GiantCharacterController.Freeze();
            GnomeImage.color = Color.white.WithAlpha(1f);
            GiantImage.color = Color.white.WithAlpha(0.5f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Hello");
            SwitchCharacter();
        }
    }

    public void SwitchCharacter()
    {
        if (GiantCharacterController.IsFrozen())
        {
            GnomeCharacterController.Thaw();
            GiantCharacterController.Freeze();
            CinemachineVirtualCamera.Follow = Gnome.transform;
            GnomeImage.color = Color.white.WithAlpha(1f);
            GiantImage.color = Color.white.WithAlpha(0.5f);
        }
        else
        {
            GnomeCharacterController.Freeze();
            GiantCharacterController.Thaw();
            CinemachineVirtualCamera.Follow = Giant.transform;
            GnomeImage.color = Color.white.WithAlpha(0.5f);
            GiantImage.color = Color.white.WithAlpha(1f);
        }
    }
}
