//-------------------------------------------------------------------------------
// Filename:        MainMenuMusic.cs
//
// Description:     Controls the music on the main menu. It allocates the 
//                  slider's value to the music mixer's volume. The music mixer
//                  is responsible for the volume of all of the music in the 
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

public class MainMenuMusic : MonoBehaviour
{

    //-------------------------------------------------------------------------------
    // Music Slider
    //-------------------------------------------------------------------------------
    [LabelOverride("Music Volume Slider")]
    [Tooltip("Stores the slider for the music volume.")]
    public Slider sliMusicSlider = null;

    //-------------------------------------------------------------------------------
    // Music Audio Source
    //-------------------------------------------------------------------------------
    [LabelOverride("Music Audio Source")]
    [Tooltip("Stores the Audio source for the music.")]
    public AudioSource MusicAudioSource = null;

    //-------------------------------------------------------------------------------
    // Audio Mixer for Music
    //-------------------------------------------------------------------------------
    [LabelOverride("MusicMixer")]
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

    //-------------------------------------------------------------------------------
    // Use this for initialization
    //-------------------------------------------------------------------------------
    void Start()
    {
        sliMusicSlider.value = Global.MusicVolume;
    }


    // Update is called once per frame
    void Update()
    {
        // The music's volume = the music slider.
        Global.MusicVolume = sliMusicSlider.value;
        // Converting the float to decibels.
        float fMusicVolumeDB = LinearToDecibel(Global.MusicVolume);
        // Set the float of the music volume to the db value.
        m_audmixMixer.audioMixer.SetFloat("MusicVolume", fMusicVolumeDB);
    }
}
