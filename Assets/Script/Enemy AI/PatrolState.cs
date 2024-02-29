using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, IEnemyState
{
    private Transform enemyTransform;
    private NavMeshAgent agent;

    public float followRange = 20.0f;  // Set the range within which the NPC will follow the player
    public Rigidbody NPCBody;
    public float speed = 10.0f;

    public void EnterState(Enemy enemy)
    {
        // Logic for entering patrol state

        //gets the reference form the enemy class - what is attached to the object
        enemyTransform = enemy.transform;

        agent = enemy.GetNavMeshAgent();
        // Set initial position and destination
        agent.enabled = true;
        agent.Warp(enemy.transform.position);

        SetRandomDestination();

    }

    public void UpdateState(Enemy enemy)
    {
        // Logic for patrolling

        //if the player is detected, then the state will change chase/attack state
        if (isPlayerDetected(enemy) == true)
        {
            Debug.Log("State is now Chase");
            enemy.SetState(new ChaseState());
            return;

        }
        // Check if the agent has reached the destination
        else if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            SetRandomDestination(); // Set a new random destination
        }

    }

    //ends the movement by the NavMesh AI naviagtions
    public void ExitState(Enemy enemy)
    {
        // Logic for exiting patrol state - agent is null since it isn't attached to the enemy?
        Debug.Log("Moving to Chase");
    }

    //sets a random destination for the AI to go to
    void SetRandomDestination()
    {
        // Set a random point within the NavMesh bounds as the new destination
        Vector3 randomDestination = RandomNavMeshPoint(10f);
        agent.SetDestination(randomDestination);
    }

    //gets a random vector to move to
    Vector3 RandomNavMeshPoint(float radius)
    {
        // Generate a random point within the NavMesh bounds
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += agent.transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, radius, -1);
        return navHit.position;
    }

    //if the player is in range it will change to true
    private bool isPlayerDetected(Enemy enemy)
    {
        float maxRaycastDistance = 20.0f;  // Adjust this value based on your needs
        Debug.DrawRay(enemyTransform.position, enemyTransform.forward * maxRaycastDistance, Color.red);

        // Cast a ray towards the player
        Collider[] hitColliders = Physics.OverlapSphere(enemyTransform.position, followRange);
        foreach (var hit in hitColliders)
        {
            // Check if the ray hits the player's collider
            if (hit.GetComponent<Collider>().CompareTag("Player"))
            {
                Debug.Log("Player detected");
                return true; // Player detected
            }
        }

        return false; // Player not detected
    }
}
