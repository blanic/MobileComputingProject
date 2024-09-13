using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortal : MonoBehaviour
{

    private Animator animator;
    [SerializeField] private AudioSource activatePortal;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //Play Sound and Animation of the Portal
    public void Activate()
    {
        activatePortal.Play();
        animator.SetTrigger("playerIn");
    }
}
