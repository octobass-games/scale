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
                var item = inventory.GetItem();

                if (item != null)
                {
                    InventorySlot.SetActive(true);
                    InventorySlotImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    InventorySlot.SetActive(false);
                    InventorySlotImage.sprite = null;
                }
            }
        }
    }
}
