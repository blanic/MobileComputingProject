using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    //Enemy Components
    private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private BoxCollider2D boxColliderTrigger;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource damagedSound;

    //Initial health of the enemy
    [SerializeField] private int maxHealth = 6;
    
    //Current health of the enemy
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        //Variables Initialization
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    //Causes the damage when the enemy is hit and checks if he is dead
    public void TakeDamage(int damage)
    {
        currentHealth -= 1;
        animator.SetTrigger("damaged");
        damagedSound.Play();
        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    //Kills the enemy
    private void Die()
    {
        animator.SetBool("death", true);
        deathSound.Play();
        
        //Make the enemy stop moving and reacting to player presence
        GetComponent<EnemyMovement>().enabled = false;
        boxCollider.enabled = false;
        boxColliderTrigger.enabled = false;

    }
}
