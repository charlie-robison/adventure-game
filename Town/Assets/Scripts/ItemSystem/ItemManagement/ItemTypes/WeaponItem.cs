using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour, IItem
{
    public GameObject itemObject;
    public GameObject weaponGameObject;

    public string itemName;
    public string itemDesc;
    public float itemPrice;
    public int itemQuantity;
    public GameObject itemDisplay;
    public GameObject itemGameObject;

    public int weaponPower;
    public float weaponFrequency;

    void Start()
    {
        itemGameObject = Instantiate(itemObject, transform.position, transform.rotation);
        itemGameObject.SetActive(false);
    }

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

    public void setItemQuantity(int newQuantity)
    {
        this.itemQuantity = newQuantity;
    }

    public GameObject getItemDisplay()
    {
        return itemDisplay;
    }

    public GameObject getItemGameObject()
    {
        itemGameObject.SetActive(true);
        return itemGameObject;
    }

    public int getWeaponPower()
    {
        return weaponPower;
    }

    public float getWeaponFrequency()
    {
        return weaponFrequency;
    }

    public GameObject getWeaponGameObject()
    {
        return weaponGameObject;
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

