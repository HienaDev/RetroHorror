using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;  // The NPC prefab to spawn

    public float minSpawnRadius = 10f;  // Minimum distance from spawner for NPC
    public float maxSpawnRadius = 30f;  // Maximum distance from spawner for NPC


    [SerializeField] private float spawnHeight = -3f;


    private NavMeshAgent npcAgent;  // Reference to NPC's NavMeshAgent
    [SerializeField] private Transform player;
    private Vector3 currentDestination;


    private void Start()
    {

    }

    public void SpawnNPC(State state)
    {

        if (state == State.Looking)
        {

            // Step 1: Find a random position on the NavMesh for the NPC to spawn
            Vector3 spawnPosition = GetRandomPointOnNavMesh(new Vector3(transform.position.x, transform.position.y + spawnHeight, transform.position.z), minSpawnRadius, maxSpawnRadius);

            if (spawnPosition != Vector3.zero)
            {
                // Step 2: Instantiate the NPC
                GameObject npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
                npc.GetComponent<EnemyVision>().SetPlayer(player);
                npc.GetComponent<EnemyVision>().SetState(state);
                npc.GetComponent<EnemyVision>().StartRoaming(spawnPosition);
                
            }
            else
            {
                Debug.LogError("Failed to find a valid NavMesh position to spawn NPC.");
            }
        }
        else if (state == State.DontMove)
        {
            // Step 1: Find a random position on the NavMesh for the NPC to spawn
            Vector3 spawnPosition = GetRandomPointOnNavMesh(transform.position, 3f, 5f);

            if (spawnPosition != Vector3.zero)
            {
                // Step 2: Instantiate the NPC
                GameObject npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
                npc.GetComponent<EnemyVision>().SetPlayer(player);
                npc.GetComponent<EnemyVision>().SetState(state);
                npc.GetComponent<EnemyVision>().StartRoaming(spawnPosition);

            }
            else
            {
                Debug.LogError("Failed to find a valid NavMesh position to spawn NPC.");
            }
        }
        else if (state == State.Chase)
        {
            // Step 1: Find a random position on the NavMesh for the NPC to spawn
            Vector3 spawnPosition = GetRandomPointOnNavMesh(transform.position, 3f, 5f);

            if (spawnPosition != Vector3.zero)
            {
                // Step 2: Instantiate the NPC
                GameObject npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
                npc.GetComponent<EnemyVision>().SetPlayer(player);
                npc.GetComponent<EnemyVision>().SetState(state);
                npc.GetComponent<EnemyVision>().StartRoaming(spawnPosition);

            }
            else
            {
                Debug.LogError("Failed to find a valid NavMesh position to spawn NPC.");
            }
        }
        else if (state == State.Here)
        {
            // Step 1: Find a random position on the NavMesh for the NPC to spawn
            // Step 1: Find a random position on the NavMesh for the NPC to spawn
            Vector3 spawnPosition = GetRandomPointOnNavMesh(new Vector3(transform.position.x, transform.position.y + spawnHeight, transform.position.z), minSpawnRadius, maxSpawnRadius);

            if (spawnPosition != Vector3.zero)
            {
                // Step 2: Instantiate the NPC
                GameObject npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
                npc.GetComponent<EnemyVision>().SetPlayer(player);
                npc.GetComponent<EnemyVision>().SetState(state);
                npc.GetComponent<EnemyVision>().StartRoaming(spawnPosition);

            }
            else
            {
                Debug.LogError("Failed to find a valid NavMesh position to spawn NPC.");
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
                    if(!(hit.position.x > 5.5f && hit.position.y > 3.5f))
                        if(hit.position.y < 7f)
                            return hit.position;
                }
            }
        }

        return Vector3.zero;  // Return zero vector if no valid point found
    }




    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + spawnHeight, transform.position.z), maxSpawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + spawnHeight, transform.position.z), minSpawnRadius);
    }
}
