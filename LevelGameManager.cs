using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LevelGameManager : ScriptableObject
{
    private static LevelGameManager _instance;
    public Transform AllLevels;
    public static List<GameObject> indivLevels = new List<GameObject>();
    public int collectableCnt = 0;

    //persisting the level data through scenes
    private LevelData levelData;

    //gets all the current levels and children, can be dynamically changed
    private void Start()
    {
        if(AllLevels == null)
        {
            Debug.LogError("No AllLevels");
        }


        int children = AllLevels.childCount;
        Debug.Log(children);

        for (int i = 0; i < children; i++)
        {
            indivLevels.Add(AllLevels.GetChild(i).gameObject);
            if (i == 0)
            {
                levelData.levelCompletion.Add(true);
            }
            else
            {
                levelData.levelCompletion.Add(false);
            }

            Debug.Log("Children: " + AllLevels.GetChild(i).name);
        }

    }
    private void Update()
    {
        for (int i = 0; i < AllLevels.childCount; i++)
        {
            if(levelData.levelCompletion[i] == false)
            {
                indivLevels[i].gameObject.SetActive(false);
            }
            else
            {
                indivLevels[i].gameObject.SetActive(true);
            }
        }
    }

    //get the specific placement for a level - name of level needs to be same as the button
    public int GetLevel()
    {
        //will this get the scene when changed?
        string currentLevel = SceneManager.GetActiveScene().name;

        //find the level index
        for(int i = 0; i < indivLevels.Count; i++)
        {
            if(currentLevel == indivLevels[i].name)
            {
                Debug.Log(currentLevel);
                return i;
            }
        }

        return -1;
    }

    //mark a level as completed
    public void LevelCompleted()
    {
        //gets the current level
        int index = GetLevel();

        //sets the next level to be accessed
        if(index != -1)
        {
            levelData.levelCompletion[index + 1] = true;
            Debug.Log(index);
        }
        else
        {
            Debug.LogError("No Level Found");
        }

    }

    //checks the level status
    public bool LevelStatus()
    {
        //gets the current level
        int index = GetLevel();

        //sets the next level to be accessed
        if (index != -1)
        {
            //if the level is less than the collectables then you can earn on
            if(collectableCnt < index - 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            Debug.LogError("No Level Found");
        }

        return true;
    }

    //updates the collectable count

    //creates the instance of the script
    public static LevelGameManager Instance;
    private void Awake()
    {
        /*Singleton method
        if (_instance == null)
        {
            //First run, set the instance
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (_instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(_instance.gameObject);
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        */

        levelData = new LevelData();
        levelData.levelCompletion = new List<bool>();
    }

}

public class LevelData
{
    public List<bool> levelCompletion;
}
