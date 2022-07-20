using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperties : MonoBehaviour
{
    private readonly string itemName;
    private readonly int itemQuantity;
    private readonly float itemPrice;
    private readonly GameObject itemObject;

    public ItemProperties(string itemName, int itemQuantity, float itemPrice, GameObject itemObject)
    {
        this.itemName = itemName;
        this.itemQuantity = itemQuantity;
        this.itemPrice = itemPrice;
        this.itemObject = itemObject;
    }

    public string getItemName()
    {
        return itemName;
    }

    public int getItemQuantity()
    {
        return itemQuantity;
    }

    public float getItemPrice()
    {
        return itemPrice;
    }

    public GameObject getGameObject()
    {
        return itemObject;
    }
}
