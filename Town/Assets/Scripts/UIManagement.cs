using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagement : MonoBehaviour
{
    public GameControls controls;
    public GameObject inventoryCanvas;

    private bool enableInventory = false;
    private Vector2 inventorySelectorDirection;
    private GameObject currentInventory;
    private int currentInvIndex;
    private float switchTimer = 0f;
    private float timeInterval = 0f;

    private void Awake()
    {
        controls = new GameControls();

        controls.UI.Inventory.performed += ctx => enableInventory = !enableInventory;
        controls.UI.SwitchInventory.performed += ctx => inventorySelectorDirection = ctx.ReadValue<Vector2>();
        controls.UI.SwitchInventory.canceled += ctx => inventorySelectorDirection = Vector2.zero;
    }

    private void Start()
    {
        currentInvIndex = 4;
        currentInventory = inventoryCanvas.transform.GetChild(currentInvIndex).gameObject;
    }

    void OnEnable()
    {
        controls.UI.Enable();
    }

    void OnDisable()
    {
        controls.UI.Disable();
    }

    void checkUI()
    {
        if (enableInventory)
        {
            Time.timeScale = 0f;
            inventoryCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            // Checks if the player selected a different inventory.
            if (inventorySelectorDirection.magnitude >= 0.1f && timeInterval > switchTimer)
            {
                // Checks if the right arrow or left arrow was pressed.
                if (inventorySelectorDirection.x == 1f && currentInvIndex < 5)
                {
                    // Sets the current inventory to false.
                    currentInventory.SetActive(false);
                    currentInvIndex++;

                    // Updates the current inventory.
                    currentInventory = inventoryCanvas.transform.GetChild(currentInvIndex).gameObject;
                    currentInventory.SetActive(true);
                }
                else if (inventorySelectorDirection.x == -1f && currentInvIndex > 4)
                {
                    // Sets the current inventory to false.
                    currentInventory.SetActive(false);
                    currentInvIndex--;

                    // Updates the current inventory.
                    currentInventory = inventoryCanvas.transform.GetChild(currentInvIndex).gameObject;
                    currentInventory.SetActive(true);
                }

                switchTimer = timeInterval + 50f;
            }
        }
        else
        {
            Time.timeScale = 1f;
            inventoryCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        checkUI();

        timeInterval++;
    }
}
