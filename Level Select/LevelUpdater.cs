using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class LevelUpdater : MonoBehaviour
{
    //cant get an instance of the object through script, put into the inspector directly
    public LevelGameManager levelGameInsatnce;
    private DatabaseManager database;

    // Start is called before the first frame update
    void Start()
    {
        database = DatabaseManager.Instance;
        database.LoadDataFn();

        StartCoroutine(LoadLevels(3f));
    }

    IEnumerator LoadLevels(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (levelGameInsatnce == null)
        {
            Debug.LogError("No Level Game Instance");
        }

        //if the level completion is false, thend disable it
        foreach (Level level in levelGameInsatnce.levels)
        {
            if (level.isCompleted)
            {
                GameObject currentLevel = GameObject.Find(level.levelName);
                currentLevel.SetActive(true);
            }
            else
            {
                GameObject currentLevel = GameObject.Find(level.levelName);
                currentLevel.SetActive(false);
            }

            Debug.Log(level.levelName + level.isCompleted);
        }
        Debug.Log("Collectables: " + levelGameInsatnce.collectableCnt);
    }

    private void OnApplicationQuit()
    {
        FirebaseAuth.DefaultInstance.SignOut();
    }

}
