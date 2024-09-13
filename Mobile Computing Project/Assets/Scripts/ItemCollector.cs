using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    //UI
    [SerializeField] private Text flowersCount;      //flowers number
    [SerializeField] private GameObject keyPanel;    //key

    //Player Components
    private Animator animator;
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private AudioSource collectKeySound;

    //BOOL Collecting Conditions
    private bool isColliding = false;
    private bool buttonPressed = false;

    //Player Has Key?
    private bool hasKey = false;

    //Flowers currently owned
    private static int flowers = 0;

    private int STARTING_FLOWERS = 0;

    //Collition currently happening with key or flower
    private Collider2D currentCollision;

    private void Start()
    {
        animator = GetComponent<Animator>();
        flowersCount.text = flowers.ToString();
    }

    
    //If player in collision with flower or key try collect
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flower") || collision.gameObject.CompareTag("Key"))
        {
            isColliding = true;
            currentCollision = collision;
            TryCollect();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flower") || collision.gameObject.CompareTag("Key"))
        {
            isColliding = false;
            currentCollision = null;
        }
    }

    //If button pressed try collect
    public void InteractButtonPressed()
    {
        buttonPressed = true;
        TryCollect();
    }
    public void InteractButtonReleased()
    {
        buttonPressed = false;
    }

    //if player is colliding with flower or key and interact button is pressed collect
    private void TryCollect()
    {
        if (isColliding && buttonPressed)
        {
            Collect();
        }
    }

    //Collect the object in collision with the player
    public void Collect()
    {
        animator.SetTrigger("pickUp");
        if (currentCollision.gameObject.CompareTag("Flower"))
        {
            collectSound.Play();
            flowers++;
            flowersCount.text = flowers.ToString();
        }

        else if (currentCollision.gameObject.CompareTag("Key"))
        {
            collectKeySound.Play();
            hasKey = true;
            keyPanel.SetActive(true);
        }
        Destroy(currentCollision.gameObject);
    }

    public int ShowFlowers()
    {
        return flowers;
    }

    public void UseFlower()
    {
        flowers = flowers - 1;
        flowersCount.text = flowers.ToString();
      
    }

    public void ResetFlowers()
    {
        flowers = STARTING_FLOWERS;
    }

    public bool showKey()
    {
        return hasKey;
    }
}
