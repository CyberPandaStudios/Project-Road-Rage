using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    /*
     * Author: Brandon, Extended by Bryce
     * Basic enemy AI controller
     * 
     */


    //Personal Mob stats
    public float healthPoints = 200;


    //Aggro radius for checking if the player is close enough
    public float aggroRadius = 20f;
    public float lookRadius;
    //For some enemies aggroRadius != attack radius
    public float attackRadius = 20f;
    //Distance at which enemy stops aggroing on a player and returns to patrol state
    public float deAggroRadius = 120f;
    //Speed that enemy patrols at
    public float patrolSpeed = 2f;
    //Speed that the enemey chases at
    public float followSpeed = 5f;
    
    
    
    
    
    public bool isAggrod = false;
    


    //Patrolling:
    //Point our enemy patrols
    public Vector3 walkPoint;
    //Flag used to start patroling
    public bool isWalkPointSet = false;
    //Determines how big of an area this enemy will patrol
    public float patrolRange = 10f;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    //States
    public bool playerInAggroRange;
    public bool playerInAttackRange;
    public bool playerInDeAggroRange;



    //Target the enemy will go towards
    private Transform target;
    //NavMeshAgent for using Unity's agent system
    NavMeshAgent agent;


    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public string State;


    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        agent.speed = patrolSpeed;

        /*
         * There is a PlayerManager script on the GameManager object that will always hold the player, 
         * this allows scripts to find the player without searching through every instance of an object with the player script
         */

        target = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        /*
         * Get the distance between player and enemy, if distance is within look radius then follow the player as well face the player while moving.
         */
         Debug.Log(State);
        float distance = Vector3.Distance(target.position, transform.position);
        Debug.Log("Distance: " + distance);

        //playerInAggroRange = Physics.CheckSphere(transform.position, aggroRadius, whatIsPlayer);
        playerInAggroRange = distance < aggroRadius ? true : false;
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRadius, whatIsPlayer);
        playerInAttackRange = distance < attackRadius ? true : false;
       // playerInDeAggroRange = Physics.CheckSphere(transform.position, deAggroRadius, whatIsPlayer);
        playerInDeAggroRange = distance < deAggroRadius ? true : false;



        //Debug.Log("Truths: " + playerInAggroRange + ", " + playerInAttackRange + ", " + playerInDeAggroRange);
        if(isAggrod && !playerInDeAggroRange)
        {
            Debug.Log("Mob has been de-aggrod");
            isAggrod = false;
        }
        if(!playerInAggroRange && !isAggrod) Patroling();

        if(playerInAggroRange && playerInAttackRange)
        {
            isAggrod = true;
            AttackPlayer();
        }
        //Because deaggro check happens before this chase conditon, if a player leaves the deaggro range they will not be chased
        //However if you are inside of the deaggro radius but outside of the aggro range with the aggro flag still true
        //You will be chased. 
        //EX: aggro radius 20m and deaggro 50m, but you are 40m away you will still be chased, until 51m away.
        if(playerInAggroRange || isAggrod)
        {
            
            ChasePlayer();
        }

    }

    /*
     * Function to handle enemy rotation when moving toward player
     */

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, Time.deltaTime * 5f);
    }

    //This allows for a visual representation of the look radius of an enemy.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }








    private void Patroling()
    {
        State = "Patrol";
        agent.speed = patrolSpeed;

        if(!isWalkPointSet)
        {
            Debug.Log("Creating new walk point");
            searchWalkPoint();
        }
        if(isWalkPointSet)
        {
            Debug.Log("Destination Set!");
            agent.SetDestination(walkPoint);
        }


        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            Debug.Log("Walkpoint is: " + distanceToWalkPoint.magnitude);
            Debug.Log("Walkpoint failed!");
            isWalkPointSet = false;
        }
    }

    private void searchWalkPoint()
    {
        //Calculate next search point
        float randomZ = Random.Range(-patrolRange, patrolRange);
        float randomX = Random.Range(-patrolRange, patrolRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        isWalkPointSet = true;

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            Debug.Log("Walkpoint = true");
            isWalkPointSet = true;
        }
        else
        {
            Debug.Log("Walkpoint Failed!");

        }
    }
    private void ChasePlayer()
    {
        State = "Chase";
        agent.speed = followSpeed;
        agent.SetDestination(target.position);
    }
    private void AttackPlayer()
    {
        State = "Attack";
        agent.SetDestination(transform.position);
        transform.LookAt(target);
        if(!alreadyAttacked)
        {
            //Attack Code here

            alreadyAttacked = true;
        }
    }

    public void takeDamage(float damage)
    {
        //Take damage
        healthPoints -= damage;
        //Check death
        if(healthPoints < 0)
        {

            //Die
        }
    }


}
