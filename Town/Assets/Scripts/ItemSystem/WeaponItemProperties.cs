using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemProperties : ItemProperties
{
    public int weaponPower;

    public WeaponItemProperties(string itemName, string itemDesc, int itemQuantity, float itemPrice, GameObject itemObject)
        : base(itemName, itemDesc, itemQuantity, itemPrice, itemObject)
    { }

    public int getWeaponPower()
    {
        return weaponPower;
    }
}
