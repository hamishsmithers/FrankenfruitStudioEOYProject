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
    public MovieTexture movie;

    //-------------------------------------------------------------------------------
    //Time until loading main menu.
    //-------------------------------------------------------------------------------
    public float timeToDelay;

    //-------------------------------------------------------------------------------
    // An audiosource to play the sound effect of the splat.
    //-------------------------------------------------------------------------------
    private AudioSource audSource;

    //-------------------------------------------------------------------------------
    // Use this for initialization
    //-------------------------------------------------------------------------------
    void Start()
    {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audSource = GetComponent<AudioSource>();
        audSource.clip = movie.audioClip;
        movie.Play();
        audSource.Play();
    }

    //-------------------------------------------------------------------------------
    // Update is called once per frame
    //-------------------------------------------------------------------------------
    void Update()
    {
        // If the splashscreen is over, change to main menu scene.
        if (Time.time > timeToDelay)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
