using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public string getItemName();
    public string getItemDesc();
    public float getItemPrice();
    public int getItemQuantity();
    public void setItemQuantity(int newQuantity);
    public GameObject getItemDisplay();
    public GameObject getItemGameObject();
}
