using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2.09f, player.transform.position.z);
    }

    void RotatePlayer()
    {
        if (aimCamera.activeInHierarchy)
        {
            // Sets the player's angle around the y axis to the follow target's angle. (Moves player side to side).
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, transform.eulerAngles.y, player.transform.eulerAngles.z);

            // Sets the gun holster's angle around the x axis to the follow target's angle. (Moves gun up and down).
            gunHolster.transform.eulerAngles = new Vector3(transform.eulerAngles.x, gunHolster.transform.eulerAngles.y, gunHolster.transform.eulerAngles.z);
        }
    }

    void RotateTarget()
    {
        if (targetDirection.magnitude >= 0.1f)
        {
            // Rotates the target about the x axis and y axis whenever the input is triggered.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x + targetDirection.y * cameraAngularVelocity, transform.eulerAngles.y + (-1 * targetDirection.x * cameraAngularVelocity), transform.eulerAngles.z);
        }
    }

    void CheckAngularVelocity()
    {
        if (aimCamera.activeInHierarchy)
        {
            cameraAngularVelocity = 0.04f;
        }
        else
        {
            cameraAngularVelocity = 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckAngularVelocity();
        RotatePlayer();
        FollowPlayer();
        RotateTarget();
    }
}
