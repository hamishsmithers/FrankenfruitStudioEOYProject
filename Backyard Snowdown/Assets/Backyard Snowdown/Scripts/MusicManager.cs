//-------------------------------------------------------------------------------
// Filename:        MusicManager.cs
//
// Description:     This script controls the music for the main scene. It 
//                  chooses a song randomly from the Music array then plays it.
//                      
// Author:          Nathan Nette
// Editors:         Nathan Nette
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

    //-------------------------------------------------------------------------------
    // Array to store all of the different songs for the background music.
    //-------------------------------------------------------------------------------
    [Tooltip("The array hold all of the songs to be randomly selected from.")]
    public AudioClip[] Music;

    //-------------------------------------------------------------------------------
    // Music Source
    //-------------------------------------------------------------------------------
    [LabelOverride("Music Source")]
    [Tooltip("The audio source that the music will be played from.")]
    public AudioSource MusicSource;

    //-------------------------------------------------------------------------------
    // Audio Mixer for Sound Effects
    //-------------------------------------------------------------------------------
    [LabelOverride("MusicMixer")]
    [Tooltip("The audio mixer titled Music.")]
    public AudioMixerGroup m_audmixMixer = null;


    // Use this for initialization
    void Start ()
    {
        // Allocates the AudioSource class into the MusicSource.
        MusicSource = gameObject.GetComponent<AudioSource>();

        // An int that randomly selects from the Music array.
        int Selector = Random.Range(0, Music.Length);

        // Sets the music source's clip to the randomly selected clip.
        MusicSource.clip = Music[Selector];

        // Play the current clip in the music source.
        MusicSource.Play();
	}

    //-------------------------------------------------------------------------------
    // Function that turns a float to decibels
    //
    //  Param: 
    //      linear:
    //          The slider float value
    //-------------------------------------------------------------------------------  
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
    // Update is called once per frame
    //-------------------------------------------------------------------------------  
    void Update ()
    {
        //MusicSource.volume = Global.MusicVolume;
        float fMusicVolumeDB = LinearToDecibel(Global.MusicVolume);
        m_audmixMixer.audioMixer.SetFloat("MusicVolume", fMusicVolumeDB);
    }
}
