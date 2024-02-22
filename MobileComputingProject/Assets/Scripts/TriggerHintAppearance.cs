using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerHintAppearance : MonoBehaviour
{
    //Hint Panel to activate/disable
    [SerializeField] private GameObject hintUI;

    //Activate the panel when the player enter the area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintUI.SetActive(true);
        }
        
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    //}

    //Disable the panel and itself when the player leaves the area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintUI.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }


}
