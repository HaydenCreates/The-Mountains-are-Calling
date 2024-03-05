using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button exitButton;
    // Start is called before the first frame update
    public void OnStartButtonClick()
    {
        // Load the game scene
        SceneManager.LoadScene("Level Select");
    }

    public void OnLoginButtonClick()
    {
        SceneManager.LoadScene("Login");
    }

    public void OnExitButtonClick()
    {
        // Quit the application
        Application.Quit();
    }
}
