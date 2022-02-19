using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    /*
     * Author: Brandon
     * Basic enemy AI controller
     * 
     */

    //A look radius for checking if the player is close enough
    public float lookRadius = 10f;

    //Target the enemy will go towards
    private Transform target;
    //NavMeshAgent for using Unity's agent system
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();

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
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
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
}
