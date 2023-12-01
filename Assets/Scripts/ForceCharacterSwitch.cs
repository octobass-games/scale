using UnityEngine;

public class ForceCharacterSwitch : MonoBehaviour
{
    public CharacterSwitcher CharacterSwitcher;
    public GameObject CharacterSwitcherPanel;
    public GameObject CharacterSwitcherInstruction;
    public PauseManager PauseManager;

    void Update()
    {
        if (Input.anyKeyDown && !PauseManager.IsPaused)
        {
            CharacterSwitcher.SetEnableSwithing(true);
            CharacterSwitcherPanel.SetActive(true);
            CharacterSwitcherInstruction.SetActive(false);
        }        
    }
}
