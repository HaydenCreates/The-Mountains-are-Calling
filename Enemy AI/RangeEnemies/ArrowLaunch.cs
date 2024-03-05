using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLaunch : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float launchForce = 10f;
    private GameObject arrow;

    public float arrowDamage = 5;
    public Transform firePoint;
    private Vector3 launchDirection;
    private Transform playerPos;

    private RangedEnemy rangedEnemyInstance;
    

    public void LaunchArrow(RangedEnemy enemy)
    {
        // Ensure there is a fire point assigned
        playerPos = enemy.GetPlayerPosition();

        Vector3 enemyPosition = enemy.GetEnemyPosition();

        // Get the fire point position and rotation
        Vector3 launchPosition = new Vector3(enemyPosition.x + 1f, enemyPosition.y + 0.7f, enemyPosition.z);
        // Instantiate the fireball at the fire point - think it's hitting the player
        arrow = Instantiate(arrowPrefab, launchPosition, Quaternion.identity);

        // Calculate the direction from the launch point to the player's position
        launchDirection = (playerPos.position - enemyPosition).normalized;
        launchDirection.y = launchDirection.y + 0.15f;

        // Apply force to the fireball to make it move in the aiming direction
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        if (arrowRb != null)
        {
            arrowRb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Environment"))
        {
            Debug.Log("Wall hit");
            Destroy(this.gameObject);
        }
        else if (collision.collider.CompareTag("Player"))
        {
            // The collision occurred with an object having the specified tag
            Debug.Log("Hit far enemy with tag: " + collision.collider.tag);

            // deal damage to the enemy
            PlayerController enemyHealth = collision.collider.GetComponent<PlayerController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(arrowDamage);
            }

            Destroy(this.gameObject);
        }

    }
}
