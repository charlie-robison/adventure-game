using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponInventory weaponInventory;

    // Holds all items for the player.
    // Items are stored with their name as the key, and their quantity as the value.
    private Dictionary<string, int> weaponItems;
    // private Dictionary<string, int> healingItems;
    private Dictionary<string, ItemProperties> allItems;

    void Start()
    {
        weaponItems = new Dictionary<string, int>();
        allItems = new Dictionary<string, ItemProperties>();
        // healingItems = new Dictionary<string, int>();
    }

    public Dictionary<string, int> getWeaponItems()
    {
        return weaponItems;
    }

    public Dictionary<string, ItemProperties> getAllItems()
    {
        return allItems;
    }

    public void addWeaponItem(ItemProperties item)
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

    public void removeWeaponItem(ItemProperties item)
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
}
