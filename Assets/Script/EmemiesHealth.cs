using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemiesHealth : MonoBehaviour
{
    public float baseHealth = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //the enemies' health will go down when hit
    public void TakeDamage(float damage)
    {
        baseHealth -= damage;
    }
}
