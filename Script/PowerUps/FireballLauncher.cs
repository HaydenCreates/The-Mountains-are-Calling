using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float launchForce = 30f;
    public Transform firePoint;
    private GameObject fireball;
    private FireballPower fireballPow;

    public static FireballLauncher Instance;
    
    private void Awake()
    {
        //the fireball script
        fireballPow = FireballPower.Instance;
        Instance = this;
    }

    public void LaunchFireball()
    {
        
        // Ensure there is a fire point assigned
        if (firePoint == null)
        {
            Debug.LogError("Fire point not assigned to FireballLauncher!");
            return;
        }
        Vector3 cameraFace = Camera.main.transform.forward;
        cameraFace.y = cameraFace.y + 0.15f;

        Vector3 playerPosition = firePoint.position;

        // Get the fire point position and rotation
        Vector3 launchPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 0.5f) ;

        // Instantiate the fireball at the fire point - think it's hitting the player
        fireball = Instantiate(fireballPrefab, launchPosition, Quaternion.identity);

        // Apply force to the fireball to make it move in the aiming direction
        Rigidbody fireballRb = fireball.GetComponent<Rigidbody>();
        if (fireballRb != null)
        {
            fireballRb.AddForce(cameraFace * launchForce, ForceMode.Impulse);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Environment"))
        {
            Debug.Log("Wall hit");
            Destroy(this.gameObject);
        } else if (collision.collider.CompareTag("Enemy"))
        {
            // The collision occurred with an object having the specified tag
            Debug.Log("Hit enemy with tag: " + collision.collider.tag);

            // Add your custom logic here, e.g., deal damage to the enemy
            EmemiesHealth enemyHealth = collision.collider.GetComponent<EmemiesHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(fireballPow.fireDamage);
            }

            Destroy(this.gameObject);
        }
    }
   

}
