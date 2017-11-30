//-------------------------------------------------------------------------------
// Filename:        MainMenuSFX.cs
//
// Description:     Controls the sfx on the main menu. It allocates the 
//                  slider's value to the sfx mixer's volume. The sfx mixer
//                  is responsible for the volume of all of the sfx in the 
//                  game.
//                      
// Author:          Nathan Nette
// Editors:         Nathan Nette
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuSFX : MonoBehaviour {

    //-------------------------------------------------------------------------------
    // SFX Slider
    //-------------------------------------------------------------------------------
    [LabelOverride("SFX Volume Slider")]
    [Tooltip("Stores the slider for the sfx volume.")]
    public Slider sliSFXSlider = null;

    //-------------------------------------------------------------------------------
    // SFX Audio Source
    //-------------------------------------------------------------------------------
    [LabelOverride("SFX Audio Source")]
    [Tooltip("Stores the audio source for the sfx.")]
    public AudioSource SFXAudioSource = null;

    //-------------------------------------------------------------------------------
    // Button Selected
    //-------------------------------------------------------------------------------
    [LabelOverride("Button Selected")]
    [Tooltip("Stores the audio clip for when a button is selected.")]
    public AudioClip buttonSelected = null;

    //-------------------------------------------------------------------------------
    // Button Clicked
    //-------------------------------------------------------------------------------
    [LabelOverride("Button Clicked")]
    [Tooltip("Stores the audio clip for when a button is clicked.")]
    public AudioClip buttonClicked = null;

    //-------------------------------------------------------------------------------
    // Button Back
    //-------------------------------------------------------------------------------
    [LabelOverride("Button Back")]
    [Tooltip("Stores the audio clip for when a back button is clicked.")]
    public AudioClip buttonBack = null;


    //--------------------------------------
    // Audio Mixer for Sound Effects
    //--------------------------------------
    [LabelOverride("SFX Mixer")]
    [Tooltip("The audio mixer titled Music.")]
    public AudioMixerGroup m_audmixMixer = null;


    //------------------------------------------------------------------------------------------
    // Function that turns a float to decibels
    //
    //  Param: 
    //      linear:
    //          The slider float value
    //------------------------------------------------------------------------------------------
    private float LinearToDecibel(float linear)
    {
        // A float for decibels.
        float dB;

        // if the parsed in float is not equal to 0, do this.
        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);

        // Otherwise set it to -144
        else
            dB = -144.0f;

        // return the decibel value.
        return dB;
    }

    //------------------------------------------------------------------------------------------
    // Use this for initialization
    //------------------------------------------------------------------------------------------
    void Start()
    {
        // Sets the initial value of the slider to be the SFXVolume value.
        sliSFXSlider.value = Global.SFXVolume;
    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame
    //------------------------------------------------------------------------------------------
    void Update()
    {
        // The sfx's volume = the sfx slider.
        Global.SFXVolume = sliSFXSlider.value;
        // Converting the float to decibels.
        float fSFXVolumeDB = LinearToDecibel(Global.SFXVolume);
        // Set the float of the SFX volume to the db value.
        m_audmixMixer.audioMixer.SetFloat("SFXVolume", fSFXVolumeDB);
        
    }
    //---------------------------------------------------------------------------------------------
    // When a button is selected, set the audio source's clip to button selected clip and play it.
    //---------------------------------------------------------------------------------------------
    public void OnButtonSelect()
    {
        //change audiosource clip to select and play.
        SFXAudioSource.clip = buttonSelected;
        SFXAudioSource.Play();
    }

    //---------------------------------------------------------------------------------------------
    // When a button is clicked, set the audio source's clip to be buttonClicked and play it.
    //---------------------------------------------------------------------------------------------
    public void OnButtonClick()
    {
        //change audiosource clip to select and play.
        SFXAudioSource.clip = buttonClicked;
        SFXAudioSource.Play();
    }

    //---------------------------------------------------------------------------------------------
    // When a bacck button is clicked, set the audio source's clip to be buttonBack and play it.
    //---------------------------------------------------------------------------------------------
    public void OnButtonBack()
    {
        //change audiosource clip to select and play.
        SFXAudioSource.clip = buttonBack;
        SFXAudioSource.Play();
    }
}
