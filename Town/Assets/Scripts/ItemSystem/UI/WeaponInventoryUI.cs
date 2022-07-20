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

    private Dictionary<string, int> weaponItems;
    private Dictionary<string, ItemProperties> allItems;

    void Start()
    {
        currentSlot = weaponItemSlots.transform.GetChild(0).gameObject;
    }

    void fillSlots()
    {
        int i = 0;
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
                GameObject slot = weaponItemSlots.transform.GetChild(i).gameObject;
                TMP_Text powerLabel = slot.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
                TMP_Text quantityLabel = slot.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();

                // Enables enabled slot UI image.
                slot.transform.GetChild(0).gameObject.SetActive(false);
                slot.transform.GetChild(1).gameObject.SetActive(true);

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

                i++;
            }
        }
    }

    void Update()
    {
        fillSlots();
        currentSlot.transform.GetChild(2).gameObject.SetActive(true);
    }
}
