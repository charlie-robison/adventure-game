using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public IInventory inventoryManagement;
    public GameControls controls;
    public GameObject itemSlots;
    public int numberOfSlots;

    private readonly InventoryUISelection invSelection = new InventoryUISelection();
    private Vector2 selectionDirection;
    private float selectionTimer = 0f;
    private GameObject currentSlot;
    private int currentSlotIndex = 0;

    void Awake()
    {
        controls = new GameControls();
        controls.Gameplay.Movement.performed += ctx => selectionDirection = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => selectionDirection = Vector2.zero;
    }

    void Start()
    {
        inventoryManagement = itemSlots.GetComponent<IInventory>();
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

            // Fills the slots with the items for the correct inventory.
            inventoryManagement.fillSlots();
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
            if ((Mathf.Abs(selectionDirection.x) > 0.1f || Mathf.Abs(selectionDirection.y) > 0.1f) && inventoryManagement.getItemCount() > 0)
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

    void enableItemInfoSection()
    {
        // Checks if there are any items in the inventory.
        if (inventoryManagement.getItemCount() <= 0)
        {
            // Sets the item info section to not active.
            GameObject itemInfoUI = itemSlots.transform.GetChild(14).gameObject;
            itemInfoUI.SetActive(false);
        }
    }

    void Update()
    {
        // Updates the current slot from user input.
        updateCurrentSlot();

        // enables item info section if there are any items.
        enableItemInfoSection();
    }
}
