//-------------------------------------------------------------------------------
// Filename:        SoundChecker.cs
//
// Description:     Sound checker checks whether an audio source has finished 
//                  playing. If it has, destroy it. This simply cleans up all of
//                  the audiosources being created during the game.
//
// Author:          Nathan Nette
// Editors:         Nathan Nette
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChecker : MonoBehaviour
{

    //-------------------------------------------------------------------------------
    // Use this for initialization
    //-------------------------------------------------------------------------------
    void Start()
    {

    }

    //-------------------------------------------------------------------------------
    // Update is called once per frame
    //-------------------------------------------------------------------------------
    void Update()
    {
        // If an audio source isn't playing, destroy it.
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
