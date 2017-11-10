 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {

    //--------------------------------------
    // GameObject for the main menu buttons.
    //--------------------------------------
    [LabelOverride("Main Menu Buttons")][Tooltip("Stores Main Menu Buttons GameObject so that you can hide the buttons. This is for changing between Options and Main Menu.")]
    public GameObject m_goMainMenuButtons = null;

    //-----------------------------------------
    // GameObject for the options menu buttons.
    //-----------------------------------------
    [LabelOverride("Options Buttons")][Tooltip("Stores Options Buttons GameObject so that you can hide the buttons. This is for changing between Options and Main Menu.")]
    public GameObject m_goOptionsButtons = null;

    //--------------------------------------
    // Slider to change by the music volume.
    //--------------------------------------
    [LabelOverride("Music Slider")][Tooltip("Stores a Slider GameObject to control the level of music.")]
    public GameObject m_goMusicSlider = null;

    //----------------------------------------------
    // Button that, when pressed, will start a game.
    //----------------------------------------------
    [LabelOverride("Play Button")][Tooltip("Stores a Button GameObject that is the play button.")]
    public GameObject m_goPlayButton = null;

    //----------------------------------------------------------------------------------
    // The function to randomize the level to be loaded when the play button is pressed.
    //----------------------------------------------------------------------------------
    [LabelOverride("Scenes Array")]
    [Tooltip("Write the names of each main scene in the array boxes, it will randomize loading between the scenes in the array.")]
    public string[] Scenes;
    //--------------------------------------
    // Slider to change by the music volume.
    //--------------------------------------
    //public Slider m_sliMusicSlider;


    //--------------------------------------
    // Use this for initialization
    //--------------------------------------
    void Start ()
    {
        m_goOptionsButtons.SetActive(false);
    }

    //--------------------------------------
    // Update is called once per frame
    //--------------------------------------
    void Update ()
    {

	}


    //--------------------------------------
    //Invoked when a submit button is clicked.
    //--------------------------------------
    public void SubmitSliderSetting()
    {
        //Displays the value of the slider in the console.
        //Debug.Log(m_sliMusicSlider.value);
    }

    private string RandomizeLevel()
    {
        int Selector = Random.Range(0, Scenes.Length);
        string strSceneToLoad = Scenes[Selector].ToString();
        //SceneManager.LoadScene(strSceneToLoad);
        return strSceneToLoad;
    }

    //--------------------------------------------------------------------------------------------
    // PlayGame is the function that occurs when the Play Game button is pressed on the main menu.
    //--------------------------------------------------------------------------------------------
    public void PlayGame()
    {
        SceneManager.LoadScene(RandomizeLevel());
    }

    //-----------------------------------------------------------------------------------------
    // Options is the function that occurs when the Options button is pressed on the main menu.
    //-----------------------------------------------------------------------------------------
    public void Options()
    {
        m_goOptionsButtons.SetActive(true);
        m_goMainMenuButtons.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_goMusicSlider);
    }

    //---------------------------------------------------------------------------------------------
    // Quit Game is the function that occurs when the Quit Game button is pressed on the main menu.
    //---------------------------------------------------------------------------------------------
    public void QuitGame()
    {
        Application.Quit();
    }

    //-----------------------------------------------------------------------------------
    // Back is the function that occurs when the Back button is pressed on the main menu.
    //-----------------------------------------------------------------------------------
    public void Back()
    {
        m_goMainMenuButtons.SetActive(true);
        m_goOptionsButtons.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_goPlayButton);
    }

    //------------------------------------------------------------------------------------
    // Credits is the function that will be triggered when they press the credits button.
    // It takes you to a different scene that runs the credits.
    //------------------------------------------------------------------------------------
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
