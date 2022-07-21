using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInventoryUI : MonoBehaviour
{
    public GameControls controls;
    public GameObject player;
    public GameObject weaponItemSlots;
    public GameObject currentSlot;
    // public int currentSlotIndex = 0;

    private Dictionary<string, int> weaponItems;
    private Dictionary<string, ItemProperties> allItems;
    private string[] itemList;
    private InventoryUISelection invSelection = new InventoryUISelection();
    private Vector2 selectionDirection;
    private float selectionTimer = 0f;

    void Awake()
    {
        controls = new GameControls();
        controls.Gameplay.Movement.performed += ctx => selectionDirection = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => selectionDirection = Vector2.zero;
    }

    void Start()
    {
        // itemList = new string[weaponItems.Count];

        // Initializes all the weapon slots by enabling the disable slot and disabling the enable and selection slots.
        for (int i = 0; i < 12; i++)
        {
            GameObject slot = weaponItemSlots.transform.GetChild(i).gameObject;
            slot.transform.GetChild(0).gameObject.SetActive(true);
            slot.transform.GetChild(1).gameObject.SetActive(false);
            slot.transform.GetChild(2).gameObject.SetActive(false);
            slot.transform.GetChild(3).gameObject.SetActive(false);
            slot.transform.GetChild(4).gameObject.SetActive(false);

            // Fills the slots from the weaponItems dictionary.
            fillSlots();
        }

        currentSlot = weaponItemSlots.transform.GetChild(invSelection.getCurrentSlot(weaponItemSlots)).gameObject;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Updates the current slot from user input.
    void updateCurrentSlot()
    {
        invSelection.setSelectionDirection(selectionDirection);

        // Checks if the player can select the next or previous slot.
        if (Time.time > selectionTimer)
        {
            // Checks if there was a slot change.
            if (Mathf.Abs(selectionDirection.x) > 0.1f || Mathf.Abs(selectionDirection.y) > 0.1f)
            {
                // Sets currentSlot to the correct slot gameObject.
                currentSlot = weaponItemSlots.transform.GetChild(invSelection.getCurrentSlot(weaponItemSlots)).gameObject;

                // Resets timer.
                selectionTimer = Time.time + 0.15f;

                // Unselects all slots.
                invSelection.unselectSlots(weaponItemSlots);
                // presentSelectedItemInfo();
            }
        }
    }

    // Fills all the slots from the weapon items dictionary.
    public void fillSlots()
    {
        weaponItems = player.GetComponent<PlayerInventory>().getWeaponItems();
        allItems = player.GetComponent<PlayerInventory>().getAllItems();

        int slotIndex = 0;

        // Checks if the player has any weapons.
        if (weaponItems != null)
        {
            // Iterates through the entire dictionary.
            foreach (KeyValuePair<string, int> item in weaponItems)
            {
                // Gets the properties for the item as well as the current slot and the labels for that slot.
                WeaponItemProperties itemProperties = (WeaponItemProperties)allItems[item.Key];
                GameObject slot = weaponItemSlots.transform.GetChild(slotIndex).gameObject;
                TMP_Text powerLabel = slot.transform.GetChild(3).gameObject.GetComponent<TMP_Text>();
                TMP_Text quantityLabel = slot.transform.GetChild(4).gameObject.GetComponent<TMP_Text>();
                GameObject itemDisplay = slot.transform.GetChild(5).gameObject;

                // Enables enabled slot UI image.
                slot.transform.GetChild(0).gameObject.SetActive(false);
                slot.transform.GetChild(1).gameObject.SetActive(true);
                slot.transform.GetChild(3).gameObject.SetActive(true);
                slot.transform.GetChild(4).gameObject.SetActive(true);

                // Sets the power label.
                powerLabel.text = itemProperties.getWeaponPower().ToString();

                // Sets the quantity label.
                if (item.Value <= 0)
                {
                    quantityLabel.text = "";
                }
                else
                {
                    quantityLabel.text = "x" + item.Value.ToString();
                }

                // Sets the item display for the item.
                GameObject newItemDisplay = Instantiate(itemProperties.getItemGameObject(), itemDisplay.transform.position, itemDisplay.transform.rotation);
                newItemDisplay.transform.parent = itemDisplay.transform;

                // itemList[slotIndex] = item.Key;

                slotIndex++;
            }
        }
    }

    void presentSelectedItemInfo()
    {
        int currentSlotIndex = invSelection.getCurrentSlot(weaponItemSlots);
        ItemProperties itemInfo = allItems[itemList[currentSlotIndex]];
        TMP_Text itemLabel = weaponItemSlots.transform.GetChild(14).gameObject.GetComponent<TMP_Text>();
        itemLabel.text = itemInfo.getItemName();
    }

    void Update()
    {
        if (currentSlot != null)
        {
            // Selects the current slot.
            currentSlot.transform.GetChild(2).gameObject.SetActive(true);
        }

        // Updates the current slot from user input.
        updateCurrentSlot();
    }
}
