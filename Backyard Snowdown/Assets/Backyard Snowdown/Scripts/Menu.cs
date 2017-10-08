using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Menu : MonoBehaviour
{
    bool bEscapeToggle = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Player scpPlayer = gameObject.GetComponent<Player>();
        //if (Input.GetKeyDown(KeyCode.Escape) || (XCI.GetButtonDown(XboxButton.Start, scpPlayer.controller)))
        //{
        //    if (bEscapeToggle)
        //    {
        //        Time.timeScale = 0;
        //        bEscapeToggle = false;
        //    }
        //    else if (!bEscapeToggle)
        //    {
        //        Time.timeScale = 1;
        //        bEscapeToggle = true;
        //    }
        //}
    }
}
