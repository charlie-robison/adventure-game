using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItemProperties : ItemProperties
{
    public int healingPower;

    public HealingItemProperties(int healingPower, string itemName, string itemDesc, int itemQuantity, float itemPrice, GameObject itemObject)
        : base(itemName, itemDesc, itemQuantity, itemPrice, itemObject)
    {
        this.healingPower = healingPower;
    }

    public int getHealingPower()
    {
        return healingPower;
    }
}

