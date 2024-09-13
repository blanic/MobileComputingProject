using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public AudioSource audioSource;  // Attach the Audio Source component here
    public AudioClip uiMusic;  // Assign your UI music here

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object across scenes
            PlayMusic(uiMusic);  // Start the UI music
        }
        else
        {
            Destroy(gameObject);  // Prevent multiple instances
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (audioSource.clip != musicClip)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }
    public void StopMusic()
    {
        audioSource.Stop();
        instance = null;  // Reset the instance, so it can be recreated later
    }
}