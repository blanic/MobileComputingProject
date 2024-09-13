using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class VolumeSettingsManager : MonoBehaviour
{
    //UI ELEMENTS
    public Slider volumeSlider;
    public Toggle volumeToggle;
    public Text VolumeToggle_Label;
    
    //AUDIO MIXER
    public AudioMixer audioMixer;

    //MAGIC NUMBERS
    private static float MAX_VOLUME = 1f;
    private static float MIN_VOLUME = 0.0001f;
    private static float LAST_VOLUME = 1f;
    
    //IS SOUND OFF
    private static bool CURRENT_AUDIOTOGGLE = false;

    //UI MESSAGE
    private static String LABEL_SOUND_ON = "Sound On";
    private static String LABEL_SOUND_OFF = "Sound Off";

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = GetVolume();
        volumeToggle.isOn = GetVolumeToggleStatus();
    }

    //Change Volume
    public void SetNewVolumeLevel(float volume)
    {
        if(volumeToggle.isOn == true)
        {
            volumeToggle.isOn = false;
            VolumeToggle_Label.text = LABEL_SOUND_ON;
        }

        LAST_VOLUME = volume;
        SetVolume(volume);
    }

    //Turn audio on/off
    public void ToggleVolume(bool toggle)
    {
        if (toggle)
        {
           
            CURRENT_AUDIOTOGGLE = true;
            SetVolume(MIN_VOLUME);
            VolumeToggle_Label.text = LABEL_SOUND_OFF;
        }
        else
        {
            CURRENT_AUDIOTOGGLE = false;
            SetVolume(LAST_VOLUME);
            VolumeToggle_Label.text = LABEL_SOUND_ON;
        }

    }

    //SET NEW VOLUME LEVEL
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*20);
    }

    // UI UPDATE METHODS
    public float GetVolume()
    {
        return LAST_VOLUME;
    }

    public bool GetVolumeToggleStatus()
    {
        return CURRENT_AUDIOTOGGLE;
    }

}
