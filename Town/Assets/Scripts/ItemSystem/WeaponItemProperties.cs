using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemProperties : ItemProperties
{
    private int weaponPower;

    public WeaponItemProperties(int weaponPower, string itemName, int itemQuantity, float itemPrice, GameObject itemObject)
        : base(itemName, itemQuantity, itemPrice, itemObject)
    {
        this.weaponPower = weaponPower;
    }

    public int getWeaponPower()
    {
        return weaponPower;
    }
}
