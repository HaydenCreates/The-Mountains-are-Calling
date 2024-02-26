using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFactory : PowerUpFactory
{
    //reference to the Speedboost prefab
    public HealthPower healthPowerPrefab;
    public override IPowerUps CreatePowerUp(PowerUpType powerUp)
    {
        Debug.Log("Creating Health");
        HealthPower healthPower = Instantiate(healthPowerPrefab);

        // Optionally set position or perform additional configuration
        InitializePowerUp(healthPower);
        return healthPower;
    }
}
