//------------------------------------------------------------------------------------------
// Filename:        Credits.cs
//
// Description:     Credits does all of the functions in the credits scene. Simply, being
//                  able to continue by pressing escape, start or a on the controller.
//
// Author:          Nathan Nette
// Editors:         Nathan Nette
//------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Credits : MonoBehaviour {

    public XboxController controller;

    //------------------------------------------------------------------------------------------
    // Credits Music
    //------------------------------------------------------------------------------------------
    [LabelOverride("Credits Music")]
    [Tooltip("The audio source for the credits music.")]
    public AudioSource m_CreditsMusic = null;

    //------------------------------------------------------------------------------------------
    // Use this for initialization
    //------------------------------------------------------------------------------------------
    void Start ()
    {
        // Setting the music volume to the music volume singleton.
        m_CreditsMusic.volume = Global.m_fMusicVolume;
    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame
    //------------------------------------------------------------------------------------------
    void Update ()
    {
        // The buttons that, when pressed, take you back to the menu.
        if (Input.GetKey(KeyCode.Escape) || XCI.GetButtonDown(XboxButton.Start, controller) || XCI.GetButtonDown(XboxButton.A, controller))
        {
            // Load the Main Menu.
            SceneManager.LoadScene("Main Menu");
        }
    }
}
