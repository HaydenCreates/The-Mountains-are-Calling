using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemiesHealth : MonoBehaviour
{
    public float baseHealth = 100.0f;

    //the enemies' health will go down when hit
    public void TakeDamage(float damage)
    {
        baseHealth -= damage;
    }
}
