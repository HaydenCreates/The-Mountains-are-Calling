using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This would be the spawner and such for the items
 */
public class PowerUpBehavior : MonoBehaviour
{
    public PowerUpFactory powerUpFactory;
    public PowerUpType powerUpType;
    public Transform spawnPoint;
    public GameObject floor;
    public int numberOfObjectsToSpawn = 5;

    void Start()
    {
        SpawnPowerUp();
    }

    void SpawnPowerUp()
    {
        //creates an powerup and intializes it - need to make it allow for multiple powerups
        IPowerUps powerUp = powerUpFactory.CreatePowerUp(powerUpType);
        if (powerUp != null)
        {
            powerUp.Initialize();
        }

        //get the size of the floor 
        if (floor != null)
        {
            // Assuming the target GameObject has a MeshRenderer or SpriteRenderer component
            Renderer rendererComponent = floor.GetComponent<Renderer>();

            if (rendererComponent != null)
            {
                // Get the size of the target GameObject
                Vector3 size = rendererComponent.bounds.size;

                Debug.Log("Size of target GameObject: " + size);
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
        if (floor != null && powerUp != null)
        {
            for (int i = 0; i < numberOfObjectsToSpawn; i++)
            {
                // Generate a random spawn point within the bounds of the floor
                Vector3 randomSpawnPoint = GetRandomSpawnPoint(floor);

                // Spawn the object at the random spawn point
                Instantiate(powerUp.GameObject, randomSpawnPoint, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogError("Please assign the floor and objectToSpawn in the Inspector.");
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

            return new Vector3(randomX, bounds.center.y + 1, randomZ);
        }
        else
        {
            Debug.LogError("Renderer component not found on the floor GameObject.");
            return Vector3.zero;
        }
    }
  

}
