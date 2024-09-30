using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class EnemyVision : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float viewAngle = 120f; // Field of view angle (120 degrees)
    public float viewDistance = 10f; // Maximum view distance
    public float minDistance = 3f;   // Minimum distance to start detection
    public LayerMask obstructionMask; // Layer for objects that can block vision (e.g., walls)

    public float minMoveDistance = 15f; // Minimum distance NPC should move from spawn point
    public float maxMoveDistance = 20f; // Minimum distance NPC should move from spawn point
    public float checkInterval = 1f;    // How often to check if the NPC reached its destination

    private State currentState;

    private Vector3 currentRoamLocation;
    private NavMeshAgent npcAgent;

    private void FixedUpdate()
    {
        if (CanSeePlayer())
        {
            if (currentState == State.Looking)
            {
                currentState = State.Chase;
                GetComponentInChildren<Animator>().SetTrigger("Crawl");
                npcAgent.speed = 5f;
            }
            Debug.Log("Player is in sight!");
        }
        else
        {
            Debug.Log("Player is out of sight.");
        }

        if (currentState == State.Chase)
        {
            ChasePlayer();
        }

        CheckArrival(npcAgent);
    }

    public void StartRoaming(Vector3 spawnPosition)
    {
        npcAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(SetNextDestination(npcAgent, spawnPosition, minMoveDistance));
    }

    private void CheckArrival(NavMeshAgent agent)
    {

        // Step 4: Check if the NPC has arrived at the destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath)
        {
            // Step 5: Once the NPC arrives, find a new destination
            Debug.Log("NPC arrived at destination. Finding new destination...");
            StartCoroutine(SetNextDestination(agent, agent.transform.position, minMoveDistance));
        }



    }

    IEnumerator SetNextDestination(NavMeshAgent agent, Vector3 currentPosition, float minMoveDistance)
    {
        Vector3 newDestination = Vector3.zero;

        // Keep trying to find a valid destination that's far enough from current position
        while (newDestination == Vector3.zero)
        {
            Vector3 randomPoint = GetRandomPointOnNavMesh(currentPosition, minMoveDistance, maxMoveDistance);
            if (randomPoint != Vector3.zero)
            {
                newDestination = randomPoint;
                agent.SetDestination(newDestination);
                //Debug.Log($"NPC moving to {newDestination}");
            }
            else
            {
                Debug.LogError("Failed to find a valid move destination.");
            }

            yield return null;
        }
    }

    Vector3 GetRandomPointOnNavMesh(Vector3 center, float minRadius, float maxRadius)
    {
        for (int i = 0; i < 30; i++)  // Try 30 times to find a valid NavMesh position
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxRadius;
            randomDirection += center;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, maxRadius, NavMesh.AllAreas))
            {
                float distanceFromCenter = Vector3.Distance(center, hit.position);
                if (distanceFromCenter >= minRadius) // Ensure it's outside the minimum radius
                {
                    return hit.position;
                }
            }
        }

        return Vector3.zero;  // Return zero vector if no valid point found
    }

    private void ChasePlayer()
    {
        npcAgent.SetDestination(player.transform.position);
    }

    bool CanSeePlayer()
    {
        // Step 1: Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Step 2: Check if the player is within the minimum and maximum distances
        if (distanceToPlayer < minDistance || distanceToPlayer > viewDistance)
        {
            return false; // Player is either too close or too far
        }

        // Step 3: Calculate the angle between the enemy's forward direction and the direction to the player
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // Step 4: Check if the player is within the field of view (120 degrees)
        if (angleToPlayer <= viewAngle / 2)
        {
            // Step 5: Perform a raycast to check if there's a direct line of sight to the player
            if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstructionMask))
            {
                return true; // No obstacles, enemy can see the player
            }
        }

        return false; // Player is either outside the FOV or blocked by an obstacle
    }

    public void SetPlayer(Transform playerPos) => player = playerPos;

    public void SetState(State state) => currentState = state;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewDistance);


        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minMoveDistance);

    }
}
