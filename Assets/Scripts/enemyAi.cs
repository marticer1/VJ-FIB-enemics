using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAi : MonoBehaviour
{

    public NavMeshAgent agent;

    public GameObject player;

    public LayerMask whatIsEnemyGround, whatIsPlayer;

    public Animator myAnim;

    public float health;
    public float armor;

    public int attackDamage;

    public AudioSource atacar;
    public AudioSource herido;
    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Start() {
        
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        
        if (gameObject.tag == "EnemyMelee") {
            
            walkPointRange = 30f;
            sightRange = 70f; 
            attackRange = 7.5f;
            timeBetweenAttacks = 1f;
            health = 50f;
            armor = 100f;
            attackDamage = 25;
        }
        if (gameObject.tag == "EnemyRange") {
            
            walkPointRange = 30f;
            sightRange = 120f; 
            attackRange = 40f;
            timeBetweenAttacks = 5f;
            health = 30f;
            armor = 50f;
            attackDamage = 30;
        }
        if (gameObject.tag == "EnemyBoss") {
            
            walkPointRange = 30f;
            sightRange = 300f; 
            attackRange = 20f;
            timeBetweenAttacks = 3f;
            health = 150f;
            armor = 300f;
            attackDamage = 50;
        }
        

    }

    private void Update() {
        
        playerInSightRange = Physics.CheckBox(transform.position, new Vector3(sightRange, 40, sightRange), Quaternion.identity, whatIsPlayer);
        playerInAttackRange = Physics.CheckBox(transform.position, new Vector3(attackRange, 40, attackRange), Quaternion.identity, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();

    }

    private void Patroling() {

        if (!walkPointSet) {
            SearchWalkPoint();
            myAnim.SetBool("isWalking", false);

        }
        if (walkPointSet) {
            agent.SetDestination(walkPoint);
            myAnim.SetBool("isWalking", true);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint() {

        //Calculate random point in range
        float randomAngle = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(150 * Mathf.Sin(randomAngle), transform.position.y, 150 * Mathf.Cos(randomAngle)); 

        if (Physics.Raycast(walkPoint, -transform.up, 4f, whatIsEnemyGround))
            walkPointSet = true;


    }
    private void ChasePlayer() {

        agent.SetDestination(player.transform.position);
        myAnim.SetBool("isWalking", true);

    }
    private void AttackPlayer() {

        agent.SetDestination(transform.position);
        myAnim.SetBool("isWalking", false);

        transform.LookAt(player.transform);

        if (!alreadyAttacked) {

            //Attack
            myAnim.SetTrigger("isAttacking");
            atacar.Play();
            if (gameObject.tag == "EnemyMelee" || gameObject.tag == "EnemyBoss") {

                if (!player.GetComponent<playerScript>().invulnerable)
                {
                    player.GetComponent<playerScript>().takedamage(attackDamage);
                }
                    

            }
            if (gameObject.tag == "EnemyRange") {

                Debug.Log("Shoot");
                
            }
            alreadyAttacked = true;
           
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
    }

    private void ResetAttack() {

        alreadyAttacked = false;
        myAnim.SetTrigger("isAttacking");

    }

    public void TakeDamage(int damage) {

        health -= damage;
        herido.Play();
        if (health <= 0)
        {
            myAnim.SetTrigger("muerto");
            Invoke("muerte",2f);

        }
    }
    private void muerte()
    {
        Destroy(gameObject);
    }

}
