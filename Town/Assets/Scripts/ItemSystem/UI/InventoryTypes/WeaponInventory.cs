using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** Implements a Weapon Inventory. */
public class WeaponInventory : MonoBehaviour, IInventory
{
    public GameObject player;
    public GameObject weaponHolster;
    public GameObject weaponItemSlots;

    private Dictionary<string, int> weaponItems;
    private Dictionary<string, IItem> allItems;
    private string[] itemList;

    private void Start()
    {
        weaponItems = player.GetComponent<PlayerInventory>().getWeaponItems();
        allItems = player.GetComponent<PlayerInventory>().getAllItems();
    }

    /** Fills the inventory slots. */
    public void fillSlots()
    {
        weaponItems = player.GetComponent<PlayerInventory>().getWeaponItems();
        allItems = player.GetComponent<PlayerInventory>().getAllItems();

        int slotIndex = 0;

        // Checks if the player has any weapons.
        if (weaponItems != null && weaponItems.Count > 0)
        {
            itemList = new string[weaponItems.Count];

            // Iterates through the entire dictionary.
            foreach (KeyValuePair<string, int> item in weaponItems)
            {
                // Gets the properties for the item as well as the current slot and the labels for that slot.
                WeaponItem itemProperties = (WeaponItem)allItems[item.Key];
                GameObject slot = weaponItemSlots.transform.GetChild(slotIndex).gameObject;
                TMP_Text powerLabel = slot.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
                TMP_Text quantityLabel = slot.transform.GetChild(5).gameObject.GetComponent<TMP_Text>();
                GameObject itemDisplay = slot.transform.GetChild(6).gameObject;

                // Enables enabled slot UI image.
                slot.transform.GetChild(0).gameObject.SetActive(false);
                slot.transform.GetChild(1).gameObject.SetActive(true);
                slot.transform.GetChild(3).gameObject.SetActive(false);
                slot.transform.GetChild(4).gameObject.SetActive(true);
                slot.transform.GetChild(5).gameObject.SetActive(true);

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
                GameObject newItemDisplay = Instantiate(itemProperties.getItemDisplay());
                newItemDisplay.transform.position = itemDisplay.transform.position;
                newItemDisplay.transform.parent = itemDisplay.transform;

                itemList[slotIndex] = item.Key;

                slotIndex++;
            }
        }
    }

    /** Presents the selected item's info in the item info UI area. */
    public void presentSelectedItemInfo(int currentSlotIndex)
    {
        WeaponItem itemInfo = (WeaponItem)allItems[itemList[currentSlotIndex]];
        GameObject itemInfoUI = weaponItemSlots.transform.GetChild(14).gameObject;

        TMP_Text itemNameLabel = itemInfoUI.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        TMP_Text itemDescLabel = itemInfoUI.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
        TMP_Text itemPowerLabel = itemInfoUI.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();

        // Displays item information.
        itemNameLabel.text = itemInfo.getItemName();
        itemDescLabel.text = itemInfo.getItemDesc();
        itemPowerLabel.text = itemInfo.getWeaponPower().ToString();

        GameObject itemDisplay = weaponItemSlots.transform.GetChild(13).gameObject;

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

        if (weaponItems != null)
        {
            numberOfItems = weaponItems.Count;
        }

        return numberOfItems;
    }

    /** Gets the inventory's items. */
    public Dictionary<string, int> getItems()
    {
        return weaponItems;
    }

    /** Uses the item selected. */
    public void useItem(int currentSlotIndex)
    {
        // Gets the item's info.
        WeaponItem itemInfo = (WeaponItem)allItems[itemList[currentSlotIndex]];

        // Unequips all weapons then equips the selected weapon.
        unEquipAllWeapons();
        equipSelectedWeapon(currentSlotIndex);

        // Destroys all children in weaponHolster.
        foreach (Transform child in weaponHolster.transform)
        {
            Destroy(child.gameObject);
        }

        // Adds the weapon to the player's weapon holster.
        GameObject newWeapon = Instantiate(itemInfo.getWeaponGameObject());
        newWeapon.transform.position = weaponHolster.transform.position;
        newWeapon.transform.parent = weaponHolster.transform;
        newWeapon.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);

        /* Modify player's power. */
        /* Modify player's attack frequency. */

        print("Equipped " + itemInfo.getItemName());
    }

    /** Drops the item selected. */
    public void dropItem(int currentSlotIndex, int numberDropped)
    {
        WeaponItem itemInfo = (WeaponItem)allItems[itemList[currentSlotIndex]];

        // Checks if the number dropped is less than or equal to the amount of the item possessed.
        if (numberDropped <= weaponItems[itemInfo.getItemName()])
        {
            for (int i = 0; i < numberDropped; i++)
            {
                player.GetComponent<PlayerInventory>().removeWeaponItem(itemInfo);

                // Sets random positions for the dropped item around the player.
                float randomX = Random.Range(player.transform.position.x - 2f, player.transform.position.x + 2f);
                float randomZ = Random.Range(player.transform.position.z - 2f, player.transform.position.z + 2f);

                // Drops items around the player.
                GameObject droppedItem = Instantiate(itemInfo.getItemGameObject());
                droppedItem.transform.position = new Vector3(randomX, 0f, randomZ);
            }
        }
    }

    /** Unequips all weapons. */
    private void unEquipAllWeapons()
    {
        int slotIndex = 0;

        // Iterates through each weapon in the inventory and unequips it.
        foreach (KeyValuePair<string, int> item in weaponItems)
        {
            // Gets the weapon and sets its equipped value to false.
            WeaponItem itemInfo = (WeaponItem)allItems[item.Key];
            itemInfo.setIsEquipped(false);

            // Disables equip slot UI.
            GameObject slot = weaponItemSlots.transform.GetChild(slotIndex).gameObject;
            slot.transform.GetChild(3).gameObject.SetActive(false);

            slotIndex++;
        }
    }

    /** Equips the selected weapon. */
    private void equipSelectedWeapon(int currentSlotIndex)
    {
        // Gets the weapon and sets its equipped value to true.
        WeaponItem itemInfo = (WeaponItem)allItems[itemList[currentSlotIndex]];
        itemInfo.setIsEquipped(true);

        // Enables the equip slot UI.
        GameObject slot = weaponItemSlots.transform.GetChild(currentSlotIndex).gameObject;
        slot.transform.GetChild(3).gameObject.SetActive(true);

    }
}
