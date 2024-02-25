using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Will use this as the base for the conrete FireballFactory method <-- it will call it
 * Also uses the interface to make the powerups
 */
public class FireballPower : MonoBehaviour, IPowerUps
{
    [SerializeField] private string powerName = "Fireball";
    public string PowerUp { get; set; }
    public GameObject GameObject { get { return this.gameObject; } }
    public float fireDamage = 20.0f;
    public int fireballNum = 3;

    public PlayerController playerControl;

    //allows for the playerController script to be initalized at the start of the game
    void Start()
    {
        //the player controll script reference
        playerControl = FindObjectOfType<PlayerController>();
    }

    //how it will be created when it is called on at runtime
    public void Initialize()
    {
        //sets the ower up name based on the Factory
        PowerUp = "Fireball";

        // Unique logic for activating the powerup
        Debug.Log($"Activating {powerName} powerup!");

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

    //how the player will be able to use the powerup
    public void PlayerInteraction()
    {
        Debug.Log("Fireball used");

        //make the current power up in PlayerController == to fireball
        playerControl.PowerUp = powerName;
        playerControl.powerDamage = fireDamage;
        playerControl.numOfPowerUp = fireballNum;
    }
}
