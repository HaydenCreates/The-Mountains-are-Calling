using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class VMovement : MonoBehaviour
{
    private Rigidbody cube_RigidBody;
    public float speed = 3.0f;

    private PlayerInput playerInput;
    private PlayerControls playerControl;
    private CinemachineFreeLook freeLookCamera;
    public bool isSprint;

    //subscribing to actions mean you can use them?

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Sprint Started");
            isSprint = true;
            speed = 6.0f;

        }

        if (context.canceled)
        {
            Debug.Log("Sprint Stopped");
            isSprint = false;
            speed = 3.0f;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector3 movementInput = context.ReadValue<Vector3>();
        Move(movementInput);
    }

    private void Move(Vector3 movementInput)
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Transform input to world space based on camera's forward and right vectors
            Vector3 forward = mainCamera.transform.forward.normalized;
            Vector3 right = mainCamera.transform.right.normalized;

            Vector3 movement = (forward * movementInput.y + right * movementInput.x).normalized * speed;

            // Apply movement to the character
            cube_RigidBody.velocity = new Vector3(movement.x, cube_RigidBody.velocity.y, movement.z);
            cube_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        }
        else
        {
            Debug.LogError("Main Camera not found.");
        }
    }

    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerControls();
            playerControl.Enable();
            playerInput = GetComponent<PlayerInput>();
            playerInput.actions["Sprint"].performed += OnSprint;
            playerInput.actions["Sprint"].started += OnSprint;
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].started += OnMove;
        }
    }

    private void OnDisable()
    {
        if (playerControl != null)
        {
            playerControl.Disable();
            playerInput.actions["Sprint"].performed -= OnSprint;
            playerInput.actions["Sprint"].canceled -= OnSprint;
            playerInput.actions["Sprint"].started -= OnSprint;
            playerInput.actions["Move"].started -= OnMove;
            playerInput.actions["Move"].performed -= OnMove;
            playerInput.actions["Move"].canceled -= OnMove;
        }
    }

    private void Awake()
    {
        playerControl = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Sprint"].performed += OnSprint;
        playerInput.actions["Sprint"].canceled += OnSprint;
        playerInput.actions["Sprint"].started += OnSprint;
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["Move"].started += OnMove;

    }

    void Start()
    {
        cube_RigidBody = GetComponent<Rigidbody>();
    }

   
}
