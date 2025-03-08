using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBookHead : MonoBehaviour
{
    public float attackRange;
    public Transform playerTrans;
    public float detectionRadius;
    public float roamRange;
    public float stopChaseDistance;
    private NavMeshAgent navAgent;
    private Animator animator;
    private Vector3 startPOS;
    private string action;

    public float damage;
    public GameObject Player;
    public GameObject Managers;
    AudioSource audioSrc;
    public void Awake()
    {

        Player = GameObject.FindWithTag("Player");

        Managers = GameObject.FindWithTag("Managers");
        playerTrans = Player.transform;
    }


    public void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startPOS = transform.position;
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {

        audioSrc.Play();

        animator.SetBool("Roam", true);
        animator.SetBool("Chase", false);
        animator.SetBool("Attack", false);

        float distanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);
        //Debug.Log("Distance to player: " + distanceToPlayer + " detectionradius : " + detectionRadius);

        if (distanceToPlayer < detectionRadius)
        {
            ChasePlayer();
            //action = "chase"; 
        }
        else
        {
            action = "roam";

            RoamAround();
        }

    }


    private void RoamAround()
    {

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
            //Debug.Log(hit.position);  

        }


    }

    private void ChasePlayer()
    {
        navAgent.isStopped = false;


        float distanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);

        if (distanceToPlayer <= attackRange)
        {
            animator.SetBool("Roam", false);
            animator.SetBool("Chase", false);

            animator.SetBool("Attack", true);
            action = "Attack";
            //Debug.Log("PAS OP");

            // Player.GetComponent<PlayerStats>().TakeDamage(damage); attack wordt te vaak gecontroleerd -> te snel geen health meer
            // Make the zombie face the player
            Vector3 directionToPlayer = (playerTrans.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            navAgent.isStopped = true;


        }
        else
        {
            action = "chase";
            animator.SetBool("Chase", true);
            animator.SetBool("Roam", false);
            animator.SetBool("Attack", false);

            Vector3 directionToPlayer = (playerTrans.position - transform.position).normalized;
            Vector3 stoppingPoint = playerTrans.position - directionToPlayer * attackRange;

            navAgent.isStopped = false;
            navAgent.SetDestination(stoppingPoint);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("PAS OP");
        if (other.CompareTag("Player") && action == "Attack")
        {
            // Debug.Log("DAMAGE");
            Managers.GetComponent<PlayerStats>().TakeDamage(damage);


        }
    }


}