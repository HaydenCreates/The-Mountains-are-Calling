using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    private PlayerController playerInstance;
    private EnemyController enemyControllerInstance;
    private LevelManager levelManagerInstance;
    private PowerUpBehavior powerUpInstance;

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        //get the scirpts instances
        playerInstance = PlayerController.Instance;
        enemyControllerInstance = EnemyController.Instance;
        levelManagerInstance = LevelManager.Instance;
        powerUpInstance = PowerUpBehavior.Instance;

        canvas = GameObject.FindGameObjectWithTag("MainCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        //gets the Text component
        Transform healthUITransform = canvas.transform.Find("Health");
        TextMeshProUGUI healthText = healthUITransform.GetComponentInChildren<TextMeshProUGUI>();

        //sets the text component
        healthText.text = "Health: " + playerInstance.baseHealth;

        Transform powerUITransform = canvas.transform.Find("PowerUp");
        TextMeshProUGUI powerUpText = powerUITransform.GetComponentInChildren<TextMeshProUGUI>();
        powerUpText.fontSize = 25.6f;
        if (playerInstance.PowerUp == "")
        {
            powerUpText.text = "Power: ";
        }
        else
        {
            if (playerInstance.PowerUp == "SpeedBoost")
            {
                powerUpText.fontSize = 20f;
            }
            else
            {
                powerUpText.fontSize = 25.6f;
            }

            powerUpText.text = "Power: " + playerInstance.PowerUp;
        }
        

        Transform enemyUITransform = canvas.transform.Find("Enemies");
        TextMeshProUGUI enemyText = enemyUITransform.GetComponentInChildren<TextMeshProUGUI>();
        enemyText.text = "Current Enemies: " + enemyControllerInstance.currentEnemies;

        Transform powerNumUITransform = canvas.transform.Find("PowerNum");
        TextMeshProUGUI powerNumText = powerNumUITransform.GetComponentInChildren<TextMeshProUGUI>();
        powerNumText.text = "Power remaining: " + playerInstance.numOfPowerUp;

        Transform waveUITransform = canvas.transform.Find("Wave");
        TextMeshProUGUI waveText = waveUITransform.GetComponentInChildren<TextMeshProUGUI>();
        waveText.text = "Wave: " + levelManagerInstance.currentWave;

    }
}
