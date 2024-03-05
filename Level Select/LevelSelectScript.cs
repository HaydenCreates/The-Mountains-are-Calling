using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectScript : MonoBehaviour
{
    [SerializeField] private Button LevelButton;
    [SerializeField] private Button ReturnButton;

    // Start is called before the first frame update
    public void OnLevelSelect()
    {
        // Load the game scene
        SceneManager.LoadScene("Demo Level");
    }

    public void OnReturnTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}
