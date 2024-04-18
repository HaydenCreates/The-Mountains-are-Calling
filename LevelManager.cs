using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int waves = 3;
    public int currentWave = 1;

    private EnemyController enemyControllerInstance;
    private PlayerController playerControllerInstance;
    public static LevelManager Instance;
    public LevelGameManager levelGameInsatnce;
    public DatabaseManager database;

    private void Start()
    {
        enemyControllerInstance = EnemyController.Instance;
        playerControllerInstance = PlayerController.Instance;
        database = DatabaseManager.Instance;
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
                DontDestroyOnLoad(playerControllerInstance.gameObject);
                string currentLevelName = levelGameInsatnce.GetCurrentLevelData().levelName;

                //get the level object and change it's value to true
                bool status = levelGameInsatnce.isNextLevelCompleted(currentLevelName);
                //check if the level has been completed before 
                if (!status)
                {
                    levelGameInsatnce.updateCollect();
                    levelGameInsatnce.MarkLevelCompleted(currentLevelName);
                    database.SaveDataFn();
                    Debug.Log("Add Collectable");
                }

                SceneManager.LoadScene("Win Screen");
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
