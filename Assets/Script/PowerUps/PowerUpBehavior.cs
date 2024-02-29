using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This would be the spawner and such for the items
 */
public class PowerUpBehavior : MonoBehaviour
{
    public Dictionary<PowerUpType, PowerUpFactory> powerUpFactories;
    public PowerUpType powerUpType;
    public Transform spawnPoint;
    public GameObject floor;
    public int numberOfObjectsToSpawn = 5;
    public int itemsSpawned = 0;
    private bool isSpawning = false;


    private FireballFactory fireballFactory;
    private SpeedBoostFactory SpeedBoostFactory;
    private HealthFactory healthFactory;

    public PlayerController playerControl;

    void Start()
    {
        //instaniate the factory objects
        fireballFactory = FindObjectOfType<FireballFactory>();
        SpeedBoostFactory = FindObjectOfType<SpeedBoostFactory>();
        healthFactory = FindObjectOfType<HealthFactory>();

        playerControl = FindObjectOfType<PlayerController>();

        // Manually set up the powerUpFactories dictionary in the Unity Editor
        powerUpFactories = new Dictionary<PowerUpType, PowerUpFactory>
        {
            { PowerUpType.SpeedBoost, SpeedBoostFactory},
            { PowerUpType.Fireball, fireballFactory},
            { PowerUpType.Health, healthFactory },
            // Add more mappings as needed
        };

        //SpawnPowerUp();
    }

    //dynamically spawns the power ups
    private void Update()
    {
        if (itemsSpawned < numberOfObjectsToSpawn && !isSpawning)
        {
            StartCoroutine(SpawnPowerUpWithDelay());
        }
    }

    //ensures that the spawner will only spawn one power up at a time
    IEnumerator SpawnPowerUpWithDelay()
    {
        isSpawning = true;

        // Introduce a delay before attempting to spawn the next power-up
        yield return new WaitForSeconds(1.0f); // Adjust the delay time as needed

        SpawnPowerUp();

        isSpawning = false;
    }

    //spawns the power up randomly
    void SpawnPowerUp()
    {

        //get the size of the floor 
        if (floor != null)
        {
            // Assuming the target GameObject has a MeshRenderer or SpriteRenderer component
            Renderer rendererComponent = floor.GetComponent<Renderer>();

            if (rendererComponent != null)
            {
                // Get the size of the target GameObject
                Vector3 size = rendererComponent.bounds.size;

            }
            else
            {
                Debug.LogError("Renderer component not found on the target GameObject.");
            }
        }
        else
        {
            Debug.LogError("Target GameObject is not assigned. Please assign it in the Inspector.");
        }

        //create and place a random powerup on the floor
        if (floor != null)
        {
            //error for hitting the button to spawn power up
            if (playerControl.PowerUp == null||playerControl.PowerUp == "")
            {
                if (playerControl.powerUpSpawned == false)
                {
                    // Assuming you have an array or list of available power-up types
                    PowerUpType[] availablePowerUpTypes = { PowerUpType.Fireball, PowerUpType.SpeedBoost, PowerUpType.Health };

                    // Randomly select a power-up type
                    PowerUpType powerUpType = availablePowerUpTypes[Random.Range(0, availablePowerUpTypes.Length)];
                        
                    //gets the power up randomly
                    if (powerUpFactories.TryGetValue(powerUpType, out PowerUpFactory selectedFactory))
                    {
                        IPowerUps powerUp = selectedFactory.CreatePowerUp(powerUpType);
                        if (powerUp != null)
                        {
                            powerUp.Initialize();

                            // Generate a random spawn point within the bounds of the floor
                            Vector3 randomSpawnPoint = GetRandomSpawnPoint(floor);

                            // Spawn the object at the random spawn point
                            Instantiate(powerUp.GameObject, randomSpawnPoint, Quaternion.identity);
                            itemsSpawned++;
                            Debug.Log(itemsSpawned);
                    }
                        else
                        {
                            Debug.LogError("Failed to create or initialize the power-up.");
                            return;
                        }
                    }
                }
                else
                {
                    Debug.Log("Already Spawned Powerup");
                }
            }
            else
            {
                Debug.Log("Already have Power Up");
            }

                

        }

    }

    //creates a random Vector3 position to place the powerups at
    Vector3 GetRandomSpawnPoint(GameObject floor)
    {
        Renderer floorRenderer = floor.GetComponent<Renderer>();

        if (floorRenderer != null)
        {
            Bounds bounds = floorRenderer.bounds;

            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            return new Vector3(randomX, bounds.center.y + 0.5f, randomZ);
        }
        else
        {
            Debug.LogError("Renderer component not found on the floor GameObject.");
            return Vector3.zero;
        }
    }
  
}
