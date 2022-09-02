using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClothingInventory : MonoBehaviour, IInventory
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject clothingItemSlots;

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
                TMP_Text defenseLabel = slot.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
                GameObject itemDisplay = slot.transform.GetChild(5).gameObject;

                // Enables enabled slot UI image.
                slot.transform.GetChild(0).gameObject.SetActive(false);
                slot.transform.GetChild(1).gameObject.SetActive(true);
                slot.transform.GetChild(4).gameObject.SetActive(true);
                slot.transform.GetChild(5).gameObject.SetActive(true);

                // Sets the defense label.
                defenseLabel.text = itemProperties.getClothingDefense().ToString();

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
        TMP_Text playerDefenseLabel = itemInfoUI.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
        TMP_Text playerDefenseAfterEquipLabel = itemInfoUI.transform.GetChild(6).gameObject.GetComponent<TMP_Text>();

        // Displays item information.
        itemNameLabel.text = itemInfo.getItemName();
        itemDescLabel.text = itemInfo.getItemDesc();
        playerDefenseLabel.text = player.GetComponent<Stats>().getDefense().ToString();
        playerDefenseAfterEquipLabel.text = (player.GetComponent<Stats>().getBaseDefense() + itemInfo.getClothingDefense()).ToString();

        // Checks if the slot is equipped.
        if (clothingItemSlots.transform.GetChild(currentSlotIndex).GetChild(2).gameObject.activeSelf)
        {
            // Disables playerDefenseAfterEquipLabel and arrow for equipped slot.
            itemInfoUI.transform.GetChild(5).gameObject.SetActive(false);
            itemInfoUI.transform.GetChild(6).gameObject.SetActive(false);
            itemInfoUI.transform.GetChild(7).gameObject.SetActive(false);
        }
        else
        {
            itemInfoUI.transform.GetChild(5).gameObject.SetActive(true);
            itemInfoUI.transform.GetChild(6).gameObject.SetActive(true);
            itemInfoUI.transform.GetChild(7).gameObject.SetActive(true);

            // Checks if the player's current defense is greater than the player's base defense plus the new clothing defense.
            if (player.GetComponent<Stats>().getDefense() > (player.GetComponent<Stats>().getBaseDefense() + itemInfo.getClothingDefense()))
            {
                playerDefenseAfterEquipLabel.color = Color.red;
            }
            else
            {
                playerDefenseAfterEquipLabel.color = Color.white;
            }
        }

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
        GameObject itemInfoUI = clothingItemSlots.transform.GetChild(14).gameObject;
        TMP_Text playerDefenseLabel = itemInfoUI.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
        TMP_Text playerDefenseAfterEquipLabel = itemInfoUI.transform.GetChild(6).gameObject.GetComponent<TMP_Text>();

        // Checks if slot is already equipped.
        if (clothingItemSlots.transform.GetChild(currentSlotIndex).GetChild(2).gameObject.activeSelf)
        {
            // Unequips the item.
            clothingItemSlots.transform.GetChild(currentSlotIndex).GetChild(2).gameObject.SetActive(false);

            // Disables playerDefenseAfterEquipLabel and arrow for equipped slot.
            itemInfoUI.transform.GetChild(5).gameObject.SetActive(true);
            itemInfoUI.transform.GetChild(6).gameObject.SetActive(true);
            itemInfoUI.transform.GetChild(7).gameObject.SetActive(true);

            // Resets defense of player to base defense then adds the defense from the clothing.
            player.GetComponent<Stats>().resetDefense();

            playerDefenseLabel.text = player.GetComponent<Stats>().getDefense().ToString();
            playerDefenseAfterEquipLabel.text = (player.GetComponent<Stats>().getBaseDefense() + itemInfo.getClothingDefense()).ToString();
        }
        else
        {
            // Unequips all slots.
            for (int i = 0; i < 12; i++)
            {
                GameObject slot = clothingItemSlots.transform.GetChild(i).gameObject;
                print(slot.name);
                slot.transform.GetChild(2).gameObject.SetActive(false);
            }

            // Equips the slot that is being equipped.
            clothingItemSlots.transform.GetChild(currentSlotIndex).GetChild(2).gameObject.SetActive(true);

            // Disables playerDefenseAfterEquipLabel and arrow for equipped slot.
            itemInfoUI.transform.GetChild(5).gameObject.SetActive(false);
            itemInfoUI.transform.GetChild(6).gameObject.SetActive(false);
            itemInfoUI.transform.GetChild(7).gameObject.SetActive(false);

            // Resets defense of player to base defense then adds the defense from the clothing.
            player.GetComponent<Stats>().resetDefense();
            player.GetComponent<Stats>().setDefense(itemInfo.getClothingDefense());

            playerDefenseLabel.text = player.GetComponent<Stats>().getDefense().ToString();
        }
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
                GameObject droppedItem = Instantiate(itemInfo.getItemGameObject());
                droppedItem.SetActive(true);
                droppedItem.transform.position = new Vector3(randomX, droppedItem.transform.position.y, randomZ);

                // Sets the item quantity to 1 since only one item is dropped.
                droppedItem.GetComponent<IItem>().setItemQuantity(1);
            }
        }
    }
}
