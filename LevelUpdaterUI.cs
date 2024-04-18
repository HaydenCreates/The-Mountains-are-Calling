using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class LevelUpdaterUI : MonoBehaviour
{//cant get an instance of the object through script, put into the inspector directly
    public LevelGameManager levelGameInsatnce;
    private DatabaseManager database;
    public UIDocument uIDocument;

    private Button level1Butt;
    private Button level2Butt;
    private Button exitButt;
    private Button saveButt;

    private void OnEnable()
    {
        VisualElement root = uIDocument.rootVisualElement;
        
        level1Butt = root.Q<Button>("Demo_Level");
        level2Butt = root.Q<Button>("Level_2");
        exitButt = root.Q<Button>("Exit");
        saveButt = root.Q<Button>("Save");

    }

    // Start is called before the first frame update
    void Start()
    {
        database = DatabaseManager.Instance;
        database.LoadDataFn();

        StartCoroutine(LoadLevels(3f));

        level1Butt.clicked += delegate { OnLevel1Select(); };
        level2Butt.clicked += delegate { OnLevel2Select(); };
        exitButt.clicked += delegate { OnReturnTitle(); };
        saveButt.clicked += delegate { database.SaveDataFn(); };
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
            Button currentButton = uIDocument.rootVisualElement.Q<Button>(level.levelName); // Find button by level name
            if (level.isCompleted)
            {
                currentButton.SetEnabled(true);
            }
            else
            {
                currentButton.SetEnabled(false);
            }

            Debug.Log(level.levelName + level.isCompleted);
        }
        Debug.Log("Collectables: " + levelGameInsatnce.collectableCnt);
    }


    public void OnLevel1Select()
    {
        // Load the game scene
        SceneManager.LoadScene("Demo_Level");
    }

    public void OnLevel2Select()
    {
        // Load the game scene
        SceneManager.LoadScene("Level_2");
    }


    public void OnReturnTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnApplicationQuit()
    {
        FirebaseAuth.DefaultInstance.SignOut();
    }
}
