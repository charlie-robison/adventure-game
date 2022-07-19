using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject itemObject;
    public string itemName;
    public int itemQuantity;
    public float itemPrice;

    private ItemProperties item;
    private ItemTypes itemType;

    void Start()
    {
        item = new ItemProperties(itemName, itemQuantity, itemPrice);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (itemType == ItemTypes.WeaponItem)
            {
                Destroy(itemObject);
                col.gameObject.GetComponent<PlayerInventory>().addWeaponItem(item);
                print(item.getItemName() + ": " + col.gameObject.GetComponent<PlayerInventory>().getWeaponItems()[item.getItemName()]);
            }
        }
    }
}
