using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    private AudioSource sound;

    // Start is called before the first frame update
    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    //Trigger sound on collision with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (sound != null)
        {
            sound.Play();   
        }
    }
}
