using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
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

    public void Kill()
    {
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Character")
        {
            Destroy(gameObject);
        }
    }
}
