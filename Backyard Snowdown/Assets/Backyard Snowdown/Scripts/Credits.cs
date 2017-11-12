using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Credits : MonoBehaviour {

    public XboxController controller;

    //public GameObject CreditsText = null;

    //private Vector3 CreditTransform;

	// Use this for initialization
	void Start ()
    {
        //CreditTransform.x = 0;
        //CreditTransform.y = 0;
        //CreditTransform.z = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //CreditsText.transform.localPosition = CreditTransform;

        //CreditTransform += Vector3.up * Time.deltaTime * 50;

        if(XCI.GetButtonDown(XboxButton.Start, controller) || XCI.GetButtonDown(XboxButton.A, controller))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }


    
}
