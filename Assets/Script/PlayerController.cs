using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerControls playerControl;
    private InputAction attackAction;
    public PowerUpType PowerUp;

    public float baseDamage = 10f;
    public float baseHealth = 100.0f;

    private void Awake()
    {
        playerControl = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
        attackAction.performed += OnHit;
        attackAction.canceled += OnHit;

    }

    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerControls();
            playerControl.Enable();
            playerInput = GetComponent<PlayerInput>();
            attackAction.performed += OnHit;

        }
    }

    private void OnDisable()
    {
        if (playerControl != null)
        {
            playerControl.Disable();
            attackAction.performed -= OnHit;
            attackAction.canceled += OnHit;

        }
    }

    //if the sowrd game object hits a object with the emeny tag, then that enemeny's health will go down by the base attack damage
    private void OnHit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Perform the attack logic here
            Debug.Log("Attacking!");

            // Check for collisions with objects of the "Enemy" tag
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f); // You might need to adjust the radius
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    // The collision occurred with an object having the specified tag
                    Debug.Log("Hit enemy with tag: " + hitCollider.tag);

                    // Add your custom logic here, e.g., deal damage to the enemy
                    EmemiesHealth enemyHealth = hitCollider.GetComponent<EmemiesHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(baseDamage);
                    }
                }
            }
        }

    }


}
