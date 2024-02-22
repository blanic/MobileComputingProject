using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    
    //Enemy Components
    private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private AudioSource attackSound;

    //Magic Numbers
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRate = 4f;
    [SerializeField] float nextAttackTime = 0f;

    //Player Info
    private Transform targetTransform;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        //Initialization of variables
        animator = GetComponent<Animator>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //Triggers Attack animation and audio
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("attack");
            attackSound.Play();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("attack");
            attackSound.Play();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    //Called by ANIMATOR EVENT
    //Check if player is in range and cause damage
    private void Attack()
    {
        //Detect if player is in range
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D hitObject in hit)
        {
            if (hitObject.gameObject.CompareTag("Player"))
            {
                //Cause damage
                hitObject.GetComponent<PlayerLife>().Hit();
            }
            
       }
    }
    //Visual rapresentation of the range area
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
