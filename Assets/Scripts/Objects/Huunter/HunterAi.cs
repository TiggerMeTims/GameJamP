using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HunterAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 15f;

    [Header("Damage")]
    [SerializeField] private float minDamagePerSecond = 2f;
    [SerializeField] private float maxDamagePerSecond = 20f;

    [Header("Wandering")]
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderDelay = 4f;

    private NavMeshAgent agent;
    private float wanderTimer;
    private float distance;


    [Header("Audio")]
    [SerializeField] private AudioSource audioController;
    [SerializeField] private AudioClip audioClipChase;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = wanderDelay;


        Debug.Log("Agent enabled: " + agent.enabled);
        Debug.Log("Is on NavMesh: " + agent.isOnNavMesh);
        Debug.Log("Path Status: " + agent.pathStatus);
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        distance = Vector3.Distance(transform.position, player.position);

        //Debug.Log(distance);

        if (distance <= detectionRange)
        {
            // Chase player
            agent.SetDestination(player.position);

            // Damage player
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController != null)
            {
                if(audioController != null)
                    PlayClipSounds.Instance.PlayAudio(audioController, audioClipChase, true);

                // 0 = edge of range, 1 = touching hunter
                float closeness = 1f - (distance / detectionRange);

                // Makes damage increase much faster when close
                closeness = Mathf.Pow(closeness, 2f);

                float damagePerSecond = Mathf.Lerp(
                    minDamagePerSecond,
                    maxDamagePerSecond,
                    closeness
                );

                playerController.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
        else
        {   
            wanderTimer += Time.deltaTime;

            if (wanderTimer >= wanderDelay)
            {
                Wander();
                wanderTimer = 0f;
            }
        }
    }

    private void Wander()
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