using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class ZombieAI : MonoBehaviour // reset de component voor changes 
{
    public enum Zone
    {
        None,      // Not in any zone
        Zone1,     // Zone 1, where the zombie roams and chases
        Zone2,     // Zone 2 (do nothing here)
        Zone3      // Zone 3 (do nothing here)
    }

    //public Zone currentZone = Zone.None;  
    public Transform playerTrans;             
    public float detectionRadius = 50;   
    public float roamRange = 100;         
    public float stopChaseDistance = 20; 
    private NavMeshAgent navAgent;
    private Animator animator; 
    private Vector3 startPOS;
    private string action;
    //private bool exitedZone = false;
    //public Transform startZone1;
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
        // Default zone is None, which means the zombie doesn't do anything yet.
        animator = GetComponent<Animator>();
         startPOS = transform.position;
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Debug.Log(action);
        //if (currentZone == Zone.Zone1)
        audioSrc.Play();
        //{
            animator.SetBool("Roam", true);
            animator.SetBool("Chase", false);
            animator.SetBool("Attack", false);

            float distanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);
            //Debug.Log("Distance to player: " + distanceToPlayer + " detectionradius : " + detectionRadius);

            if (distanceToPlayer < detectionRadius)
            {
                ChasePlayer();
                //Debug.Log("chasing");
                //action = "chase"; 2 opties
            }
            else
            {
                action = "roam";

                RoamAround();
            }
        //}
        //else
        //{
        //    // Stop animations when outside Zone 1
           
        //    navAgent.isStopped = true;
        //}
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
            //Debug.Log(hit.position);  

        }


    }

    private void ChasePlayer()
    {
        navAgent.isStopped = false;

        float attackRange = 30; 

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
    //private void OnTriggerStay(Collider other) // te snel dood, ook als  zombie nog niet echt slaat maar wel attack modus is al damage
    //{
    //    Debug.Log("PAS OP");
    //    if (other.CompareTag("Player") && action == "Attack")
    //    {

    //        other.GetComponent<PlayerStats>().TakeDamage(damage);

    //    }
    //}

    private void OnTriggerEnter(Collider other) // je kan bij de zombie blijven staan en geen damage krijge -> opl : hele tijd als triggeris
    {
        // Debug.Log("PAS OP");
        if (other.CompareTag("Player") && action == "Attack")
        {
            // Debug.Log("DAMAGE");
            Managers.GetComponent<PlayerStats>().TakeDamage(damage);
            

        }
    }


    //private void OnTriggerExit(Collider other)
    //{ // 2 colliders?, timer is niet consistent
    //    if (!other.CompareTag("Zone1") && !exitedZone)
    //    {
    //        exitedZone = true;
    //        animator.SetBool("Roam", true); 
    //        animator.SetBool("Attack", false);
    //        animator.SetBool("Chase", false);

    //        currentZone = Zone.Zone1;
    //        Debug.Log("exit zone1");
    //        Debug.Log("go to " + startPOS);

    //        //navAgent.isStopped = false;
    //        navAgent.SetDestination(startPOS);
    //        // probleem als chase of attack erna is wod de zombie 'afgeleid'
    //    }



    //}




}
