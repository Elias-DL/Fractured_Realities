using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBookHead : MonoBehaviour
{
    public Transform playerTrans;
    public float attackRange;
    public float detectionRadius;
    public float roamRange;
    public float stopChaseDistance;
    private NavMeshAgent navAgent;
    private Animator animator;
    private Vector3 startPOS;
    private string action;
    private bool isDamaging;
    public float damage;
    public GameObject Player;
    public GameObject Managers;
    private float attackDuration = 0.1f; // chech animatie
    AudioSource audioSrc;
    public AudioSource src;
    public AudioClip sfx1;

    public bool coolDown = false;
    public bool enemyGezien;
    public GameObject JumpscareUI;
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
        // Debug.Log(action);
        SoundEffects();
        action = null;

        if (src == null  || JumpscareUI == null)
        {
            src = GetComponent<AudioSource>();
           JumpscareUI = GameObject.FindWithTag("JumpscareUI");

        }
        animator.SetBool("Roam", true);
        animator.SetBool("Chase", false);
        animator.SetBool("Attack", false);

        float distanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);
        //Debug.Log("Distance to player: " + distanceToPlayer + " detectionradius : " + detectionRadius);

        

         if (Player.GetComponent<PlayerMovement>().naamGezien == "BookHeadMonster")
        {
            StartCoroutine(Scared());

            Debug.Log("Scared");
        }
        else
        {
            if (coolDown == false)
            {
                if (Managers.GetComponent<PlayerStats>().Respawning == true)
                {
                    RoamAround();
                    action = "Roam";

                }

                else if (distanceToPlayer <= detectionRadius && action != "Attack")
                {
                   
                        ChasePlayer();


                }
                else if (action != "Attack")
                {
                    action = "Roam";

                    RoamAround();
                }
                else
                {
                    

                   
                        AttackPlayer();
                    action = "Attack";

                }
            }
        }




    }

    IEnumerator Scared()
    {
        coolDown = true;
        RoamAround();
        yield return new WaitForSeconds(10f);

        coolDown = false;
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

    private void AttackPlayer()
    {
        animator.SetBool("Roam", false);
        animator.SetBool("Chase", false);

        animator.SetBool("Attack", true);
        action = "Attack";


        // Make the zombie face the player
        Vector3 directionToPlayer = (playerTrans.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        navAgent.isStopped = true;

        if (isDamaging == false)
        {

            StartCoroutine(Damage());

        }

    }
    private void ChasePlayer()
    {
        navAgent.isStopped = false;


        float distanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);
        //Debug.Log(distanceToPlayer + " , " + attackRange);

        if (distanceToPlayer <= attackRange + 2) // +2 voor veiligheid, anders vaak in de buurt van bv 30 (attack range) maar niet helemaal voor wtv reden
        {

            AttackPlayer();

        }
        else
        {

            animator.SetBool("Chase", true);
            animator.SetBool("Roam", false);
            animator.SetBool("Attack", false);

            Vector3 directionToPlayer = (playerTrans.position - transform.position).normalized;
            Vector3 stoppingPoint = playerTrans.position - directionToPlayer * attackRange;

            navAgent.isStopped = false;
            navAgent.SetDestination(stoppingPoint);
        }
    }


    IEnumerator Damage()

    {
        isDamaging = true;
        Debug.Log("DAMAGE");

        yield return new WaitForSeconds(attackDuration); // damaga na animatie zodat je tijd hebt om weg te lopen
        if (action == "Attack") //als na de animatie speler nog in de buurt is en de action dus nog steeds attack is wel damage doen.
        {
            Managers.GetComponent<PlayerStats>().TakeDamage(damage);

            isDamaging = false;
            JumpscareUI = GetComponent<PlayerStats>().JumpscareUI;

            JumpscareUI.SetActive(true);

        }
        else
        {
            isDamaging = false; // in beide gevallen is de attack animatie gedaan

        }




    }

   
    public void SoundEffects()
    {

        if (action == "roam" && !src.isPlaying)
        {

            src.clip = sfx1;
            src.volume = 1f;
            src.Play();
        }

      

    }
}
