using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClothingInventory : MonoBehaviour, IInventory
{
    public GameObject player;
    public GameObject clothingItemSlots;
    public int numberOfSlots;

    private Dictionary<string, int> clothingItems;
    private Dictionary<string, IItem> allItems;
    private string[] itemList;

    /** Fills the inventory slots. */
    public void fillSlots()
    {
        clothingItems = player.GetComponent<PlayerInventory>().getClothingItems();
        allItems = player.GetComponent<PlayerInventory>().getAllItems();

        int slotIndex = 0;

        // Checks if the player has any weapons.
        if (clothingItems != null && clothingItems.Count > 0)
        {
            itemList = new string[clothingItems.Count];

            // Iterates through the entire dictionary.
            foreach (KeyValuePair<string, int> item in clothingItems)
            {
                // Gets the properties for the item as well as the current slot and the labels for that slot.
                ClothingItem itemProperties = (ClothingItem)allItems[item.Key];
                GameObject slot = clothingItemSlots.transform.GetChild(slotIndex).gameObject;
                TMP_Text quantityLabel = slot.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
                GameObject itemDisplay = slot.transform.GetChild(5).gameObject;

                // Enables enabled slot UI image.
                slot.transform.GetChild(0).gameObject.SetActive(false);
                slot.transform.GetChild(1).gameObject.SetActive(true);
                slot.transform.GetChild(4).gameObject.SetActive(true);
                slot.transform.GetChild(5).gameObject.SetActive(true);

                // Sets the quantity label.
                if (item.Value <= 0)
                {
                    quantityLabel.text = "";
                }
                else
                {
                    quantityLabel.text = "x" + item.Value.ToString();
                }

                // Checks if there is already an item display gameObject for that slot.
                if (itemDisplay.transform.childCount > 0)
                {
                    // Destroys all of its children to prevent copies.
                    foreach (Transform child in itemDisplay.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }

                // Sets the item display for the item.
                GameObject newItemDisplay = Instantiate(itemProperties.getItemDisplay());
                newItemDisplay.transform.position = itemDisplay.transform.position;
                newItemDisplay.transform.parent = itemDisplay.transform;

                if (itemList[slotIndex] != item.Key)
                {
                    itemList[slotIndex] = item.Key;
                }

                slotIndex++;
            }
        }
    }

    /** Presents the selected item's info in the item info UI area. */
    public void presentSelectedItemInfo(int currentSlotIndex)
    {
        ClothingItem itemInfo = (ClothingItem)allItems[itemList[currentSlotIndex]];

        GameObject itemInfoUI = clothingItemSlots.transform.GetChild(14).gameObject;

        TMP_Text itemNameLabel = itemInfoUI.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        TMP_Text itemDescLabel = itemInfoUI.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
        TMP_Text itemQuantityLabel = itemInfoUI.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();

        // Displays item information.
        itemNameLabel.text = itemInfo.getItemName();
        itemDescLabel.text = itemInfo.getItemDesc();
        itemQuantityLabel.text = itemInfo.getClothingDefense().ToString();

        GameObject itemDisplay = clothingItemSlots.transform.GetChild(13).gameObject;

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

    /** Gets the number of items in the inventory. */
    public int getItemCount()
    {
        int numberOfItems = 0;

        if (clothingItems != null)
        {
            numberOfItems = clothingItems.Count;
        }

        return numberOfItems;
    }

    /** Gets the inventory's items. */
    public Dictionary<string, int> getItems()
    {
        return clothingItems;
    }

    /** Uses the item selected. */
    public void useItem(int currentSlotIndex)
    {
        ClothingItem itemInfo = (ClothingItem)allItems[itemList[currentSlotIndex]];

        print("Your defense is now: " + itemInfo.getClothingDefense());
    }

    /** Drops the item selected. */
    public void dropItem(int currentSlotIndex, int numberDropped)
    {
        ClothingItem itemInfo = (ClothingItem)allItems[itemList[currentSlotIndex]];
        int numberOfItemRemaining = clothingItems[itemInfo.getItemName()] - numberDropped;

        // Checks if there is no more of that item remaining.
        if (numberOfItemRemaining == 0)
        {
            // Deletes the current slot item display.
            GameObject slot = clothingItemSlots.transform.GetChild(currentSlotIndex).gameObject;
            GameObject itemDisplay = slot.transform.GetChild(5).gameObject;
            DestroyImmediate(itemDisplay.transform.GetChild(0).gameObject);

            GameObject lastSlot = clothingItemSlots.transform.GetChild(getItemCount() - 1).gameObject;

            // Deletes the last slot's item display if there is more than 0 items left and the lastSlot is not the same as the slot.
            if (getItemCount() > 0 && lastSlot != slot)
            {
                GameObject lastItemDisplay = lastSlot.transform.GetChild(5).gameObject;
                DestroyImmediate(lastItemDisplay.transform.GetChild(0).gameObject);
            }
        }

        // Checks if the number dropped is less than or equal to the amount of the item possessed.
        if (clothingItems.ContainsKey(itemInfo.getItemName()) && numberDropped <= clothingItems[itemInfo.getItemName()])
        {
            for (int i = 0; i < numberDropped; i++)
            {
                // Removes the item from the inventory.
                player.GetComponent<PlayerInventory>().removeClothingItem(itemInfo);

                // Sets random positions for the dropped item around the player.
                float randomX = Random.Range(player.transform.position.x - 5f, player.transform.position.x + 5f);
                float randomZ = Random.Range(player.transform.position.z - 5f, player.transform.position.z + 5f);

                // Drops items around the player.
                GameObject droppedItem = itemInfo.getItemGameObject();
                droppedItem.transform.position = new Vector3(randomX, droppedItem.transform.position.y, randomZ);

                // Sets the item quantity to 1 since only one item is dropped.
                droppedItem.GetComponent<IItem>().setItemQuantity(1);
            }
        }
    }
}
