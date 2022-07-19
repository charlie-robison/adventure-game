using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameControls controls;
    public GameObject player;
    public Transform camera;
    public CharacterController controller;
    public float speed = 4f;
    public float jumpForce = 2f;
    public float gravity = -9.8f;
    public Vector3 playerVelocity;

    public float targetAngle;

    private Vector3 direction;
    private bool jumpButton = false;
    private bool speedIncreased = false;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    void Awake()
    {
        controls = new GameControls();
        controls.Gameplay.Movement.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => direction = Vector2.zero;
        controls.Gameplay.Jump.performed += ctx => jumpButton = true;
        controls.Gameplay.Jump.canceled += ctx => jumpButton = false;
        controls.Gameplay.Speed.performed += ctx => speedIncreased = !speedIncreased;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void MovePlayer()
    {
        // Increases the speed if button was pressed.
        if (speedIncreased)
        {
            speed = 4.2f;
            // player.GetComponent<PlayerAnimations>().setAnimationSpeed(1.2f);
        }
        else
        {
            speed = 3f;
            // player.GetComponent<PlayerAnimations>().setAnimationSpeed(1f);
        }

        // Checks if there direction is not 0.
        if (direction.magnitude >= 0.1f)
        {
            // Gets the player's angle by adding the camera's angle and the angle between the x direction and y direction from the input system.
            targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Rotates the player to the angle and sets the direction it is facing.
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Sets the x and z velocity from the player.
            playerVelocity.x = moveDirection.normalized.x * speed;
            playerVelocity.z = moveDirection.normalized.z * speed;
            // player.GetComponent<PlayerAnimations>().setRunAnimation();
        }
        else
        {
            // Sets the x and z velocity to 0.
            playerVelocity.x = 0f;
            playerVelocity.z = 0f;
            // player.GetComponent<PlayerAnimations>().resetRunAnimation();
        }

        // Moves the player.
        controller.Move(playerVelocity * Time.deltaTime);
    }

/* Adds a jump force if the button is pressed. */
    void Jump()
    {
        playerVelocity.y += gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);

        // Checks if the player is on the ground.
        if (controller.isGrounded)
        {
            if (playerVelocity.y < 0)
            {
                playerVelocity.y = -0.5f;
                // player.GetComponent<PlayerAnimations>().resetJumpAnimation();
            }

            if (jumpButton)
            {
                playerVelocity.y = jumpForce;
                jumpButton = false;
                // player.GetComponent<PlayerAnimations>().setJumpAnimation();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();

        controller.Move(playerVelocity * Time.deltaTime);
    }
}
