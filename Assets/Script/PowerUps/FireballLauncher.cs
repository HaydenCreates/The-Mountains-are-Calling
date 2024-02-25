using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float launchForce = 10f;
    public Transform firePoint;
    private GameObject fireball;

    public void LaunchFireball()
    {
        // Ensure there is a fire point assigned
        if (firePoint == null)
        {
            Debug.LogError("Fire point not assigned to FireballLauncher!");
            return;
        }
        Vector3 cameraFace = Camera.main.transform.forward;
        // Get the fire point position and rotation
        Vector3 launchPosition = firePoint.position;
        //Quaternion launchRotation = firePoint.rotation;

        // Instantiate the fireball at the fire point - think it's hitting the player
        fireball = Instantiate(fireballPrefab, launchPosition, Quaternion.identity);
        Debug.Log(fireball);

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
            Destroy(fireball.gameObject);
        }
    }
}
