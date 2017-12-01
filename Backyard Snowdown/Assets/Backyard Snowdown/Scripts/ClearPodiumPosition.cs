//------------------------------------------------------------------------------------------
// Filename:        ClearPodiumPosition.cs
//
// Description:     Clear Podium Position simply clears the array holding the order that the
//                  players died in. This is called on the menu so that the array is wiped
//                  ready for another game.
//
// Author:          Nathan Nette
// Editors:         Nathan Nette
//------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPodiumPosition : MonoBehaviour {

    //------------------------------------------------------------------------------------------
    // Use this for initialization
    //------------------------------------------------------------------------------------------
    void Start()
    {
        // Resets the array that stores the player's win order.
        ScoreManager.Reset();
    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame
    //------------------------------------------------------------------------------------------
    void Update()
    {

    }
}
