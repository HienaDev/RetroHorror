using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyVision : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float viewAngle = 120f; // Field of view angle (120 degrees)
    public float viewDistance = 10f; // Maximum view distance
    public float minDistance = 3f;   // Minimum distance to start detection
    public LayerMask obstructionMask; // Layer for objects that can block vision (e.g., walls)s

    public float minMoveDistance = 15f; // Minimum distance NPC should move from spawn point
    public float maxMoveDistance = 20f; // Minimum distance NPC should move from spawn point
    public float checkInterval = 1f;    // How often to check if the NPC reached its destination

    [SerializeField] private float agentStoppingDistance = 1f;
    [SerializeField] private float jumpScareRotationDuration = 0.2f;

    private State currentState;

    private Vector3 currentRoamLocation;
    private NavMeshAgent npcAgent;

    [SerializeField] private float timeAlive = 20f;
    [SerializeField] private float timeAliveForDontMove = 5f;
    [SerializeField] private float  timeToChasePlayer = 90f;
    private float justSpawned = Mathf.Infinity;

    private bool canDie = false;

    [SerializeField] private GameObject monsterGoneSound;
    [SerializeField] private GameObject monsterSeeSound;
    

    [SerializeField] private GameObject meshObject;

    private void Start()
    {
        justSpawned = Time.time;

        if (currentState == State.DontMove)
            ChasePlayer();

        if (currentState == State.Chase)
        {
            GetComponentInChildren<Animator>().SetTrigger("Crawl");
            GameObject temp = Instantiate(monsterSeeSound);
            npcAgent.speed = 5f;
        }

        Debug.Log("state is: " + currentState);
    }

    private void FixedUpdate()
    {

        if ((Input.GetKeyDown(KeyCode.P)))
        {
            int layerSeeThrough = LayerMask.NameToLayer("SeeThroughWall");
            if(meshObject.layer != layerSeeThrough)
                meshObject.layer = layerSeeThrough;
            else
                meshObject.layer = LayerMask.NameToLayer("Default");
        }

        if (CanSeePlayer())
        {
            if (currentState == State.Looking || currentState == State.Here)
            {
                currentState = State.Chase;
                GetComponentInChildren<Animator>().SetTrigger("Crawl");
                GameObject temp = Instantiate(monsterSeeSound);
                npcAgent.speed = 5f;
            }
            Debug.Log("Player is in sight!");
        }
        else
        {
            Debug.Log("Player is out of sight.");
        }

        CheckArrival(npcAgent);

        if (currentState == State.Chase)
        {
            ChasePlayer();
        }

        if (currentState == State.DontMove && player.GetComponent<PlayerMovement>().IsMoving())
        {

            currentState = State.Chase;
            GetComponentInChildren<Animator>().SetTrigger("Crawl");
            GameObject temp = Instantiate(monsterSeeSound);
            npcAgent.speed = 5f;

        }


        if (Time.time - justSpawned > timeAlive)
        {
            if (currentState == State.Looking || (currentState == State.DontMove && canDie))
            {
                GameObject temp = Instantiate(monsterGoneSound);
                temp.transform.position = gameObject.transform.position;
                Destroy(gameObject);
            }
                
        }


    }

    public void StartRoaming(Vector3 spawnPosition)
    {
        npcAgent = GetComponent<NavMeshAgent>();
        npcAgent.stoppingDistance = agentStoppingDistance;
        StartCoroutine(SetNextDestination(npcAgent, spawnPosition, minMoveDistance));
    }

    private void CheckArrival(NavMeshAgent agent)
    {

        // Step 4: Check if the NPC has arrived at the destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath)
        {
            if(currentState == State.Chase)
            {
                player.GetComponent<PlayerMovement>().ToggleMovement(false);
                player.GetComponentInChildren<FirstPersonCamera>().ToggleCamera(false);
                player.GetComponentInChildren<TriggerJumpScareScript>().JumpScare();

                Destroy(gameObject, 0.1f);
            }
            else
            {
                // Step 5: Once the NPC arrives, find a new destination
                Debug.Log("NPC arrived at destination. Finding new destination...");
                if (currentState == State.DontMove)
                {
                    canDie = true;
                    timeAlive = timeAliveForDontMove;
                    justSpawned = Time.time;
                }

                StartCoroutine(SetNextDestination(agent, agent.transform.position, minMoveDistance));
            }

        }



    }


    IEnumerator SetNextDestination(NavMeshAgent agent, Vector3 currentPosition, float minMoveDistance)
    {
        Vector3 newDestination = Vector3.zero;

        int randomDecision = Random.Range(0, 2);
        if (randomDecision == 0 || Time.time - justSpawned > timeToChasePlayer)
            ChasePlayer();
        else
        {
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

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agentStoppingDistance);

    }

}
