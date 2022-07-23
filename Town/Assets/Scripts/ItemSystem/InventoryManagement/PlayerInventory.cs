using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponInventory weaponInventory;
    public MaterialInventory materialInventory;

    // Holds all items for the player.
    // Items are stored with their name as the key, and their quantity as the value.
    private Dictionary<string, int> weaponItems;
    private Dictionary<string, int> materialItems;
    private Dictionary<string, IItem> allItems;

    void Start()
    {
        weaponItems = new Dictionary<string, int>();
        materialItems = new Dictionary<string, int>();
        allItems = new Dictionary<string, IItem>();
    }

    public Dictionary<string, int> getWeaponItems()
    {
        return weaponItems;
    }

    public Dictionary<string, int> getMaterialItems()
    {
        return materialItems;
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
            print(item.getItemName());
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

    public void addMaterialItem(IItem item)
    {
        // Checks if this item is already a key.
        if (!materialItems.ContainsKey(item.getItemName()))
        {
            // Adds the item and the quantity that it has.
            materialItems.Add(item.getItemName(), item.getItemQuantity());
            allItems.Add(item.getItemName(), item);
        }
        else
        {
            // Adds the item quantity to the existing key.
            materialItems[item.getItemName()] += item.getItemQuantity();
        }

        // Updates inventory UI.
        materialInventory.fillSlots();
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

        // Removes item from inventory if there is 0 of it.
        if (weaponItems[item.getItemName()] == 0)
        {
            weaponItems.Remove(item.getItemName());
        }
    }

    public void removeMaterialItem(IItem item)
    {
        // Checks if the player possesses the item.
        if (materialItems.ContainsKey(item.getItemName()) && materialItems[item.getItemName()] > 0)
        {
            // Removes 1 from the item.
            materialItems[item.getItemName()]--;

            // Updates inventory UI.
            materialInventory.fillSlots();
        }

        // Removes item from inventory if there is 0 of it.
        if (materialItems[item.getItemName()] == 0)
        {
            materialItems.Remove(item.getItemName());
        }
    }
}
