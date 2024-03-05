using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private VMovement VMovementInstance;
    [SerializeField] private PlayerController playerInstance;
    [SerializeField] private EnemyController enemyControllerInstance;
    [SerializeField] private LevelManager levelManagerInstance;

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        VMovementInstance = VMovement.Instance;
        playerInstance = PlayerController.Instance;
        enemyControllerInstance = EnemyController.Instance;
        levelManagerInstance = LevelManager.Instance;

        canvas = GameObject.FindGameObjectWithTag("MainCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        Transform healthUITransform = canvas.transform.Find("Health");
        TextMeshProUGUI healthText = healthUITransform.GetComponentInChildren<TextMeshProUGUI>();
        healthText.text = "Health: " + playerInstance.baseHealth;

        Transform powerUITransform = canvas.transform.Find("PowerUp");
        TextMeshProUGUI powerUpText = powerUITransform.GetComponentInChildren<TextMeshProUGUI>();
        if(playerInstance.PowerUp == "")
        {
            powerUpText.text = "Power: ";
        }
        else
        {
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
