using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    GameObject teamMate;
    PlayerHealth playerHealth;
    TeamMateHealth teamMateHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    bool teamMateInRange;
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        teamMate = GameObject.FindGameObjectWithTag("TeamMate");
        playerHealth = player.GetComponent <PlayerHealth> ();
        teamMateHealth = teamMate.GetComponent<TeamMateHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
        else if(other.gameObject == teamMate)
        {
            teamMateInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }

        else if(other.gameObject == teamMate)
        {
            teamMateInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            AttackPlayer ();
        }

        else if(timer >= timeBetweenAttacks && teamMateInRange && enemyHealth.currentHealth > 0)
        {
            AttackTeamMate();
        }
        
        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void AttackPlayer ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }

    void AttackTeamMate()
    {
        timer = 0f;

        if (teamMateHealth.currentHealth > 0)
        {
            teamMateHealth.TakeDamage(attackDamage);
        }
    }
}
