using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * Template for the enemy creation and changing states
 */
public class Enemy : MonoBehaviour
{
    //gets the current state by interface
    private IEnemyState currentState;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform; // Reference to the player's transform
    public float attackDamage = 10.0f;
    private PlayerController playerInstance;

    private void Awake()
    {
        playerInstance = PlayerController.Instance;
    }

    void Start()
    {
        // Initialize the enemy with the PatrolState
        SetState(new PatrolState());

        playerTransform = playerInstance.gameObject.transform;
    }

    //sets the current state to something 
    public void SetState(IEnemyState state)
    {
        //automantically exits the state
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = state;
        currentState.EnterState(this);
    }

    //Updates the state
    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public Transform GetPlayerPosition()
    {
        
        return playerTransform;
    }

    //gets the NavMeshAgent of the enemy
    public NavMeshAgent GetNavMeshAgent()
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }
        return navMeshAgent;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            // The collision occurred with an object having the specified tag
            Debug.Log("Hit enemy with tag: " + collision.collider.tag);

            // Add your custom logic here, e.g., deal damage to the enemy
            PlayerController playerHealth = collision.collider.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

        }
    }
}
