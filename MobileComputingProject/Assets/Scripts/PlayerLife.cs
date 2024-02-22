using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    //Player Components
    private Animator animator;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource damagedSound;

    //GAME MANAGER
    [SerializeField] private GameObject gameManager;

    //DEAD BOOL
    private bool isDead;

    // Start is called before the first frame update
    private void Start()
    {
        //Variable Initialization
        animator = GetComponent<Animator>();

        //Makes Sure Player Layer collides with Traps and Enemies layers (It is set TRUE when the player dies)
        // 8 = PLAYER LAYER
        // 3 = ENEMIES LAYER
        // 0 = DEFAULT LAYER (where Traps are)
        Physics2D.IgnoreLayerCollision(8, 3, false);
        Physics2D.IgnoreLayerCollision(8, 0, false);
    }

    //If player collides with something, it checks if it is an enemy or a trap
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //UNUSED, ENEMIES DON'T HARM THE PLAYER ON COLLISION ANYMORE
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Hit();
        //}

        //If the player collides with traps he dies
        if (collision.gameObject.CompareTag("Traps"))
        {
            Die();
        } 
    }

    //Player dies
    private void Die()
    {
        //Disables collisions between player layer and enemies and traps layers
        Physics2D.IgnoreLayerCollision(8, 3);
        Physics2D.IgnoreLayerCollision(8, 0);

        animator.SetTrigger("death");
        deathSound.Play();  

        //Player can't move after death
        GetComponent<PlayerMovement2>().enabled = false;
    }

    //Triggers player damaged sound and animation OR triggers death if player run out of lives
    public void Hit()
    {
        GetComponent<ItemCollector>().UseFlower();
       
        //Player finished lives
        if (GetComponent<ItemCollector>().ShowFlowers() < 0)
        {
            Die();
        }

        //Player is only damaged
        else
        {
            animator.SetTrigger("damaged");
            damagedSound.Play();
        }
    }

    //Ask to restart level if player died
    private void RestartLevel()
    {
        gameManager.GetComponent<GameManager>().RestartLevel();
    }

}
