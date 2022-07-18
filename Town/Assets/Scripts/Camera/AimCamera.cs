using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCamera : MonoBehaviour
{
    public GameControls controls;
    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject aimingCanvas;

    private bool isAiming = false;
    private float aimUITimer = 0f;

    void Awake()
    {
        controls = new GameControls();
        controls.Gameplay.Aim.performed += ctx => isAiming = true;
        controls.Gameplay.Aim.canceled += ctx => isAiming = false;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            if (aimUITimer == 0f)
            {
                aimUITimer = Time.time + 0.4f;
            }

            // Switches the camera to the aim camera.
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);

            // Checks if the time is greater than the aim UI timer.
            if (Time.time > aimUITimer && !aimingCanvas.activeInHierarchy)
            {
                // Sets the crosshair UI to active.
                aimingCanvas.SetActive(true);
            }
        }
        else
        {
            // Switches to the main camera.
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimingCanvas.SetActive(false);

            aimUITimer = 0f;
        }
    }
}
