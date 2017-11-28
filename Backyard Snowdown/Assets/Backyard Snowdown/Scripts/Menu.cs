//-------------------------------------------------------------------------------
// Filename:        Menu.cs
//
// Description:     Pauses time when a player pauses the game.
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Mitchell Cattini-Schultz
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Menu : MonoBehaviour
{
    public XboxController m_controller;

    ////----------------------------------------------
    //// Player GameObject.
    ////----------------------------------------------
    //[LabelOverride("Player")]
    //[Tooltip("Stores a Player GameObject.")]
    //public GameObject m_GoPlayer;

    //----------------------------------------------
    // A bool to check whether esc has been pressed.
    //----------------------------------------------
    private bool m_bEscapeToggle = false;

    //--------------------------------------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------------------------------------
    void Start()
    {

    }

    //--------------------------------------------------------------------------------------
    // Update is called once per frame
    //--------------------------------------------------------------------------------------
    void Update()
    {
        //Player scpPlayer = player.gameObject.GetComponent<Player>();
        if (Input.GetKeyDown(KeyCode.Escape) || (XCI.GetButtonDown(XboxButton.Start, m_controller)))
        {
            if (m_bEscapeToggle)
            {
                Time.timeScale = 0;
                m_bEscapeToggle = false;
            }
            else if (!m_bEscapeToggle)
            {
                Time.timeScale = 1;
                m_bEscapeToggle = true;
            }
        }
    }
}
