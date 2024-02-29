using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VMovement : MonoBehaviour
{
    private Rigidbody cube_RigidBody;
    public float baseSpeed = 5.0f;
    public float sprintSpeed = 9.0f;
    public float currentSpeed;
    public Transform cameraTransform;

    public float sprintDuration = 3.0f;  
    private float sprintTimer;
    private float dodgeTimer;
    public float dodgeTime = 1.5f;

    private PlayerInput playerInput;
    private PlayerControls playerControl;
    public bool isSprint;
    Vector3 movement;

    //changes the speed of the player for a peroid of time after pressed
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

    //start sprinting 
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

    //stop sprint
    private void StopSprint()
    {
        if (isSprint)
        {
            Debug.Log("Sprint Stopped");
            isSprint = false;
            currentSpeed = baseSpeed;
        }
    }

    //timer for sprint
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

    //gets the context's position and uses it in a move function
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector3 movementInput = context.ReadValue<Vector3>();
        Move(movementInput);
    }

    //moves the player based on the camera position
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

    private void OnDodge(InputAction.CallbackContext context)
    {
        currentSpeed = 8.0f;
        Debug.Log("Dodge");
        StartCoroutine(DodgeTimer());
    }

    private IEnumerator DodgeTimer()
    {
        dodgeTimer = dodgeTime;

        while (dodgeTimer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            dodgeTimer--;

            // Optionally, you can update UI or perform other actions based on remaining sprint time
        }

        StopDodge();
    }

    private void StopDodge()
    {
        Debug.Log("Dodge Stopped");
        currentSpeed = baseSpeed;
        dodgeTime = 1.5f;
    }


    //allows for continous movement
    private void FixedUpdate()
    {

        cube_RigidBody.velocity = new Vector3(movement.x, cube_RigidBody.velocity.y * Time.deltaTime, movement.z);
        //stops it from falling over
        cube_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

    }

    //subscribes to input system actions
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
            playerInput.actions["Dodge"].performed += OnDodge;
            playerInput.actions["Dodge"].started += OnDodge;
        }
    }

    //unsubscribes to the movement action
    private void OnDisable()
    {
        if (playerControl != null)
        {
            playerControl.Disable();

            // Check if the action exists before attempting to unsubscribe
            if (playerInput.actions["Move"] != null)
            {
                playerInput.actions["Move"].started -= OnMove;
                playerInput.actions["Move"].performed -= OnMove;
                playerInput.actions["Move"].canceled -= OnMove;
            }

            if (playerInput.actions["Sprint"] != null)
            {
                playerInput.actions["Sprint"].performed -= OnSprint;
                playerInput.actions["Sprint"].canceled -= OnSprint;
                playerInput.actions["Sprint"].started -= OnSprint;
            }

            if (playerInput.actions["Dodge"] != null)
            {
                playerInput.actions["Dodge"].performed -= OnDodge;
                playerInput.actions["Dodge"].started -= OnDodge;
                playerInput.actions["Dodge"].canceled -= OnDodge;
            }
        }
    
    }

    //subscribes to all actions and the states
    private void Awake()
    {
        playerControl = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();

        // Remove existing subscriptions before adding new ones
        playerInput.actions["Move"].performed -= OnMove;
        playerInput.actions["Move"].canceled -= OnMove;
        playerInput.actions["Move"].started -= OnMove;
        playerInput.actions["Sprint"].performed -= OnSprint;
        playerInput.actions["Sprint"].canceled -= OnSprint;
        playerInput.actions["Sprint"].started -= OnSprint;
        playerInput.actions["Dodge"].performed -= OnDodge;
        playerInput.actions["Dodge"].started -= OnDodge;
        playerInput.actions["Dodge"].canceled -= OnDodge;

        // Subscribe to the events
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["Move"].started += OnMove;
        playerInput.actions["Sprint"].performed += OnSprint;
        playerInput.actions["Sprint"].canceled += OnSprint;
        playerInput.actions["Sprint"].started += OnSprint;
        playerInput.actions["Dodge"].performed += OnDodge;
        playerInput.actions["Dodge"].started += OnDodge;
        playerInput.actions["Dodge"].canceled += OnDodge;


    }

    void Start()
    {
        cube_RigidBody = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;
    }

   
}
