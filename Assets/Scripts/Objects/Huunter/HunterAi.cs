using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HunterAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Detection")]
    public float detectionRange = 15f;

    [Header("Wandering")]
    public float wanderRadius = 10f;
    public float wanderDelay = 4f;

    private NavMeshAgent agent;
    private float wanderTimer;

    void Start()
    {
    agent = GetComponent<NavMeshAgent>();
    wanderTimer = wanderDelay;

    Debug.Log("Agent enabled: " + agent.enabled);
    Debug.Log("Is on NavMesh: " + agent.isOnNavMesh);
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // Chase player
            agent.SetDestination(player.position);
        }
        else
        {
            // Wander
            wanderTimer += Time.deltaTime;

            if (wanderTimer >= wanderDelay)
            {
                Wander();
                wanderTimer = 0f;
            }
        }
    }

    void Wander()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}