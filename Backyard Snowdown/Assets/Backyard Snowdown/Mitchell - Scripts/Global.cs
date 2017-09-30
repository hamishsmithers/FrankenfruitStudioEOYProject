using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {

    static string strControl;

    //-------------------------------
    // Use Xbox controls or keyboard
    //-------------------------------
    static public bool bXboxControls = false;
    static public bool bKeyboardControls = true;

    // Use this for initialization
    void Start ()
    {
        bKeyboardControls = true;
        strControl = "xbox";
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnGUI()
    {
        if (GUI.Button(new Rect(1760, 850, 150, 50), "Use " + strControl + " controls"))
        {
            bXboxControls = !bXboxControls;
            bKeyboardControls = !bKeyboardControls;

            if (strControl == "xbox")
                strControl = "keyboard";
            else
                strControl = "xbox";
        }
    }

}
