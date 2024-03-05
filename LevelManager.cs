using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int waves = 3;
    public int currentWave = 1;

    private EnemyController enemyControllerInstance;
    public static LevelManager Instance;

    private void Start()
    {
        enemyControllerInstance = EnemyController.Instance;
    }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the wave is ended,
        //if true then it will update the min and max enemies spawned
        if(enemyControllerInstance == null)
        {
            Debug.LogError("No instance of an EnemyController");
        }

        if (enemyControllerInstance.waveEnded())
        {
            updateEnemiesSpawned();
            currentWave += 1;

            //checks if the number of waves are over
            if(currentWave <= waves)
            {
                enemyControllerInstance.startWave();
            }
            else
            {
                Debug.Log("Player Wins");
            }
        }
    }

    //increases the number of enemies per wave
    public void updateEnemiesSpawned()
    {
        enemyControllerInstance.minEnemies += 5;
        enemyControllerInstance.maxEnemies += 3;
    }
}
