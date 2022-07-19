using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperties : MonoBehaviour
{
    private readonly string itemName;
    private readonly int itemQuantity;
    private readonly float itemPrice;

    public ItemProperties(string itemName, int itemQuantity, float itemPrice)
    {
        this.itemName = itemName;
        this.itemQuantity = itemQuantity;
        this.itemPrice = itemPrice;
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
}
