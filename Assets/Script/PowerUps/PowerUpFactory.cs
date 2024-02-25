
using UnityEngine;
/*
 * Creates the interface/factory that will be used for the powerups and concrete factories
 */
public abstract class PowerUpFactory : MonoBehaviour
{
    //can us Vector position for spawning? Should I use it?
    public abstract IPowerUps CreatePowerUp(PowerUpType powerType);

    protected void InitializePowerUp(IPowerUps powerUp)
    {
        // Common initialization logic
        powerUp.Initialize();
    }

}
//creating an interface for the powerups
public interface IPowerUps
{
    public string PowerUp { get; set; }
    GameObject GameObject { get; } // Add a property to get the GameObject
    public void Initialize();
    public void PlayerInteraction();
}

//gets the type of powerup
public enum PowerUpType
{
    SpeedBoost,
    Health,
    Fireball,
    // Add more power-up types as needed
}