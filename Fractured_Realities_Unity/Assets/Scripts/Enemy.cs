using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieAI : MonoBehaviour // reset de component voor changes 
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
    public float detectionRadius = 50;   // Radius for detection
    public float roamRange = 100;         // Maximum roaming range from current position
    public float stopChaseDistance = 20; // Distance at which the zombie stops chasing the player
    private NavMeshAgent navAgent;
    private Animator animator; // Animator reference


    private string action;
    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        // Default zone is None, which means the zombie doesn't do anything yet.
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(action);
        if (currentZone == Zone.Zone1)

        {
            animator.SetBool("Roam", true);
            animator.SetBool("Chase", false);
            animator.SetBool("Attack", false);

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            //Debug.Log("Distance to player: " + distanceToPlayer + " detectionradius : " + detectionRadius);

            if (distanceToPlayer < detectionRadius)
            {
                ChasePlayer();
                //Debug.Log("chasing");
                action = "chase";
            }
            else
            {
                action = "roam";

                RoamAround();
            }
        }
        else
        {
            // Stop animations when outside Zone 1
           
            navAgent.isStopped = true;
        }
    }


    private void RoamAround()
    {
        //Debug.Log("roam");
        animator.SetBool("Roam", true);
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
        animator.SetBool("Chase", true);

        navAgent.isStopped = false;
        navAgent.SetDestination(player.position);

        // Attack if close enough
        if (Vector3.Distance(transform.position, player.position) < 20f) // Attack range
        {
            animator.SetBool("Attack", true);
        }


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
            animator.SetBool("Idle", true); // Set to Idle animation

        }
    }


}
