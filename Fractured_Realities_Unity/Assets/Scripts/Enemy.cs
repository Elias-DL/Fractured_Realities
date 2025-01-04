using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieAI : MonoBehaviour
{
    public enum Zone
    {
        None,      // Not in any zone
        Zone1,     // Zone 1, where the zombie roams and chases
        Zone2,     // Zone 2 (do nothing here)
        Zone3      // Zone 3 (do nothing here)
    }

    public Zone currentZone = Zone.None;  // Start outside of any zone
    public Transform player;              // The player the zombie will chase
    public float detectionRadius = 10f;   // Radius for detection
    public float roamRange = 15f;         // Maximum roaming range from current position
    public float stopChaseDistance = 15f; // Distance at which the zombie stops chasing the player
    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        // Default zone is None, which means the zombie doesn't do anything yet.
    }

    private void Update()
    {
        // Only move and chase if we are in Zone 1
        if (currentZone == Zone.Zone1)
        {
            // Detect player and chase if in range
            if (Vector3.Distance(transform.position, player.position) < detectionRadius)
            {
                ChasePlayer();
            }
            else
            {
                RoamAround();
            }
        }
        // If outside of Zone 1, stop everything and remain still
        else
        {
            navAgent.isStopped = true;
        }
    }

    private void RoamAround()
    {
        // Check if the agent is already moving to a destination, if not, pick a random spot to roam to
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            // Generate a random point within the roaming range
            Vector3 randomDirection = Random.insideUnitSphere * roamRange;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, roamRange, NavMesh.AllAreas))
            {
                navAgent.isStopped = false;
                navAgent.SetDestination(hit.position);
            }
        }
    }

    private void ChasePlayer()
    {
        navAgent.isStopped = false;  // Ensure the zombie isn't stopped while chasing
        navAgent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Enter Zone 1 and start roaming or chasing
        if (other.CompareTag("Zone1"))
        {
            currentZone = Zone.Zone1;
        }
        // Exit Zone 1, stop roaming and chasing
        else if (other.CompareTag("Zone2") || other.CompareTag("Zone3"))
        {
            currentZone = Zone.None;  // Zombie is out of Zone 1, do nothing
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Exit Zone 1 and stop moving
        if (other.CompareTag("Zone1"))
        {
            currentZone = Zone.None;  // Zombie is out of Zone 1, stop everything
            navAgent.isStopped = true;
        }
    }
}
