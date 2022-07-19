using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
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
        if (!weaponItems.ContainsKey(item.getItemName()))
        {
            weaponItems.Add(item.getItemName(), item.getItemQuantity());
        }
        else
        {
            weaponItems[item.getItemName()] += item.getItemQuantity();
        }
    }

    public void removeWeaponItem(ItemProperties item)
    {
        if (weaponItems.ContainsKey(item.getItemName()) && weaponItems[item.getItemName()] > 0)
        {
            weaponItems[item.getItemName()]--;
        }
    }
}
