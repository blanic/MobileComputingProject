using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.gameObject.SetActive(false);
        }
    }
}
