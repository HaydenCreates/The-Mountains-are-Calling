using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerControls playerControl;
    private InputAction PauseAction;
    public GameObject canvas;

    public GameObject pauseScreen;
    public GameObject TutorialScreen;
    [SerializeField] private Button TutorialButton;
    [SerializeField] private Button PausedReturnButton;

    public static bool isPaused = false;

    //makes the actions avaiable 
    private void Awake()
    {
        playerControl = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        PauseAction = playerInput.actions["Pause"];

        PauseAction.performed += pauseGame;
        PauseAction.canceled += pauseGame;

        pauseScreen.SetActive(false);
        TutorialScreen.SetActive(false);
        Time.timeScale = 1;

    }

    //makes the actions avaiable
    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerControls();
            playerControl.Enable();
            playerInput = GetComponent<PlayerInput>();
            PauseAction.performed += pauseGame;
        }
    }

    //unsubscribes from actions
    private void OnDisable()
    {
        if (playerControl != null)
        {
            playerControl.Disable();
            PauseAction.performed -= pauseGame;
            PauseAction.canceled -= pauseGame;

        }
    }

    //Pauses the game objects
    public void pauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPaused = !isPaused;
            if (!isPaused)
            {
                Time.timeScale = 1;
                pauseScreen.SetActive(false);

                //AudioListener.pause = true; - pauses audio
                //AudioSource.ignoreListenerPause=true; - keeps the sound going
            }
            else
            {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            }
        }
    }

    public void OnTutorial()
    {
        TutorialScreen.SetActive(true);
    }

    public void ReturnTutorial()
    {
        TutorialScreen.SetActive(false);
    }

    public void OnReturnTitle()
    {
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
