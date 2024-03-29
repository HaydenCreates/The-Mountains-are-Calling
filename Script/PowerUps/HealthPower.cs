using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPower : MonoBehaviour, IPowerUps
{
    [SerializeField] private string powerName = "Health";
    public string PowerUp { get; set; }
    public GameObject GameObject { get { return this.gameObject; } }
    public float healAmount = 25.0f;
    public int healthNum = 1;

    public PlayerController playerControl;

    //allows for the playerController script to be initalized at the start of the game
    void Awake()
    {
        //the player controll script reference
        playerControl = PlayerController.Instance;
    }

    public void Initialize()
    {
        PowerUp = powerName;
        // Unique logic for activating the powerup
        Debug.Log($"Activating {powerName} powerup!");
        playerControl.powerUpSpawned = true;
    }

    //will initate the player interaction once the player hits it
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision with player detected");

            PlayerInteraction();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Collision with non-player object detected");
        }
    }

    //sets up the power up attributes
    public void PlayerInteraction()
    {
        Debug.Log("Health used");

        //make the current power up in PlayerController == to fireball
        playerControl.PowerUp = powerName;
        playerControl.powerDamage = healAmount;
        playerControl.numOfPowerUp = healthNum;
    }
}
