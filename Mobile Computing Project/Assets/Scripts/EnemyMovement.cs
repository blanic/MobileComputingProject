using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyMovement : MonoBehaviour
{
    //WAYPOINTS
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    //MOVING SPEED
    [SerializeField] private float speed = 2f;

    //TARGET
    private GameObject target;

    //Enemy Components
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private EnemyCombat enemyCombat;

    //AGGRO BOOL
    private bool aggroActive = false;

    //CURRENT DIRECTION
    int direction = 0;  //0 == RIGHT | 1 == LEFT

    private void Start()
    {
        //Variables Initialization
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyCombat = gameObject.GetComponent<EnemyCombat>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        //If AGGRO is FALSE move between waypoints
        if (aggroActive == false)
        {
            //If Enemy has reached the current waypoint, he will select the next one.
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            {
                currentWaypointIndex++;
                // If all waypoints have been reached, he will start over
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }

            //Flip the Sprite depending of which waypoint is selected
            if (currentWaypointIndex == 0)
            {
                spriteRenderer.flipX = true;
                direction = 1;
            }
            else
            {
                spriteRenderer.flipX = false;
                direction = 0;
            }

            //Move towards the current waypoint
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
        //If AGGRO is TRUE the enemy will move towards the target
        else
        {
            //Flip the Sprite depending on target position
            if (target.transform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
                direction = 1;
            }
            else
            {
                spriteRenderer.flipX = false;
                direction = 0;
            }

            //Move towards the player
            if (Vector2.Distance(target.transform.position, transform.position) > 0.2f)
            {
                Vector3 newPosition = transform.position;
                newPosition.x = Mathf.MoveTowards(transform.position.x, target.transform.position.x, Time.deltaTime * speed);
                transform.position = newPosition;
            }
        }
        //Update enemy attack point 
        enemyCombat.UpdateAttackPoint(direction);
    }

    //AGGRO TRUE
    public void ActivateAggro()
    {
        aggroActive = true;
    }

    //AGGRO FALSE
    public void DectivateAggro()
    {
        aggroActive = false;
    }
}
