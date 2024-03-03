using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerControls playerControl;
    private InputAction attackAction;
    private InputAction powerUpAction;
    private InputAction strongAttackAction;
    
    // gets the type of powerUp when it's used
    public string PowerUp;

    //get the attributes of the powerUp
    public float powerDamage;
    public int numOfPowerUp;
    public GameObject powerUpObj;
    private bool isPowerUpActive;
    private bool strongCoolDown;
    public bool powerUpSpawned;
    public bool isDead = false;

    public float baseDamage = 10f;
    public float baseHealth = 100.0f;
    public float strongDamage = 15f;

    public FireballLauncher fireLaunch;
    public VMovement vMove;

    //allows for the playerController script to be initalized at the start of the game
    void Start()
    {
        //the player controll script reference
        fireLaunch = FindObjectOfType<FireballLauncher>();
        vMove = FindObjectOfType<VMovement>();
    }

    private void Update()
    {
        if (baseHealth <= 0.0)
        {
            isDead = true;
            Destroy(this.gameObject);
        }
    }

    //makes the actions avaiable 
    private void Awake()
    {
        playerControl = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
        powerUpAction = playerInput.actions["PowerUpAttack"];
        strongAttackAction = playerInput.actions["StrongAttack"];

        powerUpAction.performed += OnPowerUpUse;
        powerUpAction.canceled += OnPowerUpUse;
        attackAction.performed += OnHit;
        attackAction.canceled += OnHit;
        strongAttackAction.performed += OnStrongHit;
        strongAttackAction.canceled += OnStrongHit;

    }

    //makes the actions avaiable
    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerControls();
            playerControl.Enable();
            playerInput = GetComponent<PlayerInput>();
            attackAction.performed += OnHit;
            powerUpAction.performed += OnPowerUpUse;
            strongAttackAction.performed += OnStrongHit;

        }
    }

    //unsubscribes from actions
    private void OnDisable()
    {
        if (playerControl != null)
        {
            playerControl.Disable();
            attackAction.performed -= OnHit;
            attackAction.canceled -= OnHit;

            powerUpAction.performed -= OnPowerUpUse;
            powerUpAction.canceled -= OnPowerUpUse;

            strongAttackAction.performed -= OnStrongHit;
            strongAttackAction.canceled -= OnStrongHit;

        }
    }

    //if the sword game object hits a object with the emeny tag, then that enemeny's health will go down by the base attack damage
    private void OnHit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Perform the attack logic here
            Debug.Log("Attacking!");

            // Check for collisions with objects of the "Enemy" tag - does it based on general vinity
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.0f);
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

            //resets the array for infiante clicks
            System.Array.Clear(hitColliders, 0, hitColliders.Length);
        }

    }
    
    //Does a strong hit
    private void OnStrongHit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!strongCoolDown)
            {
                // Perform the attack logic here
                Debug.Log("Strong Attacking!");

                // Check for collisions with objects of the "Enemy" tag - change to just have compare tag?
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.0f); // You might need to adjust the radius
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
                            enemyHealth.TakeDamage(strongDamage);
                            StartCoroutine(StongAttackCoolDOwn(10f));
                        }
                    }
                }

                System.Array.Clear(hitColliders, 0, hitColliders.Length);
            }
            else
            {
                Debug.Log("Strong Attack on Cooldown");
            }
        }

    }

    //depending on the action, the power up will do something 
    private void OnPowerUpUse(InputAction.CallbackContext context)
    {
        //stops from spawning a powerup when power up is empty and clicked
        if (string.IsNullOrEmpty(PowerUp))
        {
            // Handle the case when PowerUp is null or empty
            Debug.Log("No power-up selected.");
            return; // Exit the method early
        }

        switch (PowerUp)
        {
            case "Fireball":
                if (numOfPowerUp > 0)
                {
                    // Player has enough Fireball power-ups
                    // Perform actions related to using Fireball power-up
                    numOfPowerUp--;

                    // Call a method or perform logic specific to the Fireball power-up
                    fireLaunch.LaunchFireball();

                }
                else
                {
                    // Player does not have enough Fireball power-ups
                    Debug.Log("Not enough Fireball power-ups!");
                }
                break;

            case "SpeedBoost":
                if (numOfPowerUp > 0)
                {
                    numOfPowerUp--;

                    vMove.currentSpeed = powerDamage;

                    // Start a coroutine to run the power-up effect for a specific duration
                    StartCoroutine(DeactivatePowerUpAfterDuration(25f));
                }
                else
                {
                    // Player does not have enough Fireball power-ups
                    Debug.Log("No more Speed Boosts");
                }
                break;

            case "Health":
                if (numOfPowerUp > 0)
                {
                    numOfPowerUp--;

                    baseHealth += powerDamage;
                }
                else
                {
                    // Player does not have enough Fireball power-ups
                    Debug.Log("No more Health");
                }
                break;

        }

        //resets the powerup attributes
        if(numOfPowerUp == 0)
        {
            PowerUp = null;
            powerDamage = 0;
            numOfPowerUp = 0;
            powerUpSpawned = false;
        }
    }

    //timer for use of strong attack
    private IEnumerator StongAttackCoolDOwn(float duration)
    {
        strongCoolDown = true;
        yield return new WaitForSeconds(duration);
        endCoolDown();
    }

    private void endCoolDown()
    {
        strongCoolDown = false;
    }

    // Coroutine to deactivate the power-up after a duration
    private IEnumerator DeactivatePowerUpAfterDuration(float duration)
    {
        isPowerUpActive = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the power-up effect
        DeactivatePowerUp();
    }

    // Method to deactivate the power-up effect
    private void DeactivatePowerUp()
    {
        isPowerUpActive = false;
        // Reset speed or perform any other necessary actions
        vMove.currentSpeed = vMove.baseSpeed;
    }

    //take damage when the player is hit
    public void TakeDamage(float damage)
    {
        baseHealth -= damage;
    }

    //end game if the player is dead - need to change to do something to indicate game over
    private void OnDestroy()
    {
        Debug.Log("Game Over");
    }
}
