using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemiesHealth : MonoBehaviour
{
    public float baseHealth = 100.0f;

    private void Update()
    {
        if (baseHealth <= 0.0)
        {
            Destroy(this.gameObject);
        }
    }

    //the enemies' health will go down when hit
    public void TakeDamage(float damage)
    {
        baseHealth -= damage;
    }

    void OnDestroy()
    {
        // Decrease enemy count or trigger wave progression
        EnemyController.Instance.DecrementEnemyCount();
    }
}
