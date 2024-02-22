using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    //Animations Management
    [SerializeField] private enum CheckPointState { turnedOff, startUp, turnDown, idle, save};
    CheckPointState state;
    //Checkpoint Components
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        state = CheckPointState.turnedOff;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            state = CheckPointState.startUp;
            animator.SetInteger("state", ((int)state));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown("s"))
        {
            state = CheckPointState.save;
            animator.SetInteger("state", ((int)state));
            //Add save logic
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            state = CheckPointState.turnDown;
            animator.SetInteger("state", ((int)state));
        }
    }

    private void SetToIdle()
    {
        state = CheckPointState.idle;
        animator.SetInteger("state", ((int)state));
    }

    private void SetToTurnedOff()
    {
        state = CheckPointState.turnedOff;
        animator.SetInteger("state", ((int)state));
    }
}
