 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {

    //-------------------------------------
    // GameObject for the main menu buttons
    //-------------------------------------
    [LabelOverride("Main Menu Buttons")][Tooltip("Stores Main Menu Buttons GameObject so that you can hide the buttons. This is for changing between Options and Main Menu.")]
    public GameObject m_goMainMenuButtons = null;
    //----------------------------------------
    // GameObject for the options menu buttons
    //----------------------------------------
    [LabelOverride("Options Buttons")][Tooltip("Stores Options Buttons GameObject so that you can hide the buttons. This is for changing between Options and Main Menu.")]
    public GameObject m_goOptionsButtons = null;

    public GameObject m_goMusicSlider = null;

    public GameObject m_goPlayButton = null;
    //--------------------------------------------
    // Slider to change by the music volume.
    //--------------------------------------------
    //public Slider m_sliMusicSlider;



    // Use this for initialization
    void Start ()
    {
        m_goOptionsButtons.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting()
    {
        //Displays the value of the slider in the console.
        //Debug.Log(m_sliMusicSlider.value);
    }



    //--------------------------------------------------------------------------------------
    // PlayGame is the function that occurs when the Play Game button is pressed on the main
    // menu.
    //--------------------------------------------------------------------------------------
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Options()
    {
        m_goOptionsButtons.SetActive(true);
        m_goMainMenuButtons.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_goMusicSlider);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        m_goMainMenuButtons.SetActive(true);
        m_goOptionsButtons.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_goPlayButton);
    }
}
