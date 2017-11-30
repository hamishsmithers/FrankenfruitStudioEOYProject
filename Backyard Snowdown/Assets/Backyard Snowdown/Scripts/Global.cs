//-------------------------------------------------------------------------------
// Filename:        Global.cs
//
// Description:     Global handles various tasks such as: Spawning the first set
//                  of snowballs and then the death snowballs.
//                  Handles appearing the pause canvas and allow for exiting back
//                  to the main menu.
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Mitchell Cattini-Schultz, Nathan Nette
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;       // Be sure to include this if you want an object to have Xbox input


public class Global : MonoBehaviour
{
    public static float MusicVolume = 0.5f;
    public static float SFXVolume = 0.5f;

    public XboxController controller;

    //--------------------------------------------------------------------------------------
    // GameObject to store the pause canvas.
    //--------------------------------------------------------------------------------------
    [LabelOverride("Pause Canvas")]
    [Tooltip("Stores the canvas for the pause overlay.")]
    public GameObject m_goPauseCanvas = null;

    //--------------------------------------------------------------------------------------
    // GameObject to store the first button to be selected.
    //--------------------------------------------------------------------------------------
    [LabelOverride("First Selected")]
    [Tooltip("This is the first button that will be selected when the pause menu pops up.")]
    public GameObject m_btnFirstButton = null;

    //--------------------------------------------------------------------------------------
    // Snowball
    //--------------------------------------------------------------------------------------
    [LabelOverride("Snowball")]
    [Tooltip("Stores the Snowball GameObject.")]
    public GameObject m_goSnowball = null;

    //--------------------------------------------------------------------------------------
    // Snowballs 
    //--------------------------------------------------------------------------------------
    [LabelOverride("Snowball Gizmo")]
    [Tooltip("Put the snowball gizmo objects in this array.")]
    public List<GameObject> m_goLstSnowballLoc = null;

    //--------------------------------------------------------------------------------------
    // Death Snowballs 
    //--------------------------------------------------------------------------------------
    [LabelOverride("Death Snowball Gizmo")]
    [Tooltip("Put the death snowball gizmo objects in this array.")]
    public List<GameObject> m_goLstDeathSnowballLoc = null;

    //--------------------------------------------------------------------------------------
    // Death Snowball Time
    //--------------------------------------------------------------------------------------
    [LabelOverride("Death Snowball Time")]
    [Tooltip("How long until the death snowballs arrive?")]
    public float m_fDeathSnowballTime = 20.0f;

    private float m_fDeathSnowballCount = 0.0f;
    private bool m_bDeathSnowballsSpawned = false;

    //--------------------------------------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------------------------------------
    void Start()
    {
        //set the menu canvas to hidden
        if (m_goPauseCanvas)
            m_goPauseCanvas.SetActive(false);

        // spawns the first set of snowballs
        if (m_goLstSnowballLoc.Count > 0)
        {
            for (int i = 0; i < m_goLstSnowballLoc.Count; i++)
            {
                GameObject goSnowball = ObjectPool.m_SharedInstance.GetPooledObject();
                // putting the snowballs into the list
                goSnowball.transform.position = m_goLstSnowballLoc[i].transform.position;
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // Update is called once per frame
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // spawns the second set of snowballs
        if (!m_bDeathSnowballsSpawned && m_fDeathSnowballCount < m_fDeathSnowballTime)
        {
            m_fDeathSnowballCount += Time.deltaTime;
        }
        else if (!m_bDeathSnowballsSpawned && m_fDeathSnowballCount > m_fDeathSnowballTime)
        {
            for (int i = 0; i < m_goLstDeathSnowballLoc.Count; i++)
            {
                GameObject goSnowball = ObjectPool.m_SharedInstance.GetPooledObject();
                // putting the snowballs into the list
                goSnowball.transform.position = m_goLstDeathSnowballLoc[i].transform.position;
            }

            m_bDeathSnowballsSpawned = true;
        }

        // wf all these random buttons are pressed the game quits, we are realising we should have removed this from the build.
        if (XCI.GetButton(XboxButton.A, controller) && XCI.GetButton(XboxButton.B, controller) && XCI.GetButton(XboxButton.X, controller) && XCI.GetButton(XboxButton.Y, controller) && XCI.GetButton(XboxButton.LeftBumper, controller) && XCI.GetButton(XboxButton.RightBumper, controller))
            Application.Quit();

        // if start and back are pressed, restart game
        ResetGame();

        if (Input.GetKeyDown(KeyCode.Escape) || XCI.GetButtonDown(XboxButton.Start, controller))
        {
            // if you press start in the main menu or end round the pause menu won't show because it will just return out
            if (SceneManager.GetActiveScene().name == "EndRound" || SceneManager.GetActiveScene().name == "Main Menu")
                return;

            // after start is pressed if the pause canvas is inactive in the hierarchy set it to active
            if (!m_goPauseCanvas.activeInHierarchy)
            {
                m_goPauseCanvas.SetActive(true);
                // game time is frozen
                Time.timeScale = 0;
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_btnFirstButton);
            }
            // after start is pressed if the pause canvas is active in the hierarchy set it to inactive
            else
            {
                m_goPauseCanvas.SetActive(false);
                // game time is unfrozen
                Time.timeScale = 1;
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // Function for when the continue button is pressed in the pause menu.
    //--------------------------------------------------------------------------------------
    public void PauseMenuContinue()
    {
        // Hides the pause canvas and continues the game.
        m_goPauseCanvas.SetActive(false);
        Time.timeScale = 1;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    //--------------------------------------------------------------------------------------
    // Function for when the exit button is pressed in the pause menu.
    //--------------------------------------------------------------------------------------
    public void PauseMenuExit()
    {
        // Exits the current round and takes you back to the main menu.
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    //--------------------------------------------------------------------------------------
    // Resets the game by returning to the Main Menu scene.
    //--------------------------------------------------------------------------------------
    public void ResetGame()
    {
        if (XCI.GetButton(XboxButton.Start, controller) && XCI.GetButton(XboxButton.Back, controller))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
