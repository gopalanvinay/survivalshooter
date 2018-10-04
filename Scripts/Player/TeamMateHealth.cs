using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class TeamMateHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public AudioClip deathClip;


    Animator anim;
    AudioSource TMAudio;
    TeamMateMovement TMMovement;
    TeamMateShooting teamMateShooting;
    bool isDead;


    void Awake()
    {
        anim = GetComponent<Animator>();
        TMAudio = GetComponent<AudioSource>();
        TMMovement = GetComponent<TeamMateMovement>();
        teamMateShooting = GetComponentInChildren<TeamMateShooting>();
        currentHealth = startingHealth;
    }


    public void TakeDamage(int amount)
    {

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        TMAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        teamMateShooting.DisableEffects();

        anim.SetTrigger("Die");

        TMAudio.clip = deathClip;
        TMAudio.Play();

        TMMovement.enabled = false;
        teamMateShooting.enabled = false;
    }
}
