using UnityEngine;
using UnityEngine.UI;

public class InventoryRenderer : MonoBehaviour
{
    public GameObject InventorySlot;
    public Image InventorySlotImage;

    private CharacterSwitcher CharacterSwitcher;

    void Start()
    {
        CharacterSwitcher = FindObjectOfType<CharacterSwitcher>();    
    }

    void Update()
    {
        if (CharacterSwitcher != null)
        {
            var activeCharacter = CharacterSwitcher.GetActiveCharacter();
            var inventory = activeCharacter.GetComponent<Inventory>();

            if (inventory != null)
            {
                for (int i = 0; i < inventory.Items.Count; i++)
                {
                    InventorySlot.SetActive(true);
                    InventorySlotImage.sprite = inventory.Items[i].GetComponent<SpriteRenderer>().sprite;
                }

                if (inventory.Items.Count == 0)
                {
                    InventorySlot.SetActive(false);
                    InventorySlotImage.sprite = null;
                }
            }
        }
    }
}
