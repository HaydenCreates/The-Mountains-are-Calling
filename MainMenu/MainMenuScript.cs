using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button exitButton;
    // Start is called before the first frame update

    private void Start()
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user != null)
        {
            // User is logged in
            startButton.enabled = true;
            Debug.Log("Logged in to start");
            Debug.Log(user.UserId);
        }
        else
        {
            startButton.enabled = false;
            Debug.Log("Need to log in");
        }

    }

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

    private void OnApplicationQuit()
    {
        FirebaseAuth.DefaultInstance.SignOut();
    }
}
