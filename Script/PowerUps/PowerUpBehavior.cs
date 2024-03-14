using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
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
    public bool isSpawning = false;

    public TextMeshProUGUI spawnText;

    private FireballFactory fireballFactory;
    private SpeedBoostFactory SpeedBoostFactory;
    private HealthFactory healthFactory;

    public PlayerController playerControl;
    public static PowerUpBehavior Instance;

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
    }

    private void Awake()
    {
        Instance = this;
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

        Debug.Log("isSPawning is true");

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

                            //sets the spawn point to a random point in the nav mesh 
                            Vector3 randomPos = RandomNavMeshPoint(35f);
                            while (randomPos == new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity) ||
                                randomPos == new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity))
                            {
                                randomPos = RandomNavMeshPoint(35f);
                            }

                            // Spawn the object at the random spawn point
                            Instantiate(powerUp.GameObject, randomPos, Quaternion.identity);
                            itemsSpawned++;

                            StartCoroutine(showSpawnText());
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

    private IEnumerator showSpawnText()
    {
        spawnText.gameObject.SetActive(true);
        spawnText.text = "Power-Up Spawned!";

        yield return new WaitForSeconds(5.0f); // Adjust the delay time as needed

        spawnText.gameObject.SetActive(false);
    }

    //creates a random Vector3 position to place the powerups at
    Vector3 RandomNavMeshPoint(float radius)
    {
        // Generate a random point within the NavMesh bounds
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, radius, -1);
        return navHit.position;
    }

}
