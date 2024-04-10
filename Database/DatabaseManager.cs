using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
/*
 * Saves and loads data to a Firebase databse
 */

//the class of data that needs to be saved to the database
[Serializable]
public class dataToSave
{
    public int collectable;
    public List<Level> levels;

}

public class DatabaseManager : MonoBehaviour
{
    public dataToSave dts;
    public string userID;
    DatabaseReference reference;
    public LevelGameManager gameManager;
    private EmailPasswordManager userInstance;
    public static DatabaseManager Instance;

    // Calls for a reference to the Firebase database
    void Awake()
    {
       reference = FirebaseDatabase.DefaultInstance.RootReference;
       userInstance = EmailPasswordManager.Instance;
       userID = userInstance.userID;
       Instance = this;
    }

    //saves the data to a firebase json
    public void SaveDataFn()
    {
        // Update dts with level data before serialization
        dts.levels = gameManager.levels;
        dts.collectable = gameManager.collectableCnt;

        // Now serialize the updated dts object
        string json = JsonUtility.ToJson(dts);

        // Save the JSON string to Firebase
        reference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    //Loads the data from the database
    public void LoadDataFn()
    {
        StartCoroutine(LoadDataEnum());
    }

    IEnumerator LoadDataEnum()
    {
        var serverData = reference.Child("users").Child(userID).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        print("Process completed");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            print("Server data found");

            dts = JsonUtility.FromJson<dataToSave>(jsonData);
            gameManager.levels = dts.levels;
            gameManager.collectableCnt = dts.collectable;
        }
        else
        {
            print("No data found");
        }

    }

    private void OnApplicationQuit()
    {
        FirebaseAuth.DefaultInstance.SignOut();
    }

}
