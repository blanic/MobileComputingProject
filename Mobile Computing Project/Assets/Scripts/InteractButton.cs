using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //PLAYER
    private GameObject player;

    //PORTAL EXIT
    [SerializeField] private GameObject door;

    private void Start()
    {
        //Initialize Variable
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //BUTTON PRESSED
    public void OnPointerDown(PointerEventData eventData)
    {
        player.GetComponent<ItemCollector>().InteractButtonPressed();
        player.GetComponent<PlayerWins>().InteractButtonPressed();
        door.GetComponent<LockOpener>().InteractButtonPressed();
    }

    //BUTTON RELEASED
    public void OnPointerUp(PointerEventData eventData)
    {
        player.GetComponent<ItemCollector>().InteractButtonReleased();
        player.GetComponent<PlayerWins>().InteractButtonReleased();
        door.GetComponent<LockOpener>().InteractButtonReleased();
    }
}
