using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public IInventory inventoryManagement;
    public GameControls controls;
    public GameObject player;
    public GameObject itemSlots;
    public int numberOfSlots;

    private Dictionary<string, int> items;
    private Dictionary<string, ItemProperties> allItems;
    private string[] itemList;
    private InventoryUISelection invSelection = new InventoryUISelection();

    private Vector2 selectionDirection;
    private float selectionTimer = 0f;
    private GameObject currentSlot;
    private int currentSlotIndex = 0;

    void Start()
    {
        setUpSlots();
    }

    void setUpSlots()
    {
        // Initializes all the item slots by enabling the disable slot and disabling the enable and selection slots.
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = itemSlots.transform.GetChild(i).gameObject;
            slot.transform.GetChild(0).gameObject.SetActive(true);
            slot.transform.GetChild(1).gameObject.SetActive(false);
            slot.transform.GetChild(2).gameObject.SetActive(false);
            slot.transform.GetChild(3).gameObject.SetActive(false);
            slot.transform.GetChild(4).gameObject.SetActive(false);

            // Fills the slots from the weaponItems dictionary.
            inventoryManagement.fillSlots(items);
        }

        // Sets the current slot to the first slot.
        currentSlot = itemSlots.transform.GetChild(currentSlotIndex).gameObject;
    }

    void updateCurrentSlot()
    {
        // Checks if the player can select the next or previous slot.
        if (Time.time > selectionTimer)
        {
            // Checks if there was a slot change and if there is at least one item in the inventory.
            if ((Mathf.Abs(selectionDirection.x) > 0.1f || Mathf.Abs(selectionDirection.y) > 0.1f) && items.Count > 0)
            {
                int newSlotIndex = invSelection.getCurrentSlot(itemSlots, selectionDirection);

                // Checks if the currentSlotIndex changed.
                if (currentSlotIndex != newSlotIndex)
                {
                    // Sets the new current slot index.
                    currentSlotIndex = newSlotIndex;

                    // Sets currentSlot to the correct slot gameObject.
                    currentSlot = itemSlots.transform.GetChild(currentSlotIndex).gameObject;

                    // Resets timer.
                    selectionTimer = Time.time + 0.15f;

                    // Unselects all slots.
                    invSelection.unselectSlots(itemSlots);

                    // Sets the item info section to active.
                    GameObject itemInfoUI = itemSlots.transform.GetChild(14).gameObject;
                    itemInfoUI.SetActive(true);

                    // Displays the selected item info on the item info area.
                    inventoryManagement.presentSelectedItemInfo(items);
                }
            }
        }
    }

    void Update()
    {
        // Updates the current slot from user input.
        updateCurrentSlot();
    }
}
