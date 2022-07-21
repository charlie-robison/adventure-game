using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUISelection : MonoBehaviour
{
    private int currentSlotIndex;
    private Vector2 selectionDirection;

    public InventoryUISelection()
    {
        currentSlotIndex = 0;
        selectionDirection = Vector2.zero;
    }

    // Sets the selectionDirection field.
    public void setSelectionDirection(Vector2 newSelectionDirection)
    {
        selectionDirection = newSelectionDirection;
    }

    // Unselects all slots for the given slots.
    public void unselectSlots(GameObject itemSlots)
    {
        // Unselects all the slots.
        for (int i = 0; i < 12; i++)
        {
            GameObject slot = itemSlots.transform.GetChild(i).gameObject;
            slot.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    // Updates the current slot from user input.
    public int getCurrentSlot(GameObject itemSlots)
    {
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
