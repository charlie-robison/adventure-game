using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Handles the UI for an Inventory, such as setting up the inventory slots, updating the current slot,
    and enabling/ disabling the item info section.*/
public class InventoryUI : MonoBehaviour
{
    public IInventory inventoryManagement;
    public GameControls controls;
    public GameObject itemSlots;
    public int numberOfSlots;

    private InventorySelectionUpdater invSelectionUpdater;
    private Vector2 selectionDirection;
    private float selectionTimer = 0f;
    private GameObject currentSlot;
    private int currentSlotIndex = -1;
    private bool useSelectedItem = false;
    private bool dropSelectedItem = false;
    private int previousItemCount;

    private void Awake()
    {
        controls = new GameControls();
        controls.Gameplay.ItemSelect.performed += ctx => selectionDirection = ctx.ReadValue<Vector2>();
        controls.Gameplay.ItemSelect.canceled += ctx => selectionDirection = Vector2.zero;
        controls.Gameplay.ItemPress.performed += ctx => useSelectedItem = true;
        controls.Gameplay.ItemPress.canceled += ctx => useSelectedItem = false;
        controls.Gameplay.ItemDropPress.performed += ctx => dropSelectedItem = true;
        controls.Gameplay.ItemDropPress.canceled += ctx => dropSelectedItem = false;
    }

    private void Start()
    {
        invSelectionUpdater = new InventorySelectionUpdater();
        inventoryManagement = itemSlots.GetComponent<IInventory>();
        previousItemCount = inventoryManagement.getItemCount();
        setUpSlots();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    /** Sets up the initial state of all the slots and then fills them up. */
    private void setUpSlots()
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
            slot.transform.GetChild(5).gameObject.SetActive(false);

            if (slot.transform.GetChild(6).gameObject.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(6).gameObject.transform.GetChild(0).gameObject);
            }

            // Fills the slots with the items for the correct inventory.
            inventoryManagement.fillSlots();
        }
    }

    /** Unselects all slots. */
    private void unselectSlots()
    {
        // Iterates through each slot and unselects it.
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = itemSlots.transform.GetChild(i).gameObject;
            slot.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    /** Updates the current slot index and assigns the appropriate gameObject to currentSlot. */
    private void updateCurrentSlot()
    {
        // Checks if the player can select the next or previous slot.
        if (Time.time > selectionTimer)
        {
            // Checks if there was a slot change and if there is at least one item in the inventory.
            if ((Mathf.Abs(selectionDirection.x) > 0.1f || Mathf.Abs(selectionDirection.y) > 0.1f) && inventoryManagement.getItemCount() > 0)
            {
                int newSlotIndex = invSelectionUpdater.getCurrentSlot(itemSlots, selectionDirection);

                // Checks if the currentSlotIndex changed.
                if (currentSlotIndex != newSlotIndex)
                {
                    // Sets the next slot index and sets currentSlot to the correct slot gameObject.
                    currentSlotIndex = newSlotIndex;
                    currentSlot = itemSlots.transform.GetChild(currentSlotIndex).gameObject;
                    selectionTimer = Time.time + 0.15f;

                    // Unselects all slots.
                    unselectSlots();

                    // Sets the item info section to active.
                    GameObject bigItemDisplay = itemSlots.transform.GetChild(13).gameObject;
                    bigItemDisplay.SetActive(false);
                    GameObject itemInfoUI = itemSlots.transform.GetChild(14).gameObject;
                    itemInfoUI.SetActive(true);

                    // Displays the selected item info on the item info area.
                    inventoryManagement.presentSelectedItemInfo(currentSlotIndex);
                }
            }
        }

        // Checks if the currentSlot is an actual slot.
        if (currentSlot != null)
        {
            // Check if there is an item in the slot.
            if (currentSlot.transform.GetChild(1).gameObject.activeSelf)
            {
                // Selects the current slot.
                currentSlot.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                // Unselects the current slot.
                currentSlot.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    /** Enables the itemInfo section when applicable. */
    private void enableItemInfoSection()
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
            // Uses the item.
            inventoryManagement.useItem(currentSlotIndex);
        }
    }

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

        if (previousItemCount > inventoryManagement.getItemCount())
        {
            // Sets the currentSlotIndex if the number of types of items is less than before.
            if (inventoryManagement.getItemCount() <= 0)
            {
                currentSlotIndex = -1;
            }
            else if (inventoryManagement.getItemCount() == 1)
            {
                currentSlotIndex = 0;
                currentSlot = itemSlots.transform.GetChild(currentSlotIndex).gameObject;
            }
            else if (currentSlotIndex == 0)
            {
                currentSlotIndex += 1;
                currentSlot = itemSlots.transform.GetChild(currentSlotIndex).gameObject;
            }
            else
            {
                currentSlotIndex -= 1;
                currentSlot = itemSlots.transform.GetChild(currentSlotIndex).gameObject;
            }

            previousItemCount = inventoryManagement.getItemCount();

            // Unselects all items.
            unselectSlots();

            // Resets all inventory slots.
            setUpSlots();
        }
    }

    private void Update()
    {
        // Updates the current slot from user input.
        updateCurrentSlot();

        // Enables item info section if there are any items.
        enableItemInfoSection();

        // Checks if selected item was pressed and uses the item.
        useCurrentItem();

        dropCurrentItem();
    }
}
