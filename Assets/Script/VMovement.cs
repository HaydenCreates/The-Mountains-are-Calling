using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class VMovement : MonoBehaviour
{
    private Rigidbody cube_RigidBody;
    public float baseSpeed = 3.0f;
    public float sprintSpeed = 6.0f;
    public float currentSpeed;

    public float sprintDuration = 3.0f;  // Adjust the duration as needed
    private float sprintTimer;

    private PlayerInput playerInput;
    private PlayerControls playerControl;
    public bool isSprint;
    Vector3 movement;

    //subscribing to actions mean you can use them?

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Sprint Started");
            StartSprint();

        }

        if (context.canceled)
        {
            Debug.Log("Sprint Stopped");
            StopSprint();
        }
    }

    private void StartSprint()
    {
        if (!isSprint)
        {
            Debug.Log("Sprint Started");
            isSprint = true;
            currentSpeed = sprintSpeed;
            StartCoroutine(SprintTimer());
        }
    }

    private void StopSprint()
    {
        if (isSprint)
        {
            Debug.Log("Sprint Stopped");
            isSprint = false;
            currentSpeed = baseSpeed;
        }
    }

    private IEnumerator SprintTimer()
    {
        sprintTimer = sprintDuration;

        while (sprintTimer > 0 && isSprint)
        {
            yield return new WaitForSeconds(1.0f);
            sprintTimer--;

            // Optionally, you can update UI or perform other actions based on remaining sprint time
        }

        StopSprint();
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

            movement = (forward * movementInput.y + right * movementInput.x).normalized * currentSpeed;

        }
        else
        {
            Debug.LogError("Main Camera not found.");
        }
    }

    //allows for continous movement
    private void FixedUpdate()
    {
        cube_RigidBody.velocity = new Vector3(movement.x, cube_RigidBody.velocity.y, movement.z);
        cube_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerControls();
            playerControl.Enable();
            playerInput = GetComponent<PlayerInput>();
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].started += OnMove;
            playerInput.actions["Sprint"].performed += OnSprint;
            playerInput.actions["Sprint"].started += OnSprint;
        }
    }

    private void OnDisable()
    {
        if (playerControl != null)
        {
            playerControl.Disable();
            playerInput.actions["Move"].started -= OnMove;
            playerInput.actions["Move"].performed -= OnMove;
            playerInput.actions["Move"].canceled -= OnMove;
            playerInput.actions["Sprint"].performed -= OnSprint;
            playerInput.actions["Sprint"].canceled -= OnSprint;
            playerInput.actions["Sprint"].started -= OnSprint;
            
        }
    }

    private void Awake()
    {
        playerControl = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["Move"].started += OnMove;
        playerInput.actions["Sprint"].performed += OnSprint;
        playerInput.actions["Sprint"].canceled += OnSprint;
        playerInput.actions["Sprint"].started += OnSprint;
        

    }

    void Start()
    {
        cube_RigidBody = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;
    }

   
}
