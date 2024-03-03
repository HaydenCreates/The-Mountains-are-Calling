
using UnityEngine;

public class SpeedBoostFactory : PowerUpFactory
{
    //reference to the Speedboost prefab
    public SpeedBoostPower speedBoostPrefab;
    public override IPowerUps CreatePowerUp(PowerUpType powerUp)
    {
        Debug.Log("Creating SpeedBoostPower");
        SpeedBoostPower speedBoost = Instantiate(speedBoostPrefab);
        // Optionally set position or perform additional configuration
        InitializePowerUp(speedBoost);
        return speedBoost;
    }
}
