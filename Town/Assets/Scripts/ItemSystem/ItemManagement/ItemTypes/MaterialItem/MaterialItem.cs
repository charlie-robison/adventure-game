using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialItem : MonoBehaviour, IItem
{
    public GameObject itemObject;

    public string itemName;
    public string itemDesc;
    public float itemPrice;
    public int itemQuantity;
    public GameObject itemDisplay;
    public GameObject itemGameObject;

    public MaterialTypes materialType;
    public int materialTypePower;

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

    public GameObject getItemGameObject()
    {
        return itemGameObject;
    }

    public MaterialTypes getMaterialType()
    {
        return materialType;
    }

    public int getMaterialTypePower()
    {
        return materialTypePower;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(itemObject);

            // Adds the item to the player inventory.
            col.gameObject.GetComponent<PlayerInventory>().addMaterialItem(this);
        }
    }
}

