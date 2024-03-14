using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
/*
* Controls the spawn of different types of enemies
*/
public class EnemyController : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public GameObject slimePrefab;
    public GameObject sheepPrefab;
    private GameObject randomEnemy;

    //the min and max number of enemies, and the tracking of enemies
    public int minEnemies = 1;
    public int maxEnemies = 10;
    public int numOfEnemiesInWave;
    public int currentEnemies;

    public static EnemyController Instance;

    private void Awake()
    {
        Instance = this;
    }

    //Enum to specify the type of enemy
    public enum EnemyType
    {
        Melee,
        Ranged
    }


    private void Start()
    {
        startWave();
    }

    //starts the wave of enemies
    public void startWave()
    {
        numOfEnemiesInWave = Random.Range(minEnemies, maxEnemies);
        currentEnemies = numOfEnemiesInWave;
        SpawnRandomEnemies(numOfEnemiesInWave); // Spawn random number enemies - in the level manager have the base number increase as different stages happen?
        Debug.Log("spawned Enemies");
    }

    //spawns a number of enemies within the level
    private void SpawnRandomEnemies(int count)
    {
        //spawns a certain amount of enemies - make random and in the level manager have waves?
        for (int i = 0; i < count; i++)
        {
            EnemyType randomType = (EnemyType) Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length);
            if (randomType == EnemyType.Melee)
            {
                Enemy temp = CreateEnemy();
                randomEnemy = temp.gameObject;
            }
            else
            {
                RangedEnemy temp = CreateRangedEnemy();
                randomEnemy = temp.gameObject;
            }

            //sets the spawn point to a random point in the nav mesh 
            Vector3 randomPos = RandomNavMeshPoint(30f);

            //sets a max number of time to get a valid position
            int maxTries = 0;
            while (randomPos == new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity) ||
                randomPos == new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity))
            {
                maxTries--;
                randomPos = RandomNavMeshPoint(30f);

                if(maxTries == 0)
                {
                    randomPos = new Vector3(0, 0, 0);
                    break;
                }
            }

            randomEnemy.transform.position = new Vector3(randomPos.x,randomPos.y,randomPos.z);

        }
    }

    //gets a random spawn point
    Vector3 RandomNavMeshPoint(float radius)
    {
        // Generate a random point within the NavMesh bounds
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, radius, -1);
        return navHit.position;
    }

    //creates a regular enemy
    public Enemy CreateEnemy()
    {
        GameObject enemyGO = null;

        //gets a random value
        int shortRangeEnemy = Random.Range(0, 2);

        //if the value if 1 create a slime, else create a sheep
        if (shortRangeEnemy == 1)
        {
            enemyGO = Instantiate(slimePrefab);
        }
        else
        {
            enemyGO = Instantiate(sheepPrefab);
        }

        return enemyGO?.GetComponent<Enemy>();

    }

    //creates a ranged enemy
    public RangedEnemy CreateRangedEnemy()
    {
        GameObject enemyGO = null;
        enemyGO = Instantiate(skeletonPrefab);
        return enemyGO?.GetComponent<RangedEnemy>();

    }

    //decreases the number of enemies when they are destroyed
    public void DecrementEnemyCount()
    {
        currentEnemies -= 1;
    }

    //checks if all enemies are dead
    public bool waveEnded()
    {
        if(currentEnemies <= 0)
        {
            Debug.Log("wave ended");
            return true;
        }

        return false;
    }
}
