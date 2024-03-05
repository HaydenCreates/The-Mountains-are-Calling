using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackState : MonoBehaviour, IEnemyStateRange
{
    private Transform enemyTransform;
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    public Vector3 playerPosition;

    public ArrowLaunch arrowLaunch;
    public float attackRange = 20.0f;
    private bool isShooting = false;

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
        navMeshAgent.Warp(enemyTransform.position);
    }

    public void UpdateState(RangedEnemy enemy)
    {
        //Dynamically update the player position
        playerTransform = enemy.GetPlayerPosition();
        playerPosition = playerTransform.position;

        // Check if the player is still in attack range
        if (!IsPlayerAttackable())
        {
            // If the player is no longer attackable, transition to a different state (e.g., Chase or Patrol)
            if (isShooting)
            {
                // Cancel the repeating invocation if it's set up
                enemy.CancelInvoke("InvokeShotDelay");
                isShooting = false;
            }

            enemy.SetState(new RangedChaseState());
        }
        else
        {
            // If not shooting, set up the repeating invocation
            if (!isShooting)
            {
                FacePlayer(enemy);
                isShooting = true;
            }
        }
    }

    public void ExitState(RangedEnemy enemy)
    {
        // Logic for exiting Exit state

        Debug.Log("Returning to Chase");
    }

    private void FacePlayer(RangedEnemy enemy)
    {

        if (enemyTransform == null || playerTransform == null)
        {
            Debug.LogError("enemyTransform or playerTransform is null");
            return;
        }

        // Aim at the player's position
        Vector3 lookAtPosition = playerPosition;
        enemyTransform.LookAt(lookAtPosition);

        // Assign the ShotDelay method to the delegate - gets the method to shoot
        enemy.shotDelayDelegate = enemy.ShotDelay;

        // Use Invoke with the delegate
        enemy.InvokeRepeating("InvokeShotDelay", 0.5f, 10f);

    }

    private bool IsPlayerAttackable()
    {
        float distanceToPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);
        return distanceToPlayer <= attackRange;
    }
}
