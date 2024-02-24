using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPower : MonoBehaviour, IPowerUps
{
    [SerializeField] private string powerName = "SpeedBoost";
    public string PowerUp { get; set; }
    public GameObject GameObject { get; private set; }
    public float boostSpeed = 12.0f;

    public void Initialize()
    {
        PowerUp = powerName;
        // Unique logic for activating the powerup
        Debug.Log($"Activating {powerName} powerup!");
    }

    public void PlayerInteraction()
    {
        Debug.Log("Speed used");
    }
}
