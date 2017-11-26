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

    //--------------------------------------
    // GameObject to store the pause canvas.
    //--------------------------------------
    [LabelOverride("Pause Canvas")]
    [Tooltip("Stores the canvas for the pause overlay.")]
    public GameObject m_goPauseCanvas = null;

    //-----------------------------------------------------
    // GameObject to store the first button to be selected.
    //-----------------------------------------------------
    [LabelOverride("First Selected")]
    [Tooltip("This is the first button that will be selected when the pause menu pops up.")]
    public GameObject m_btnFirstButton = null;

    //----------
    // Snowball
    //----------
    [LabelOverride("Snowball")]
    [Tooltip("Stores the Snowball GameObject.")]
    public GameObject m_goSnowball = null;

    //-----------
    // Snowballs 
    //-----------
    [LabelOverride("Snowball Gizmo")]
    [Tooltip("Put the snowball gizmo objects in this array.")]
    public List<GameObject> m_goLstSnowballLoc = null;

    //-----------------
    // Death Snowballs 
    //-----------------
    [LabelOverride("Death Snowball Gizmo")]
    [Tooltip("Put the death snowball gizmo objects in this array.")]
    public List<GameObject> m_goLstDeathSnowballLoc = null;

    public float m_fDeathSnowballTime = 20.0f;
    private float m_fDeathSnowballCount = 0.0f;
    private bool m_bDeathSnowballsSpawned = false;

    ////-------------------------------
    //// Use Xbox controls or keyboard
    ////-------------------------------
    //static public bool bXboxControls = false;
    //static public bool bKeyboardControls = true;


    //----------------------------
    // Use this for initialization
    //----------------------------
    void Start()
    {
        //set the menu canvas to hidden
        if (m_goPauseCanvas)
            m_goPauseCanvas.SetActive(false);

        if (m_goLstSnowballLoc.Count > 0)
        {
            for (int i = 0; i < m_goLstSnowballLoc.Count; i++)
            {
                GameObject goSnowball = ObjectPool.m_SharedInstance.GetPooledObject();
                goSnowball.transform.position = m_goLstSnowballLoc[i].transform.position;
                //Debug.Log("global getting object");
            }
        }
    }

    //---------------------------------
    // Update is called once per frame
    //---------------------------------
    void Update()
    {
        if (!m_bDeathSnowballsSpawned && m_fDeathSnowballCount < m_fDeathSnowballTime)
        {
            m_fDeathSnowballCount += Time.deltaTime;
        }
        else if (!m_bDeathSnowballsSpawned && m_fDeathSnowballCount > m_fDeathSnowballTime)
        {
            for (int i = 0; i < m_goLstDeathSnowballLoc.Count; i++)
            {
                GameObject goSnowball = ObjectPool.m_SharedInstance.GetPooledObject();
                goSnowball.transform.position = m_goLstDeathSnowballLoc[i].transform.position;
            }

            m_bDeathSnowballsSpawned = true;
        }

        if (XCI.GetButton(XboxButton.A, controller) && XCI.GetButton(XboxButton.B, controller) && XCI.GetButton(XboxButton.X, controller) && XCI.GetButton(XboxButton.Y, controller) && XCI.GetButton(XboxButton.LeftBumper, controller) && XCI.GetButton(XboxButton.RightBumper, controller))
            Application.Quit();

        ResetGame();

        if (Input.GetKeyDown(KeyCode.Escape) || XCI.GetButtonDown(XboxButton.Start, controller))
        {
            if (!m_goPauseCanvas.activeInHierarchy)
            {
                m_goPauseCanvas.SetActive(true);
                Time.timeScale = 0;
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_btnFirstButton);
            }
            else
            {
                m_goPauseCanvas.SetActive(false);
                Time.timeScale = 1;
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    public void PauseMenuContinue()
    {
        m_goPauseCanvas.SetActive(false);
        Time.timeScale = 1;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    public void PauseMenuExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void ResetGame()
    {
        if (XCI.GetButton(XboxButton.Start, controller) && XCI.GetButton(XboxButton.Back, controller))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
