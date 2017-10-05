using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class SnowMan : MonoBehaviour
{
    //-----------------
    // Ability SnowMan
    //-----------------
    public GameObject m_SnowMan = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //--------------------------------------------------------
    // SnowMan, creates a snowman infront of the player
    //--------------------------------------------------------
    public void CreateSnowMan()
    {
        Player scpPlayer = gameObject.GetComponent<Player>();
        
        if (Input.GetKeyDown(KeyCode.LeftShift) || XCI.GetButtonDown(XboxButton.RightBumper, scpPlayer.controller))
        {
            GameObject copy = Instantiate(m_SnowMan);
            copy.transform.position = transform.position + transform.forward;
        }
    }
}
