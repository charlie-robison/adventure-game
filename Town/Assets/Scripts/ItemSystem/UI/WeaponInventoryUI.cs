using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInventoryUI : MonoBehaviour
{
    public GameObject player;
    public GameObject weaponItemSlots;
    public GameObject currentSlot;
    public int currentSlotIndex;

    private Dictionary<string, int> weaponItems;
    private Dictionary<string, ItemProperties> allItems;

    void Start()
    {
        for (int i = 0; i < weaponItemSlots.transform.childCount; i++)
        {
            GameObject slot = weaponItemSlots.transform.GetChild(i).gameObject;
            slot.transform.GetChild(0).gameObject.SetActive(true);
            slot.transform.GetChild(1).gameObject.SetActive(false);
            slot.transform.GetChild(2).gameObject.SetActive(false);
            slot.transform.GetChild(3).gameObject.SetActive(false);
            slot.transform.GetChild(4).gameObject.SetActive(false);

            fillSlots();
        }

        currentSlot = null;
    }

    public void fillSlots()
    {
        int slotIndex = 0;
        weaponItems = player.GetComponent<PlayerInventory>().getWeaponItems();
        allItems = player.GetComponent<PlayerInventory>().getAllItems();

        // Checks if the player has any weapons.
        if (weaponItems != null)
        {
            // Iterates through the entire dictionary.
            foreach (KeyValuePair<string, int> item in weaponItems)
            {
                // Gets the properties for the item as well as the current slot and the labels for that slot.
                WeaponItemProperties itemProperties = (WeaponItemProperties)allItems[item.Key];
                GameObject slot = weaponItemSlots.transform.GetChild(slotIndex).gameObject;
                TMP_Text powerLabel = slot.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
                TMP_Text quantityLabel = slot.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
                GameObject itemDisplay = slot.transform.GetChild(5).gameObject;

                // Enables enabled slot UI image.
                slot.transform.GetChild(0).gameObject.SetActive(false);
                slot.transform.GetChild(1).gameObject.SetActive(true);
                slot.transform.GetChild(3).gameObject.SetActive(true);
                slot.transform.GetChild(4).gameObject.SetActive(true);

                // Sets the power label.
                powerLabel.text = itemProperties.getWeaponPower().ToString();

                // Sets the quantity label.
                if (item.Value <= 0)
                {
                    quantityLabel.text = "";
                }
                else
                {
                    quantityLabel.text = "x" + item.Value.ToString();
                }

                // Sets the item display for the item.
                GameObject newItemDisplay = Instantiate(itemProperties.getItemGameObject(), itemDisplay.transform.position, itemDisplay.transform.rotation);
                newItemDisplay.transform.parent = itemDisplay.transform;

                slotIndex++;
            }
        }
    }

    void Update()
    {
        // fillSlots();
        if (currentSlot != null)
        {
            currentSlot = weaponItemSlots.transform.GetChild(currentSlotIndex).gameObject;
            currentSlot.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
