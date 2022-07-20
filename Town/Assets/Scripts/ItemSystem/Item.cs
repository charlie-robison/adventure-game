using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject itemObject;
    public string itemName;
    public int itemQuantity;
    public float itemPrice;
    public int itemPower;

    private ItemProperties item;
    private ItemTypes itemType;

    void Start()
    {
        // Checks the item type and creates the correct item accordingly.
        if (itemType == ItemTypes.WeaponItem)
        {
            item = new WeaponItemProperties(itemPower, itemName, itemQuantity, itemPrice, itemObject);
        }
        else
        {
            item = new ItemProperties(itemName, itemQuantity, itemPrice, itemObject);
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
                print(item.getItemName() + ": " + col.gameObject.GetComponent<PlayerInventory>().getWeaponItems()[item.getItemName()]);
            }
        }
    }
}
