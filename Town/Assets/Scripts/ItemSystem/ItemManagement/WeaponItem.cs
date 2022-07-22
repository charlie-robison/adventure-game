using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour, IItem
{
    public GameObject itemObject;

    public string itemName;
    public string itemDesc;
    public float itemPrice;
    public int itemQuantity;
    public GameObject itemDisplay;

    public int weaponPower;
    public float weaponFrequency;

    public string getItemName()
    {
        return itemName;
    }

    public string getItemDesc()
    {
        return itemDesc;
    }

    public float getItemPrice()
    {
        return itemPrice;
    }

    public int getItemQuantity()
    {
        return itemQuantity;
    }

    public GameObject getItemDisplay()
    {
        return itemDisplay;
    }

    public int getWeaponPower()
    {
        return weaponPower;
    }

    public float getWeaponFrequency()
    {
        return weaponFrequency;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(itemObject);

            // Adds the item to the player inventory.
            col.gameObject.GetComponent<PlayerInventory>().addWeaponItem(this);
        }    
    }
}

