using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Updates the current slot index given the item slots and the user input. */
public class InventorySelectionUpdater : MonoBehaviour
{
    private int currentSlotIndex;
    private Vector2 selectionDirection;

    public InventorySelectionUpdater()
    {
        currentSlotIndex = 0;
        selectionDirection = Vector2.zero;
    }

    /** Returns the current slot's index which is updated based on user input. */
    public int getCurrentSlot(GameObject itemSlots, Vector2 newSelectionDirection)
    {
        // Updates the selection direction.
        selectionDirection = newSelectionDirection;

        // Selects the correct slot based on input.
        if (selectionDirection.x == 1)
        {
            if (currentSlotIndex < 11)
            {
                GameObject nextSlot = itemSlots.transform.GetChild(currentSlotIndex + 1).gameObject;

                // Checks if the slot is active in order to move onto it.
                if (nextSlot.transform.GetChild(1).gameObject.activeSelf)
                {
                    currentSlotIndex += 1;
                }
            }
        }
        else if (selectionDirection.x == -1)
        {
            if (currentSlotIndex > 0)
            {
                GameObject prevSlot = itemSlots.transform.GetChild(currentSlotIndex - 1).gameObject;

                // Checks if the slot is active in order to move back to it.
                if (prevSlot.transform.GetChild(1).gameObject.activeSelf)
                {
                    currentSlotIndex -= 1;
                }
            }
        }
        else if (selectionDirection.y == 1)
        {
            if (currentSlotIndex > 3)
            {
                GameObject rowAbove = itemSlots.transform.GetChild(currentSlotIndex - 4).gameObject;

                // Checks if the slot is active in order to move back to it.
                if (rowAbove.transform.GetChild(1).gameObject.activeSelf)
                {
                    currentSlotIndex -= 4;
                }
            }
        }
        else if (selectionDirection.y == -1)
        {
            if (currentSlotIndex < 8)
            {
                GameObject rowBelow = itemSlots.transform.GetChild(currentSlotIndex + 4).gameObject;

                // Checks if the slot is active in order to move onto it.
                if (rowBelow.transform.GetChild(1).gameObject.activeSelf)
                {
                    currentSlotIndex += 4;
                }

            }
        }

        return currentSlotIndex;
    }
}
