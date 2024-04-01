using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    private PlayerController playerInstance;
    private GameObject player; 

    private void Awake()
    {
        playerInstance = PlayerController.Instance;
    }

    void Start()
    {
        player = playerInstance.gameObject;
        player.SetActive(false);
    }
}
