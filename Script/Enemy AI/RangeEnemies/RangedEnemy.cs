using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    //gets the current state by interface
    private IEnemyStateRange currentState;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Transform playerTransform; // Reference to the player's transform
    public float attackDamage = 10.0f;
    public ArrowLaunch arrowLaunch;

    public delegate void ShotDelayDelegate();

    // Declare a field of the delegate type
    public ShotDelayDelegate shotDelayDelegate;


    void Start()
    {
        // Initialize the enemy with the PatrolState
        SetState(new RangedPatrolState());
    }

    private void FixedUpdate()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }

    }

    //sets the current state to something 
    public void SetState(IEnemyStateRange state)
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
    public UnityEngine.AI.NavMeshAgent GetNavMeshAgent()
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
        return navMeshAgent;
    }

    public void ShotDelay()
    { 
        if (arrowLaunch == null)
        {
            Debug.LogError("arrowLaunch is null. Make sure it is properly initialized.");
            return;
        }

        Debug.Log("Shooting Arrow");
        // After the delay, initiate shooting or other attack-related actions
        arrowLaunch.LaunchArrow();
    }

    public void InvokeShotDelay()
    {
        // Invoke the delegate
        shotDelayDelegate?.Invoke();
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
