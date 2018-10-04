using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    Transform teamMate;
    PlayerHealth playerHealth;
    TeamMateHealth teamMateHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        teamMate = GameObject.FindGameObjectWithTag("TeamMate").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        teamMateHealth = teamMate.GetComponent<TeamMateHealth>();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && (playerHealth.currentHealth > 0||teamMateHealth.currentHealth >0))
        {
            if(playerHealth.currentHealth == 0)
            {
                nav.SetDestination(teamMate.position);
            }

            else if(teamMateHealth.currentHealth == 0)
            {
                nav.SetDestination(player.position);
            }

            else if(playerHealth.currentHealth > teamMateHealth.currentHealth && teamMateHealth.currentHealth!=0)
            {
                nav.SetDestination(teamMate.position);    
            }
            else if(playerHealth.currentHealth <= teamMateHealth.currentHealth && playerHealth.currentHealth != 0)
            {
                nav.SetDestination(player.position);
            }


        }
        else
        {
            nav.enabled = false;
        }
    }
}
