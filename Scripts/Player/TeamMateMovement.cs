using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMateMovement : MonoBehaviour {

    public float rotationSpeed = 5f;
    public Transform target;
    public float distanceToPlayer;
    public float thresholdDistance = 7.5f;

    Transform player;
    PlayerHealth playerHealth;
    TeamMateHealth TMHealth;
    EnemyManager enemyManager;

     

    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        TMHealth = GetComponent<TeamMateHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();

    }


    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (enemyManager.getNumberOfEnemies() > 0)
        {
            target = enemyManager.enemies[ClosestTarget()];
        }

        if (TMHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {

            if (distanceToPlayer >= thresholdDistance)
            {
                if(nav.isStopped == true)
                {
                    nav.isStopped = false;
                }
                nav.SetDestination(player.position);
            }
            else
            {
                nav.isStopped = true;
                if(target == null)
                {
                    RotateTowards(player);    
                }

            }
            if (target != null)
            {
                nav.updateRotation = false;
                RotateTowards(target);
            }
        }


        else
        {
            nav.enabled = false;
        }
    }

    int ClosestTarget()
    {
        Vector3 offset = transform.position - enemyManager.enemies[0].position;
        float leastSqrDist = offset.sqrMagnitude;
        int minIndex = 0;

        for (int i = 0; i < enemyManager.enemies.Count; i++)
        {
            Vector3 temp = enemyManager.enemies[i].position - transform.position;
            if(temp.sqrMagnitude < leastSqrDist)
            {
                leastSqrDist = temp.sqrMagnitude;
                minIndex = i;
            }
        }
        return minIndex;
    }

    void RotateTowards(Transform Target)
    {
        Vector3 direction = (Target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

}
