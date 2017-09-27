using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
//    //--------------------------------------------------------
//    // Speed
//    //--------------------------------------------------------
//    public static int Speed = 1000;

    //--------------------------------------------------------
    // Score
    //--------------------------------------------------------
    public static int nScoreValue = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Character")
        {

        }
    }
}
