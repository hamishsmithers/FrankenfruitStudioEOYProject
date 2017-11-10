using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;       // Be sure to include this if you want an object to have Xbox input


public class Global : MonoBehaviour
{
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
    //--------------------
    // Snowball Location 1
    //--------------------
    [LabelOverride("Snowball Location 1")]
    [Tooltip("Stores the Snowball Location 1 GameObject.")]
    public GameObject m_SnowballLoc1 = null;
    //--------------------
    // Snowball Location 2
    //--------------------
    [LabelOverride("Snowball Location 2")]
    [Tooltip("Stores the Snowball Location 2 GameObject.")]
    public GameObject m_SnowballLoc2 = null;


    //-------------------------------
    // 
    //-------------------------------

    //static string strControl;

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

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject goSnowball = ObjectPool.m_SharedInstance.GetPooledObject();
            goSnowball.transform.position = m_SnowballLoc1.transform.position;
            goSnowball = ObjectPool.m_SharedInstance.GetPooledObject();
            goSnowball.transform.position = m_SnowballLoc2.transform.position;
        }

        //GameObject goEventSystem = GameObject.Find("EventSystem");
        //Event scpevntsys = goEventSystem.GetComponent<Event>();

        //bKeyboardControls = true;
        //strControl = "xbox";

        // list then when the list is full, wait a second then change to the endround scene.

        // 
    }

    //---------------------------------
    // Update is called once per frame
    //---------------------------------
    void Update()
    {

        if (XCI.GetButton(XboxButton.A, controller) && XCI.GetButton(XboxButton.B, controller) && XCI.GetButton(XboxButton.X, controller) && XCI.GetButton(XboxButton.Y, controller) && XCI.GetButton(XboxButton.LeftBumper, controller) && XCI.GetButton(XboxButton.RightBumper, controller))
            Application.Quit();

        ResetGame();


        if (Input.GetKeyDown(KeyCode.Escape) || XCI.GetButtonDown(XboxButton.Start, controller))
        {
            if (!m_goPauseCanvas.activeInHierarchy)
            {
                //SceneManager.LoadScene(0);
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
        // if(List.Size == 4)
        //  timer for a second
        // 
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
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetGame()
    {
        //Player scpPlayer = gameObject.GetComponent<Player>();
        //Debug.Log(fResetTimer);
        //if (XCI.GetButton(XboxButton.LeftStick, scpPlayer.controller) && XCI.GetButtonDown(XboxButton.RightStick, scpPlayer.controller) || XCI.GetButtonDown(XboxButton.LeftStick, scpPlayer.controller) && XCI.GetButton(XboxButton.RightStick, scpPlayer.controller))
        if (XCI.GetButton(XboxButton.Start, controller) && XCI.GetButton(XboxButton.Back, controller))// || XCI.GetButtonDown(XboxButton.Start, controller) && XCI.GetButton(XboxButton.Back, controller))
        {
            //Debug.Log("START");
            //fResetTimer += Time.deltaTime;

            //if (fResetTimer >= fResetConfirmTime)
            //{

            SceneManager.LoadScene("MainMenu");
            //   fResetTimer = 0.0f;
            //}
        }
        //else { fResetTimer = 0.0f; }
    }




    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(1760, 850, 150, 50), "Use " + strControl + " controls"))
    //    {
    //        bXboxControls = !bXboxControls;
    //        bKeyboardControls = !bKeyboardControls;

    //        if (strControl == "xbox")
    //            strControl = "keyboard";
    //        else
    //            strControl = "xbox";
    //    }
    //}

}
