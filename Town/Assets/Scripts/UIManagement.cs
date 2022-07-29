using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagement : MonoBehaviour
{
    public GameControls controls;
    public GameObject inventoryCanvas;

    private bool enableInventory = false;

    private void Awake()
    {
        controls = new GameControls();

        controls.UI.Inventory.performed += ctx => enableInventory = !enableInventory;
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
    }
}
