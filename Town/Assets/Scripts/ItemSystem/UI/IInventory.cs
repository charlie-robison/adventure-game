using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public void fillSlots();
    public void presentSelectedItemInfo(int currentSlotIndex);
    public int getItemCount();
}
