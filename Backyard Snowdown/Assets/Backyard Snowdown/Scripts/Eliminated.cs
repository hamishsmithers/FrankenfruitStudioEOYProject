using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eliminated : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void EliminatedPlayer()
    {
        Player scpPlayer = gameObject.GetComponent<Player>();

        if(!scpPlayer.bAlive)
        {
            // Swaps to AI agent.
        }
    }
}

//--------------------------------------------------
// POTENTIAL WAY OF INTEGRATING ELIMINATED STATE
//--------------------------------------------------

// Create an object pool of AI agent characters
//
// if !bAlive
//      Swap the playable character to the AI agent.
//      
//      This AI agent will walk to the swing set using a nav mesh
//      Once it's there, it swaps to the character that swings 
//      The reticle then becomes active 
