//-------------------------------------------------------------------------------
// Filename:        p1Animation.cs
//
// Description:     When a player throws a snowball this function is called on
//                  frame specified by the animation event, this toggles a bool
//                  which lets the player shoot a snowball.
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Mitchell Cattini-Schultz
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p1Animation : MonoBehaviour
{
    //--------------------------------------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------------------------------------
    void Start()
    {

    }

    //--------------------------------------------------------------------------------------
    // Update is called once per frame
    //--------------------------------------------------------------------------------------
    void Update()
    {

    }

    //--------------------------------------------------------------------------------------
    // When the animation event is triggered this function is called releasing the snowball 
    // from the players clutches.
    //--------------------------------------------------------------------------------------
    public void ThrowBall()
    {
        Player scpPlayer = GetComponentInParent<Player>();
        // releases the players grasp on the snowball
        scpPlayer.m_bThrowBall = true;
    }
}
