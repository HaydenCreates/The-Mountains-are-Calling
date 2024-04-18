using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectScript : MonoBehaviour
{
    //make it the array?
    [SerializeField] private Button Level1Button;
    [SerializeField] private Button ReturnButton;
    [SerializeField] private Button Level2Button;

    // Start is called before the first frame update
    public void OnLevel1Select()
    {
        // Load the game scene
        SceneManager.LoadScene("Demo Level");
    }

    public void OnLevel2Select()
    {
        // Load the game scene
        SceneManager.LoadScene("Level 2");
    }

    public void OnFinalLevelSelect()
    {
        // Load the game scene
        SceneManager.LoadScene("Final Level");
    }


    public void OnReturnTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
