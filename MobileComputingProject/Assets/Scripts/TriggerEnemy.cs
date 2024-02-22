using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    //The enemies triggered by the object when the player enter the area
    [SerializeField] private GameObject[] enemies;

    //Activates the aggro of the enemies 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyMovement>().ActivateAggro();
            }
        }
    }

    //Dectivates the aggro of the enemies when the player leaves the area
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyMovement>().DectivateAggro();
            }
        }
    }
}
