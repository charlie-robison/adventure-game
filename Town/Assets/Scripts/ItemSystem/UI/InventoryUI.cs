using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Handles the UI for an Inventory, such as setting up the inventory slots, updating the current slot,
    and enabling/ disabling the item info section. */
public class InventoryUI : MonoBehaviour
{
    public IInventory inventoryManagement;
    public GameControls controls;
    public GameObject itemSlots;
    public int numberOfSlots;

    private GameObject currentSlot;
    private int currentSlotIndex = -1;
    private bool useSelectedItem = false;
    private bool dropSelectedItem = false;
    private int previousItemCount;

    private void Awake()
    {
        controls = new GameControls();
        controls.UI.ItemPress.performed += ctx => useSelectedItem = true;
        controls.UI.ItemPress.canceled += ctx => useSelectedItem = false;
        controls.UI.ItemDropPress.performed += ctx => dropSelectedItem = true;
        controls.UI.ItemDropPress.canceled += ctx => dropSelectedItem = false;
    }

    private void Start()
    {
        inventoryManagement = itemSlots.GetComponent<IInventory>();
        previousItemCount = inventoryManagement.getItemCount();
        setUpSlots();
    }

    void OnEnable()
    {
        controls.UI.Enable();
    }

    void OnDisable()
    {
        controls.UI.Disable();
    }

    /** Sets up the initial state of all the slots and then fills them up. */
    private void setUpSlots()
    {
        // Initializes all the item slots by enabling the disable slot and disabling the enable and selection slots.
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = itemSlots.transform.GetChild(i).gameObject;
            int slotChildren = slot.transform.childCount;

            slot.transform.GetChild(0).gameObject.SetActive(true);

            for (int j = 1; j < slotChildren - 2; j++)
            {
                slot.transform.GetChild(j).gameObject.SetActive(false);
            }

            if (slot.transform.GetChild(slotChildren - 2).gameObject.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(slotChildren - 2).gameObject.transform.GetChild(0).gameObject);
            }
        }

        // Fills the slots with the items for the correct inventory.
        inventoryManagement.fillSlots();
    }

    /** Unselects all slots. */
    private void unselectSlots()
    {
        // Iterates through each slot and unselects it.
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = itemSlots.transform.GetChild(i).gameObject;
            slot.transform.GetChild(3).gameObject.SetActive(false);
        }
    }
    
    /** Updates the current slot index and assigns the appropriate gameObject to currentSlot. */
    private void updateCurrentSlot()
    {
        // Checks if the currentSlot is an actual slot.
        if (currentSlot != null)
        {
            // Check if there is an item in the slot.
            if (currentSlot.transform.GetChild(1).gameObject.activeSelf)
            {
                // Selects the current slot.
                currentSlot.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                // Unselects the current slot.
                currentSlot.transform.GetChild(3).gameObject.SetActive(false);
            }
        }

        // Checks if there is only one item in the inventory.
        if (inventoryManagement.getItemCount() == 1)
        {
            // Sets the current selected slot to the 1st slot.
            currentSlotIndex = 0;
            currentSlot = itemSlots.transform.GetChild(currentSlotIndex).gameObject;

            // Enables the item info section for the current item.
            enableItemInfoSection();
        }
    }

    /** Enables the itemInfo section for the current item. */
    private void enableItemInfoSection()
    {
        // Sets the item info section to active.
        GameObject bigItemDisplay = itemSlots.transform.GetChild(13).gameObject;
        bigItemDisplay.SetActive(true);
        GameObject itemInfoUI = itemSlots.transform.GetChild(14).gameObject;
        itemInfoUI.SetActive(true);

        // Displays the selected item info on the item info area.
        inventoryManagement.presentSelectedItemInfo(currentSlotIndex);
    }

    /** Disables the itemInfo section when applicable. */
    private void disableItemInfoSection()
    {
        // Checks if there are any items in the inventory.
        if (inventoryManagement.getItemCount() <= 0 || currentSlotIndex == -1)
        {
            // Sets the item info section to not active.
            GameObject bigItemDisplay = itemSlots.transform.GetChild(13).gameObject;
            GameObject itemInfoUI = itemSlots.transform.GetChild(14).gameObject;
            bigItemDisplay.SetActive(false);
            itemInfoUI.SetActive(false);
        }
    }

    /** Checks if selected item was pressed and uses the item. */
    private void useCurrentItem()
    {
        // Checks if the selected item can be used.
        if (useSelectedItem && inventoryManagement.getItemCount() > 0 && currentSlotIndex != -1)
        {
            useSelectedItem = false;

            // Uses the item.
            inventoryManagement.useItem(currentSlotIndex);
        }
    }

    /** Checks if the selected item was pressed to delete. */
    private void dropCurrentItem()
    {
        // Checks if selected item can be dropped.
        if (dropSelectedItem && inventoryManagement.getItemCount() > 0 && currentSlotIndex != -1)
        {
            previousItemCount = inventoryManagement.getItemCount();
            dropSelectedItem = false;

            // Drops the item.
            inventoryManagement.dropItem(currentSlotIndex, 1);
        }

        // Checks if there is less items then there was before.
        if (previousItemCount > inventoryManagement.getItemCount())
        {
            // Sets the currentSlotIndex to -1 if there are no items.
            if (inventoryManagement.getItemCount() <= 0)
            {
                currentSlotIndex = -1;
            }
            // Sets the currentSlotIndex to 0 if there is 1 item.
            else if (inventoryManagement.getItemCount() == 1)
            {
                currentSlotIndex = 0;
            }

            // Decrements currentSlotIndex by 1 if the currentSlotIndex exceeds the item count. (Slot becomes invalid).
            if (currentSlotIndex > (inventoryManagement.getItemCount() - 1))
            {
                currentSlotIndex -= 1;
            }

            previousItemCount = inventoryManagement.getItemCount();

            // Unselects all items and resets all the slots.
            unselectSlots();
            setUpSlots();

            // Checks if the currentSlotIndex is valid.
            if (currentSlotIndex != -1)
            {
                // Sets currentSlot and itemInfo for slot.
                currentSlot = itemSlots.transform.GetChild(currentSlotIndex).gameObject;

                // Enables the item info section for the current item.
                enableItemInfoSection();
            }
        }
    }

    /** Selects a slot when the slot is touched. */
    public void selectSlot(int slotNumber)
    {
        if (slotNumber < inventoryManagement.getItemCount())
        {
            // Unselects all the slots.
            unselectSlots();

            // Sets the current slot.
            currentSlotIndex = slotNumber;
            currentSlot = itemSlots.transform.GetChild(currentSlotIndex).gameObject;

            // Enables the item info section for the current item.
            enableItemInfoSection();
        }
    }

    private void Update()
    {
        // Updates the current slot from user input.
        updateCurrentSlot();

        // Disables item info section if there are any items.
        disableItemInfoSection();

        // Checks if selected item was pressed and uses the item.
        useCurrentItem();

        // Checks if the selected item was pressed to delete.
        dropCurrentItem();
    }
}
