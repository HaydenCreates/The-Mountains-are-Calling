using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// Nested class to hold individual level data
[System.Serializable]
public class Level
{
    public string levelName; // Name of the level
    public bool isCompleted;  // Completion status
}

[CreateAssetMenu]
public class LevelGameManager : ScriptableObject
{
    // Singleton for accessing the current instance
    public static LevelGameManager Instance;
    private DatabaseManager database;

    // List containing data for each level
    public List<Level> levels = new List<Level>();
    public int collectableCnt = 0;

    private void Awake()
    {
        Instance = this;
        database = DatabaseManager.Instance;
    }

    // Get the current level data based on scene name
    public Level GetCurrentLevelData()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        foreach (Level level in levels)
        {
            if (level.levelName == currentLevel)
            {
                return level;
            }
        }
        Debug.LogError("No matching level found for scene: " + currentLevel);
        return null;
    }

    // Mark a level as completed
    public void MarkLevelCompleted(string levelName)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            Debug.Log(levels[i]);
            Level level = levels[i];
            if (level.levelName == levelName)
            {
                level.isCompleted = true;
                // Check if there's a next level based on your order
                if (i + 1 < levels.Count)
                {
                    levels[i + 1].isCompleted = true;
                }
                else
                {
                    Debug.Log("Last level completed!");
                }
            }
        }

        Debug.LogError("No matching level found for name: " + levelName);
    }

    // Check if a level is completed
    public bool IsLevelCompleted(string levelName)
    {
        foreach (Level level in levels)
        {
            if (level.levelName == levelName)
            {
                return level.isCompleted;
            }
        }
        Debug.LogError("No matching level found for name: " + levelName);
        return false;
    }

    //checks if the next level is done too, if not allow the player to access it
    public bool isNextLevelCompleted(string current)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            Level level = levels[i];
            if (level.levelName == current)
            {
                level.isCompleted = true;
                // Check if there's a next level based on your order
                if (i + 1 < levels.Count)
                {

                    return levels[i + 1].isCompleted;
                }
                else
                {
                    Debug.Log("Last level completed!");
                }
            }
        }

        Debug.LogError("No matching level found for name: " + current);
        return false;
    }

    //update the number of collectables
    public void updateCollect()
    {
        collectableCnt += 1;
    }

    //reset when closed
    private void OnDisable()
    {
        Reset();
    }

    //reset all values
    public void Reset()
    {
        collectableCnt = 0;
        levels.Clear();
        //removed get level data since it was not getting updated level
    }
}
