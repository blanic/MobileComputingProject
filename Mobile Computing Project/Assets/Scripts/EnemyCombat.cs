using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static System.TimeZoneInfo;

public class EnemyCombat : MonoBehaviour
{
    
    //Enemy Components
    private Animator animator;
    public Transform attackPoint;
    [SerializeField] public Transform attackPoint_Right;
    [SerializeField] public Transform attackPoint_Left;
    [SerializeField] private AudioSource attackSound;

    //Magic Numbers
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRate = 4f;
    [SerializeField] float nextAttackTime = 0f;

    //Player Info
    private Transform targetTransform;
    [SerializeField] private LayerMask playerLayer;

    private bool hasHit = false;

    private void Start()
    {
        //Initialization of variables
        animator = GetComponent<Animator>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        attackPoint = attackPoint_Right;
    }

    //Triggers Attack animation and audio
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("attack");
            attackSound.Play();
            nextAttackTime = Time.time + Random.Range(2f, 6f);
        }
    }

    //If player stays close to the enemy, it keeps attacking
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("attack");
            attackSound.Play();
            //nextAttackTime = Time.time + 1f / attackRate;
            nextAttackTime = Time.time + Random.Range(2f, 6f);
        }
    }

    //Called by ANIMATOR EVENT
    //Check if player is in range and cause damage
    private void Attack()
    {
        if (!hasHit)
        {
            //Detect if player is in range
            Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            foreach (Collider2D hitObject in hit)
            {
                if (hitObject.gameObject.CompareTag("Player"))
                {
                    hasHit = true;
                    //Cause damage
                    hitObject.GetComponent<PlayerLife>().Hit();
                }
            }
        }
        else
        {
            StartCoroutine(Wait());
            hasHit = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }


    //Updates the enemy's attack point based on the given direction
    //0 == RIGHT | 1 == LEFT 
    public void UpdateAttackPoint(int direction)
    {
        if(direction == 0)
        {
            attackPoint = attackPoint_Right;
        }
        else
        {
            attackPoint = attackPoint_Left;
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
