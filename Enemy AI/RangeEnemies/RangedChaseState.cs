using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedChaseState : MonoBehaviour, IEnemyStateRange
{
    private Transform enemyTransform;
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    public Vector3 playerPosition;

    public float followRange = 30.0f;  // Set the range within which the NPC will follow the player
    public float attackRange = 20.0f; // Set the range within which the NPC will start to attack the player
    //public Rigidbody NPCBody;
    public float speed = 10.0f;

    public void EnterState(RangedEnemy enemy)
    {
        // Logic for entering Chase state
        playerTransform = enemy.GetPlayerPosition();
        playerPosition = playerTransform.position;
        //gets the reference form the enemy class - what is attached to the object
        enemyTransform = enemy.transform;

        navMeshAgent = enemy.GetNavMeshAgent();
        // Set initial position and destination
        navMeshAgent.enabled = true;
        navMeshAgent.Warp(enemy.transform.position);

    }

    public void UpdateState(RangedEnemy enemy)
    {

        //while the player is in range of the player, move towards them
        if (IsPlayerDetected(enemy))
        {
            MoveTowardsPlayer();
            if (IsPlayerAttackable())
            {
                enemy.SetState(new RangedAttackState());
            }

        }
        // Check if the player is within attack range
        else if (IsPlayerAttackable())
        {
            Debug.Log("Attacking Range State 2");
            enemy.SetState(new RangedAttackState());
            return;
        }
        //if neither then return to patrol state
        else
        {
            enemy.SetState(new RangedAttackState());
            return;
        }

    }

    //call exit state when the enemy goes to a new state?
    public void ExitState(RangedEnemy enemy)
    {
        // Logic for exiting Chase state

        // Stop the NavMeshAgent when exiting the patrol state
        if (navMeshAgent != null)
        {
            MoveTowardsPlayer();
        }

    }

    //checks if the player is in range for attacking
    private bool IsPlayerAttackable()
    {
        float distanceToPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);
        return distanceToPlayer <= attackRange;
    }

    //moves closers to the player
    private void MoveTowardsPlayer()
    {
        float maxRaycastDistance = 20.0f;  // Adjust this value based on your needs
        Debug.DrawRay(enemyTransform.position, enemyTransform.forward * maxRaycastDistance, Color.red);

        Collider[] hitColliders = Physics.OverlapSphere(enemyTransform.position, followRange);
        foreach (var hitcollider in hitColliders)
        {
            // Check if the ray hits the player
            if (hitcollider.CompareTag("Player"))
            {
                // Move the NPC towards the player
                // Set the destination to player position
                navMeshAgent.SetDestination(playerPosition);

            }
        }

    }

    //if the player is in range it will change to true
    private bool IsPlayerDetected(RangedEnemy enemy)
    {
        float maxRaycastDistance = 30.0f;  // Adjust this value based on your needs
        Debug.DrawRay(enemyTransform.position, enemyTransform.forward * maxRaycastDistance, Color.red);

        // Cast a ray towards the player
        Collider[] hitColliders = Physics.OverlapSphere(enemyTransform.position, followRange);
        foreach (var hitcollider in hitColliders)
        {
            // Check if the ray hits the player's collider
            if (hitcollider.CompareTag("Player"))
            {
                return true; // Player detected
            }
        }

        return false; // Player not detected
    }
}
