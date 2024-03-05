using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        // Check if the agent has reached the destination
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            SetRandomDestination(); // Set a new random destination
        }
    }

    void SetRandomDestination()
    {
        // Set a random point within the NavMesh bounds as the new destination
        Vector3 randomDestination = RandomNavMeshPoint(10f);
        agent.SetDestination(randomDestination);
    }

    Vector3 RandomNavMeshPoint(float radius)
    {
        // Generate a random point within the NavMesh bounds
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, radius, -1);
        return navHit.position;
    }
}
