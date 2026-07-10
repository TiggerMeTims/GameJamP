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


    [Header("Audio")]
    [SerializeField] private PlayAudio audioController;
    [SerializeField] private AudioClip audioClipChase;
    [SerializeField] private AudioClip audioClipWander;

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

            // Damage player
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController != null)
            {
                audioController.StopAudioSource();
                audioController.audioFile = audioClipChase;
                audioController.PlayAudioFile();
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
            /*
            if(!audioController.GetAudioSource().isPlaying)
            {
                audioController.audioFile = audioClipWander;
                audioController.PlayAudioFile();
            }
            */
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