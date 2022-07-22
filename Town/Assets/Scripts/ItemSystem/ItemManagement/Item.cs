using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    /*
    public GameObject itemObject;
    public GameObject itemDisplay;
    public string itemName;
    public string itemDesc;
    public int itemQuantity;
    public float itemPrice;
    public int itemPower;
    public int itemHealingPower;
    public ItemTypes itemType;

    private ItemProperties item;

    void Start()
    {
        // Checks the item type and creates the correct item accordingly.
        if (itemType == ItemTypes.WeaponItem)
        {
            item = new WeaponItemProperties(itemPower, itemName, itemDesc, itemQuantity, itemPrice, itemDisplay);
        }
        else if (itemType == ItemTypes.HealingItem)
        {
            item = new HealingItemProperties(itemHealingPower, itemName, itemDesc, itemQuantity, itemPrice, itemDisplay);
        }
        else
        {
            item = new ItemProperties(itemName, itemDesc, itemQuantity, itemPrice, itemObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            // Checks the type of item.
            if (itemType == ItemTypes.WeaponItem)
            {
                Destroy(itemObject);

                // Adds the item to the player inventory.
                col.gameObject.GetComponent<PlayerInventory>().addWeaponItem(item);
                // print(item.getItemName() + ": " + col.gameObject.GetComponent<PlayerInventory>().getWeaponItems()[item.getItemName()]);
            }
            else if (itemType == ItemTypes.HealingItem)
            {
                Destroy(itemObject);

                // Adds the item to the player inventory.
                col.gameObject.GetComponent<PlayerInventory>().addHealingItem(item);
                print(item.getItemName() + ": " + col.gameObject.GetComponent<PlayerInventory>().getHealingItems()[item.getItemName()]);
            }
        }
    } */
}
