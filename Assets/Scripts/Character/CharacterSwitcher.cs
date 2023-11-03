using Cinemachine;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera CinemachineVirtualCamera;
    public GameObject Giant;
    public GameObject Gnome;
    public GameObject FirstActiveCharacter;

    private CharacterController2D GiantCharacterController;
    private CharacterController2D GnomeCharacterController;

    void Start()
    {
        GiantCharacterController = Giant.GetComponent<CharacterController2D>();
        GnomeCharacterController = Gnome.GetComponent<CharacterController2D>();

        if (FirstActiveCharacter == Giant)
        {
            GnomeCharacterController.enabled = false;
        }
        else
        {
            GiantCharacterController.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCharacter();
        }
    }

    public void SwitchCharacter()
    {
        if (GiantCharacterController.enabled)
        {
            GnomeCharacterController.enabled = true;
            GiantCharacterController.enabled = false;
            CinemachineVirtualCamera.Follow = Gnome.transform;
        }
        else
        {
            GnomeCharacterController.enabled = false;
            GiantCharacterController.enabled = true;
            CinemachineVirtualCamera.Follow = Giant.transform;
        }
    }
}
