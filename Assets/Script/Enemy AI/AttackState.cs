using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IEnemyState
{
    private Transform enemyTransform;
    private Transform playerTransform;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Vector3 playerPosition;

    public float attackRange = 10.0f;

    public void EnterState(Enemy enemy)
    {
        // Logic for entering Chase state
        playerTransform = enemy.GetPlayerPosition();
        playerPosition = playerTransform.position;
        //gets the reference form the enemy class - what is attached to the object
        enemyTransform = enemy.transform;

        navMeshAgent = enemy.GetNavMeshAgent();
        // Set initial position and destination
        navMeshAgent.enabled = true;
        navMeshAgent.Warp(enemyTransform.position);
    }

    public void UpdateState(Enemy enemy)
    {
        //Dynamically update the player position
        playerTransform = enemy.GetPlayerPosition();
        playerPosition = playerTransform.position;

        if (IsPlayerAttackable())
        {
            Debug.Log("Attacking");
            navMeshAgent.SetDestination(playerPosition);
        }
        else
        {
            Debug.Log("Not Attackable");
            enemy.SetState(new ChaseState());
            return;
        }
    }

    public void ExitState(Enemy enemy)
    {
        // Logic for exiting Exit state

        Debug.Log("Returning to Chase");
    }

    private bool IsPlayerAttackable()
    {
        float distanceToPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);
        return distanceToPlayer <= attackRange;
    }

}
