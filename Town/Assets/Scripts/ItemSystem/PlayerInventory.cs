using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Holds all items for the player.
    // Items are stored with their name as the key, and their quantity as the value.
    private Dictionary<string, int> weaponItems;
    // private Dictionary<string, int> healingItems;

    void Start()
    {
        weaponItems = new Dictionary<string, int>();
        // healingItems = new Dictionary<string, int>();
    }

    public Dictionary<string, int> getWeaponItems()
    {
        return weaponItems;
    }

    public void addWeaponItem(ItemProperties item)
    {
        // Checks if this item is already a key.
        if (!weaponItems.ContainsKey(item.getItemName()))
        {
            // Adds the item and the quantity that it has.
            weaponItems.Add(item.getItemName(), item.getItemQuantity());
        }
        else
        {
            // Adds the item quantity to the existing key.
            weaponItems[item.getItemName()] += item.getItemQuantity();
        }
    }

    public void removeWeaponItem(ItemProperties item)
    {
        // Checks if the player possesses the item.
        if (weaponItems.ContainsKey(item.getItemName()) && weaponItems[item.getItemName()] > 0)
        {
            // Removes 1 from the item.
            weaponItems[item.getItemName()]--;
        }
    }
}
