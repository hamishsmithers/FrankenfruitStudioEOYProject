using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;       // Be sure to include this if you want an object to have Xbox input


public class Global : MonoBehaviour
{
    private float fResetTimer = 0.0f;
    public float fResetConfirmTime = 3.0f;

    
    //static string strControl;

    ////-------------------------------
    //// Use Xbox controls or keyboard
    ////-------------------------------
    //static public bool bXboxControls = false;
    //static public bool bKeyboardControls = true;

    // Use this for initialization
    void Start()
    {
        //bKeyboardControls = true;
        //strControl = "xbox";

        // list then when the list is full, wait a second then change to the endround scene.
        
        // 
    }

    // Update is called once per frame
    void Update()
    {
        ResetGame();

        // if(List.Size == 4)
        //  timer for a second
        // 
    }

    public void ResetGame()
    {
        //Player scpPlayer = gameObject.GetComponent<Player>();

        //if (XCI.GetButton(XboxButton.LeftStick, scpPlayer.controller) && XCI.GetButtonDown(XboxButton.RightStick, scpPlayer.controller) || XCI.GetButtonDown(XboxButton.LeftStick, scpPlayer.controller) && XCI.GetButton(XboxButton.RightStick, scpPlayer.controller))
        //{
        //    fResetTimer += Time.deltaTime;

        //    if (fResetTimer >= fResetConfirmTime)
        //    {
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //        fResetTimer = 0.0f;
        //    }
        //}
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
