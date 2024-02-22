using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    //Player Components
    private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private AudioSource attackSound;

    //MAGIC NUMBERS
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] float nextAttackTime = 0f;

    //ATTACK BUTTON
    [SerializeField] private Button attackButton;

    private void Start()
    {
        //Variable Initialization
        animator = GetComponent<Animator>();

        //If the attack button is pressed triggers the attack
        attackButton.onClick.AddListener(AttackButtonPressed);
    }

    //Triggers Audio and Animation of the attack
    private void AttackButtonPressed()
    {
        if (Time.time >= nextAttackTime)
        {
                animator.SetTrigger("attack");
                attackSound.Play();
                nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    //Called by ANIMATION EVENT
    //Checks if the attack has hit enemies, if enemies are hit it damages them
    private void Attack()
    {
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damages them
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyLife>().TakeDamage(attackDamage);
            }
        }
    }

    //Visual Rapresentation of the range area
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
