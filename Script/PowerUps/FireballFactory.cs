using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * You are creating an object of type Fireball, which is a concrete implementation of the IPowerUp interface.
 */
public class FireballFactory : PowerUpFactory
{
    //reference to the fireball prefab
    public FireballPower fireballPrefab;
    public override IPowerUps CreatePowerUp(PowerUpType powerUp)
    {
        FireballPower fireball = Instantiate(fireballPrefab);
        // Optionally set position or perform additional configuration
        InitializePowerUp(fireball);
        return fireball;
    }
}
