using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponInventory weaponInventory;
    public HealingInventory healingInventory;

    // Holds all items for the player.
    // Items are stored with their name as the key, and their quantity as the value.
    private Dictionary<string, int> weaponItems;
    private Dictionary<string, int> healingItems;
    private Dictionary<string, IItem> allItems;

    void Start()
    {
        weaponItems = new Dictionary<string, int>();
        healingItems = new Dictionary<string, int>();
        allItems = new Dictionary<string, IItem>();
    }

    public Dictionary<string, int> getWeaponItems()
    {
        return weaponItems;
    }

    public Dictionary<string, int> getHealingItems()
    {
        return healingItems;
    }

    public Dictionary<string, IItem> getAllItems()
    {
        return allItems;
    }

    public void addWeaponItem(IItem item)
    {
        // Checks if this item is already a key.
        if (!weaponItems.ContainsKey(item.getItemName()))
        {
            // Adds the item and the quantity that it has.
            weaponItems.Add(item.getItemName(), item.getItemQuantity());
            allItems.Add(item.getItemName(), item);
        }
        else
        {
            // Adds the item quantity to the existing key.
            weaponItems[item.getItemName()] += item.getItemQuantity();
        }

        // Updates inventory UI.
        weaponInventory.fillSlots();
    }

    public void addHealingItem(IItem item)
    {
        // Checks if this item is already a key.
        if (!healingItems.ContainsKey(item.getItemName()))
        {
            // Adds the item and the quantity that it has.
            healingItems.Add(item.getItemName(), item.getItemQuantity());
            allItems.Add(item.getItemName(), item);
        }
        else
        {
            // Adds the item quantity to the existing key.
            healingItems[item.getItemName()] += item.getItemQuantity();
        }

        // Updates inventory UI.
        healingInventory.fillSlots();
    }

    public void removeWeaponItem(IItem item)
    {
        // Checks if the player possesses the item.
        if (weaponItems.ContainsKey(item.getItemName()) && weaponItems[item.getItemName()] > 0)
        {
            // Removes 1 from the item.
            weaponItems[item.getItemName()]--;

            // Updates inventory UI.
            weaponInventory.fillSlots();
        }
    }

    public void removeHealingItem(IItem item)
    {
        // Checks if the player possesses the item.
        if (healingItems.ContainsKey(item.getItemName()) && healingItems[item.getItemName()] > 0)
        {
            // Removes 1 from the item.
            healingItems[item.getItemName()]--;

            // Updates inventory UI.
            healingInventory.fillSlots();
        }
    }
}
