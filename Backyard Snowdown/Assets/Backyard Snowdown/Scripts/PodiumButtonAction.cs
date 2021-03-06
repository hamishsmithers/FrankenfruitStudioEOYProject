﻿//------------------------------------------------------------------------------------------
// Filename:        PodiumButtonAction.cs
//
// Description:     This code is just to go to the main menu when the a or start button is
//                  pressed.
//
// Author:          Nathan Nette
// Editors:         Nathan Nette
//------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class PodiumButtonAction : MonoBehaviour
{

    public XboxController m_controller;

    //------------------------------------------------------------------------------------------
    // Use this for initialization, called when the script is first accessed.
    //------------------------------------------------------------------------------------------
    void Start()
    {

    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame.
    //------------------------------------------------------------------------------------------
    void Update()
    {
        // If the A or Start button is pressed, set scene to main menu.
        if (XCI.GetButtonDown(XboxButton.Start, m_controller) || XCI.GetButton(XboxButton.A, m_controller))
        {
            SceneManager.LoadScene("Main Menu");
        }
        // If the Escape or Space key is pressed, set the scene to the main menu.
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
