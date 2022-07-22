using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealingInventory : MonoBehaviour, IInventory
{
    public GameObject player;
    public GameObject healingItemSlots;

    private Dictionary<string, int> healingItems;
    private Dictionary<string, ItemProperties> allItems;
    private string[] itemList;

    // Fills the inventory slots.
    public void fillSlots()
    {
        healingItems = player.GetComponent<PlayerInventory>().getHealingItems();
        allItems = player.GetComponent<PlayerInventory>().getAllItems();

        int slotIndex = 0;

        // Checks if the player has any weapons.
        if (healingItems != null && healingItems.Count > 0)
        {
            itemList = new string[healingItems.Count];

            // Iterates through the entire dictionary.
            foreach (KeyValuePair<string, int> item in healingItems)
            {
                // Gets the properties for the item as well as the current slot and the labels for that slot.
                HealingItemProperties itemProperties = (HealingItemProperties)allItems[item.Key];
                GameObject slot = healingItemSlots.transform.GetChild(slotIndex).gameObject;
                TMP_Text quantityLabel = slot.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
                GameObject itemDisplay = slot.transform.GetChild(4).gameObject;

                // Enables enabled slot UI image.
                slot.transform.GetChild(0).gameObject.SetActive(false);
                slot.transform.GetChild(1).gameObject.SetActive(true);
                slot.transform.GetChild(3).gameObject.SetActive(true);
                slot.transform.GetChild(4).gameObject.SetActive(true);

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
                GameObject newItemDisplay = Instantiate(itemProperties.getItemGameObject());
                newItemDisplay.transform.position = itemDisplay.transform.position;
                newItemDisplay.transform.parent = itemDisplay.transform;

                itemList[slotIndex] = item.Key;

                slotIndex++;
            }
        }
    }

    public void presentSelectedItemInfo(int currentSlotIndex)
    {
        HealingItemProperties itemInfo = (HealingItemProperties)allItems[itemList[currentSlotIndex]];

        GameObject itemInfoUI = healingItemSlots.transform.GetChild(14).gameObject;

        TMP_Text itemNameLabel = itemInfoUI.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        TMP_Text itemDescLabel = itemInfoUI.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
        TMP_Text itemQuantityLabel = itemInfoUI.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();

        // Displays item information.
        itemNameLabel.text = itemInfo.getItemName();
        itemDescLabel.text = itemInfo.getItemDesc();
        itemQuantityLabel.text = itemInfo.getHealingPower().ToString();

        GameObject itemDisplay = healingItemSlots.transform.GetChild(13).gameObject;

        // Destroys all child gameObjects of itemDisplay.
        foreach (Transform child in itemDisplay.transform)
        {
            Destroy(child.gameObject);
        }

        // Sets the item display.
        GameObject newItemDisplay = Instantiate(itemInfo.getItemGameObject());
        newItemDisplay.transform.position = itemDisplay.transform.position;
        newItemDisplay.transform.parent = itemDisplay.transform;
        newItemDisplay.transform.localScale = newItemDisplay.transform.localScale * 3f;
    }

    public int getItemCount()
    {
        int numberOfItems = 0;

        if (healingItems != null)
        {
            numberOfItems = healingItems.Count;
        }

        return numberOfItems;
    }
}
