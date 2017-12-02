//-------------------------------------------------------------------------------
// Filename:        SplashScreen.cs
//
// Description:     Splashscreen does all of the code that is needed for the
//                  splashscreen to work as desired.
//
// Author:          Emma Wearing
// Editors:         Emma Wearing, Hamish Smithers
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class SplashScreen : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // The splashscreen movie.
    //-------------------------------------------------------------------------------
    public MovieTexture m_movtexMovie;

    //-------------------------------------------------------------------------------
    //Time until loading main menu.
    //-------------------------------------------------------------------------------
    public float m_fTimeToDelay;

    //-------------------------------------------------------------------------------
    // An audiosource to play the sound effect of the splat.
    //-------------------------------------------------------------------------------
    private AudioSource m_audSource;

    //-------------------------------------------------------------------------------
    // Use this for initialization
    //-------------------------------------------------------------------------------
    void Start()
    {
        GetComponent<RawImage>().texture = m_movtexMovie as MovieTexture;
        m_audSource = GetComponent<AudioSource>();
        m_audSource.clip = m_movtexMovie.audioClip;
        m_movtexMovie.Play();
        m_audSource.Play();
    }

    //-------------------------------------------------------------------------------
    // Update is called once per frame
    //-------------------------------------------------------------------------------
    void Update()
    {
        // If the splashscreen is over, change to main menu scene.
        if (Time.time > m_fTimeToDelay)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
