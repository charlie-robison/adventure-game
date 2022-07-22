using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialInventory : MonoBehaviour, IInventory
{
    public GameObject player;
    public GameObject materialItemSlots;

    private Dictionary<string, int> materialItems;
    private Dictionary<string, IItem> allItems;
    private string[] itemList;

    // Fills the inventory slots.
    public void fillSlots()
    {
        materialItems = player.GetComponent<PlayerInventory>().getMaterialItems();
        allItems = player.GetComponent<PlayerInventory>().getAllItems();

        int slotIndex = 0;

        // Checks if the player has any weapons.
        if (materialItems != null && materialItems.Count > 0)
        {
            itemList = new string[materialItems.Count];

            // Iterates through the entire dictionary.
            foreach (KeyValuePair<string, int> item in materialItems)
            {
                // Gets the properties for the item as well as the current slot and the labels for that slot.
                MaterialItem itemProperties = (MaterialItem)allItems[item.Key];
                GameObject slot = materialItemSlots.transform.GetChild(slotIndex).gameObject;
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
                GameObject newItemDisplay = Instantiate(itemProperties.getItemDisplay());
                newItemDisplay.transform.position = itemDisplay.transform.position;
                newItemDisplay.transform.parent = itemDisplay.transform;

                itemList[slotIndex] = item.Key;

                slotIndex++;
            }
        }
    }

    public void presentSelectedItemInfo(int currentSlotIndex)
    {
        MaterialItem itemInfo = (MaterialItem)allItems[itemList[currentSlotIndex]];

        GameObject itemInfoUI = materialItemSlots.transform.GetChild(14).gameObject;

        TMP_Text itemNameLabel = itemInfoUI.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        TMP_Text itemDescLabel = itemInfoUI.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
        TMP_Text itemQuantityLabel = itemInfoUI.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();

        // Displays item information.
        itemNameLabel.text = itemInfo.getItemName();
        itemDescLabel.text = itemInfo.getItemDesc();
        itemQuantityLabel.text = itemInfo.getMaterialTypePower().ToString();

        GameObject itemDisplay = materialItemSlots.transform.GetChild(13).gameObject;

        // Destroys all child gameObjects of itemDisplay.
        foreach (Transform child in itemDisplay.transform)
        {
            Destroy(child.gameObject);
        }

        // Sets the item display.
        GameObject newItemDisplay = Instantiate(itemInfo.getItemDisplay());
        newItemDisplay.transform.position = itemDisplay.transform.position;
        newItemDisplay.transform.parent = itemDisplay.transform;
        newItemDisplay.transform.localScale = newItemDisplay.transform.localScale * 3f;
    }

    public int getItemCount()
    {
        int numberOfItems = 0;

        if (materialItems != null)
        {
            numberOfItems = materialItems.Count;
        }

        return numberOfItems;
    }
}
