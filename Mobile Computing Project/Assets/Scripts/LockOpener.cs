using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOpener : MonoBehaviour
{
    //BOX COLLIDERS OF THE DOOR
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private BoxCollider2D trigger;
    
    //Animator
    private Animator animator;

    //Audio
    [SerializeField] private AudioSource doorOpeningSound;
    [SerializeField] private AudioSource doorClosedSound;

    //BOOL
    private bool isColliding = false;
    private bool buttonPressed = false;

    //Player
    private GameObject player;

    private void Start()
    {
        //Variables Initialization
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    //Checks if the player enter in collision with the door and calls TryOpenDoor
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;
            TryOpenDoor();
        }
    }

    //Checks when the player leaves the collision area of the door
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
    
    //Set interact button as pressed and call TryOpenDoor method
    public void InteractButtonPressed()
    {
        buttonPressed = true;
        TryOpenDoor();
    }

    //Set interact button as released
    public void InteractButtonReleased()
    {
        buttonPressed = false;
    }

    //Calls the method thet opens the door if player is  lose to the door and the interact button is pressed
    private void TryOpenDoor()
    {
        if (isColliding && buttonPressed)
        {
            OpenDoor();
        }
    }

    //Open the door if the player has the key
    private void OpenDoor()
    {
        //Open Door
        if (player.GetComponent<ItemCollector>().showKey())
        {
            animator.SetTrigger("unlocked");
            PlayOpeningSound();
        }
        //Play door locked sound
        else
        {
            doorClosedSound.Play();
        }
    }

    //Door opening sound
    private void PlayOpeningSound()
    {
        doorOpeningSound.Play();
    }

    //Disable boxcolliders once the door is opened
    private void DisableBoxCollidersAndAnimator()
    {
        collider.enabled = false;
        trigger.enabled = false;
    }
}
