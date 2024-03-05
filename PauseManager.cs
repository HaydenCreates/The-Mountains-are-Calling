using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerControls playerControl;
    private InputAction PauseAction;

    public static bool isPaused = false;

    //makes the actions avaiable 
    private void Awake()
    {
        playerControl = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        PauseAction = playerInput.actions["Pause"];

        PauseAction.performed += pauseGame;
        PauseAction.canceled += pauseGame;

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
                Debug.Log("Paused Screen");

                //AudioListener.pause = true; - pauses audio
                //AudioSource.ignoreListenerPause=true; - keeps the sound going
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }
}
