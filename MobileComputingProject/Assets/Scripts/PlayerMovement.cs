using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour
{
    //Animations Management
    [SerializeField] private enum MovementState {idle, walk, jump, fall}
    private MovementState state;


    //Magic Numbers
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float moveValue = 7f;

    //Terrain Layer Mask used in IsGrounded() to verify if the player has landed on the ground and can jump again
    [SerializeField] private LayerMask jumpableGround;

    //Useful Player Components
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    //The current direction of the player on the x axe
    private float dirX = 0f;

    //Player Attack Point
    [SerializeField] private Transform attackPoint;

    //Player Audio
    [SerializeField] private AudioSource walkSound1;
    [SerializeField] private AudioSource walkSound2;
    [SerializeField] private AudioSource jumpAndLandSound;
    
    //JOYSTICK
    [SerializeField] private Joystick joystick;

    // Start is called before the first frame update
    // We use start to inizialize all the player components we need
    void Start()
    {
        //Variables Initialization
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() 
    {
        //Walk 
        dirX = joystick.Horizontal;
        rb.velocity = new Vector2(dirX * moveValue, rb.velocity.y);

        //Jump
        if ((joystick.Vertical > .3f) && IsGrounded())
        {
            jumpAndLandSound.Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //Calles method to update animation
        UpdateAnimation();

    }
    
    //Updates Animation
    private void UpdateAnimation()
    {
        //If the player is moving on the horizontal axe, set animation state variable on walk
        //Depending on the direction flips the sprite
        if (dirX > 0f)
        {
            state = MovementState.walk;
            spriteRenderer.flipX = false;
            attackPoint.position = new Vector3(transform.position.x + 2, attackPoint.position.y, attackPoint.position.z);

        }
        else if (dirX < 0f)
        {
            state = MovementState.walk;
            spriteRenderer.flipX = true;
            attackPoint.position = new Vector3(transform.position.x - 2, attackPoint.position.y, attackPoint.position.z);

        }
        //If the player is not moving set the animation state variable on idle
        else
        {
            state = MovementState.idle;
        }

        //If player is moving on the vertical axe, set the animation state variable on jump/fall (overwriting privious assignments of the variable
        if(rb.velocity.y > .2f)
        {
            state = MovementState.jump;
        }
        else if(rb.velocity.y < -.2f)
        {
            state = MovementState.fall;
        }

        //UPDATE ANIMATION
        animator.SetInteger("state", ((int)state));
    }

    //Check if the player has landed on the ground
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }


    //METHODS THAT TRIGGER SOUND
    private void WalkingSound1()
    {
        walkSound1.Play();
    }

    private void WalkingSound2()
    {
        walkSound2.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            jumpAndLandSound.Play();
        }
    }

    private void JumpingAndLandingSound()
    {
        jumpAndLandSound.Play();
    }

    private void PlayerReadyToPlay()
    {
        state = MovementState.idle;
        animator.SetInteger("state", (int)state);
    }
}
