using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    // Fills the inventory slots.
    public void fillSlots();

    // Presents the selected item's info in the item info UI area.
    public void presentSelectedItemInfo(int currentSlotIndex);

    // Gets the number of items in the inventory.
    public int getItemCount();

    public void useItem(int currentSlotIndex);
}
