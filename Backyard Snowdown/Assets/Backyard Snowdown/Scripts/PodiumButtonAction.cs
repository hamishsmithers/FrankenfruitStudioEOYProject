using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class PodiumButtonAction : MonoBehaviour {

    public XboxController controller;


    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (XCI.GetButtonDown(XboxButton.Start, controller) || XCI.GetButton(XboxButton.A, controller))
        {
            SceneManager.LoadScene(0);
        }
    }
}
