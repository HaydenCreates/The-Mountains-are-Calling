using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUI : MonoBehaviour
{
    private PlayerController playerInstance;
    private EnemyController enemyControllerInstance;
    private LevelManager levelManagerInstance;
    private PowerUpBehavior powerUpInstance;

    public UIDocument uIDocument;

    //the UI elements
    public IntegerField health;
    public IntegerField enemies;
    public IntegerField waves;
    public IntegerField powerNum;

    public VisualElement healthImage;
    public VisualElement fireImage;
    public VisualElement speedImage;

    // Start is called before the first frame update

    //UI Buttons[
    private void OnEnable()
    {
        VisualElement root = uIDocument.rootVisualElement;
        health = root.Q<IntegerField>("Health");
        enemies = root.Q<IntegerField>("Enemies");
        waves = root.Q<IntegerField>("Waves");
        powerNum = root.Q<IntegerField>("PowerNum");

        healthImage = root.Q<VisualElement>("HealthPow");
        speedImage = root.Q<VisualElement>("Speed");
        fireImage = root.Q<VisualElement>("Fireball");
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the scirpts instances
        playerInstance = PlayerController.Instance;
        enemyControllerInstance = EnemyController.Instance;
        levelManagerInstance = LevelManager.Instance;
        powerUpInstance = PowerUpBehavior.Instance;

        healthImage.SetEnabled(false);
        speedImage.SetEnabled(false);
        fireImage.SetEnabled(false);


    }

    // Update is called once per frame
    void Update()
    {
        //set all values and update them dynamically
        health.value = (int)playerInstance.baseHealth;
        enemies.value = (int) enemyControllerInstance.currentEnemies;
        waves.value = (int) levelManagerInstance.currentWave;
        powerNum.value = (int) playerInstance.numOfPowerUp;
        powerImageUpdater();
    }

    //enable and disable the powerups images
    void powerImageUpdater()
    {
        switch (playerInstance.PowerUp)
        {
            case "Fireball":
                fireImage.SetEnabled(true);
                break;
            case "SpeedBoost":
                speedImage.SetEnabled(true);
                break;
            case "Health":
                healthImage.SetEnabled(true);
                break;
            default:
                healthImage.SetEnabled(false);
                speedImage.SetEnabled(false);
                fireImage.SetEnabled(false);
                break;
        }
    }
}
