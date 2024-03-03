using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPower : MonoBehaviour, IPowerUps
{
    [SerializeField] private string powerName = "SpeedBoost";
    public string PowerUp { get; set; }
    public GameObject GameObject { get { return this.gameObject; } }
    public float boostSpeed = 15.0f;
    public int speedBoostNum = 1;
    
    public VMovement vMove;
    public PlayerController playerControl;

    //allows for the playerController script to be initalized at the start of the game
    void Start()
    {
        //the player controll script reference
        playerControl = FindObjectOfType<PlayerController>();
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
        Debug.Log("Speed used");

        //make the current power up in PlayerController == to fireball
        playerControl.PowerUp = powerName;
        playerControl.powerDamage = boostSpeed;
        playerControl.numOfPowerUp = speedBoostNum;
    }
}
