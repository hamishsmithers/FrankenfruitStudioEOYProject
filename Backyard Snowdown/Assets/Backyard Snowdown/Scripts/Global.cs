using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;       // Be sure to include this if you want an object to have Xbox input


public class Global : MonoBehaviour
{
    public XboxController m_Controller;

    public GameObject m_GoPauseCanvas = null;
    public GameObject m_BtnFirstButton = null;

    private float m_fResetTimer = 0.0f;
    public float m_fResetConfirmTime = 3.0f;

    //static string strControl;

    ////-------------------------------
    //// Use Xbox controls or keyboard
    ////-------------------------------
    //static public bool bXboxControls = false;
    //static public bool bKeyboardControls = true;

    // Use this for initialization
    void Start()
    {
        //set the menu canvas to hidden
        if (m_GoPauseCanvas)
            m_GoPauseCanvas.SetActive(false);


        //GameObject goEventSystem = GameObject.Find("EventSystem");
        //Event scpevntsys = goEventSystem.GetComponent<Event>();





        //bKeyboardControls = true;
        //strControl = "xbox";

        // list then when the list is full, wait a second then change to the endround scene.

        // 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || XCI.GetButton(XboxButton.A, m_Controller) && XCI.GetButton(XboxButton.B, m_Controller) && XCI.GetButton(XboxButton.X, m_Controller) && XCI.GetButton(XboxButton.Y, m_Controller) && XCI.GetButton(XboxButton.LeftBumper, m_Controller) && XCI.GetButton(XboxButton.RightBumper, m_Controller))
            Application.Quit();

        ResetGame();


        if (XCI.GetButtonDown(XboxButton.Start, m_Controller))
        {
            if (!m_GoPauseCanvas.activeInHierarchy)
            {
                //SceneManager.LoadScene(0);
                m_GoPauseCanvas.SetActive(true);
                Time.timeScale = 0;
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_BtnFirstButton);
            }
            else
            {
                m_GoPauseCanvas.SetActive(false);
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
        m_GoPauseCanvas.SetActive(false);
        Time.timeScale = 1;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    public void PauseMenuExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ResetGame()
    {
        //Player scpPlayer = gameObject.GetComponent<Player>();
        //Debug.Log(fResetTimer);
        //if (XCI.GetButton(XboxButton.LeftStick, scpPlayer.controller) && XCI.GetButtonDown(XboxButton.RightStick, scpPlayer.controller) || XCI.GetButtonDown(XboxButton.LeftStick, scpPlayer.controller) && XCI.GetButton(XboxButton.RightStick, scpPlayer.controller))
        if (XCI.GetButton(XboxButton.Start, m_Controller) && XCI.GetButton(XboxButton.Back, m_Controller))// || XCI.GetButtonDown(XboxButton.Start, controller) && XCI.GetButton(XboxButton.Back, controller))
        {
            //Debug.Log("START");
            //fResetTimer += Time.deltaTime;

            //if (fResetTimer >= fResetConfirmTime)
            //{

            SceneManager.LoadScene(0);
            //   fResetTimer = 0.0f;
            //}
        }
        //else { fResetTimer = 0.0f; }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
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
