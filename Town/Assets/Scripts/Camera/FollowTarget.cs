using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowTarget : MonoBehaviour
{
    public GameControls controls;
    public GameObject player;
    public GameObject gunHolster;
    public GameObject aimCamera;

    private Vector3 targetDirection;
    private float cameraAngularVelocity;

    void Awake()
    {
        controls = new GameControls();
        controls.Gameplay.Camera.performed += ctx => targetDirection = ctx.ReadValue<Vector2>();
        controls.Gameplay.Camera.canceled += ctx => targetDirection = Vector2.zero;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void followPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2.09f, player.transform.position.z);
    }

    void rotatePlayer()
    {
        if (aimCamera.activeInHierarchy)
        {
            // Sets the player's angle around the y axis to the follow target's angle. (Moves player side to side).
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, transform.eulerAngles.y, player.transform.eulerAngles.z);

            // Sets the gun holster's angle around the x axis to the follow target's angle. (Moves gun up and down).
            gunHolster.transform.eulerAngles = new Vector3(transform.eulerAngles.x, gunHolster.transform.eulerAngles.y, gunHolster.transform.eulerAngles.z);
        }
    }

    void rotateTarget()
    {
        if (targetDirection.magnitude >= 0.1f)
        {
            print(transform.eulerAngles.x);
            // print(targetDirection.normalized.y);

            if ((transform.eulerAngles.x < 270f && targetDirection.normalized.y == 1f) || (transform.eulerAngles.x > 90f && targetDirection.normalized.y == -1f))
            {
                print("STOP");
                targetDirection.y = 0f;
            }

            // Rotates the target about the x axis and y axis whenever the input is triggered.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x + (-1 * targetDirection.y * cameraAngularVelocity), transform.eulerAngles.y + targetDirection.x * cameraAngularVelocity, transform.eulerAngles.z);
        }
    }

    void checkAngularVelocity()
    {
        if (aimCamera.activeInHierarchy)
        {
            cameraAngularVelocity = 1f;
        }
        else
        {
            cameraAngularVelocity = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 mousePos = Mouse.current.position.ReadValue();
        // targetDirection = mousePos.normalized;
        if (Time.timeScale == 1f)
        {
            checkAngularVelocity();
            rotatePlayer();
            followPlayer();
            rotateTarget();
        }
    }
}
