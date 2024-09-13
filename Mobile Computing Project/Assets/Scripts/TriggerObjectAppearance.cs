using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectAppearance : MonoBehaviour
{
    //GAME OBJECT
    [SerializeField] private GameObject objectToMaterialize;

    //Enables the object when the player enter the area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(objectToMaterialize != null)
            {
                objectToMaterialize.SetActive(true);
            }
        }
    }

}
