//--------------------------------------------------------------------------------------
// Filename:        MainMenuButtons.cs
//
// Description:     This script hold the functions for all of the main menu 
//                  buttons.
//                      
// Author:          Nathan Nette
// Editors:         Nathan Nette
//--------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    //--------------------------------------------------------------------------------------
    // GameObject for the main menu buttons.
    //--------------------------------------------------------------------------------------
    [LabelOverride("Main Menu Buttons")]
    [Tooltip("Stores Main Menu Buttons GameObject so that you can hide the buttons. This is for changing between Options and Main Menu.")]
    public GameObject m_goMainMenuButtons = null;

    //--------------------------------------------------------------------------------------
    // GameObject for the options menu buttons.
    //--------------------------------------------------------------------------------------
    [LabelOverride("Options Buttons")]
    [Tooltip("Stores Options Buttons GameObject so that you can hide the buttons. This is for changing between Options and Main Menu.")]
    public GameObject m_goOptionsButtons = null;

    //-------------------------------------------------------------------------------
    // Slider to change by the music volume.
    //--------------------------------------------------------------------------------------
    [LabelOverride("Music Slider")]
    [Tooltip("Stores a Slider GameObject to control the level of music.")]
    public GameObject m_goMusicSlider = null;

    //--------------------------------------------------------------------------------------
    // Button that, when pressed, will start a game.
    //--------------------------------------------------------------------------------------
    [LabelOverride("Play Button")]
    [Tooltip("Stores a Button GameObject that is the play button.")]
    public GameObject m_goPlayButton = null;

    //--------------------------------------------------------------------------------------
    // The function to randomize the level to be loaded when the play button is 
    // pressed.
    //--------------------------------------------------------------------------------------
    [Tooltip("Write the names of each main scene in the array boxes, it will randomize loading between the scenes in the array.")]
    public string[] m_strArrScenes;

    //--------------------------------------------------------------------------------------
    // Checking whether the play button is pressed.
    //--------------------------------------------------------------------------------------
    private bool m_PlayPressed;


    //--------------------------------------------------------------------------------------
    // Use this for initialization, called when the script is first accessed.
    //--------------------------------------------------------------------------------------
    void Start()
    {
        m_goOptionsButtons.SetActive(false);
        m_PlayPressed = false;
    }

    //--------------------------------------------------------------------------------------
    // Update is called once per frame.
    //--------------------------------------------------------------------------------------
    void Update()
    {
        if (m_PlayPressed)
            gameObject.GetComponent<MainMenuButtons>().enabled = false;
    }

    //--------------------------------------------------------------------------------------
    // Randomizes which level will be selected to be loaded alongside main_default.
    //--------------------------------------------------------------------------------------
    private string RandomizeLevel()
    {
        // Creates an int to store the selected scene number in the array.
        int Selector = Random.Range(0, m_strArrScenes.Length);
        // Converts the name that's selected to a string.
        string strSceneToLoad = m_strArrScenes[Selector].ToString();
        // Returns the name of the scene to be loaded.
        return strSceneToLoad;
    }

    //--------------------------------------------------------------------------------------
    // PlayGame is the function that occurs when the Play Game button is pressed on the main 
    // menu.
    //--------------------------------------------------------------------------------------
    public void PlayGame()
    {
        m_PlayPressed = true;
        SceneManager.LoadSceneAsync(RandomizeLevel(), LoadSceneMode.Single);
        SceneManager.LoadSceneAsync("Main_Default", LoadSceneMode.Additive);
    }

    //--------------------------------------------------------------------------------------
    // Options is the function that occurs when the Options button is pressed on the 
    // main menu.
    //--------------------------------------------------------------------------------------
    public void Options()
    {
        m_goOptionsButtons.SetActive(true);
        m_goMainMenuButtons.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_goMusicSlider);
    }

    //--------------------------------------------------------------------------------------
    // Quit Game is the function that occurs when the Quit Game button is pressed on the main 
    // menu.
    //--------------------------------------------------------------------------------------
    public void QuitGame()
    {
        Application.Quit();
    }

    //--------------------------------------------------------------------------------------
    // Back is the function that occurs when the Back button is pressed on the main menu.
    //--------------------------------------------------------------------------------------
    public void Back()
    {
        m_goMainMenuButtons.SetActive(true);
        m_goOptionsButtons.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_goPlayButton);
    }

    //--------------------------------------------------------------------------------------
    // Credits is the function that will be triggered when they press the credits 
    // button.
    // It takes you to a different scene that runs the credits.
//--------------------------------------------------------------------------------------
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
