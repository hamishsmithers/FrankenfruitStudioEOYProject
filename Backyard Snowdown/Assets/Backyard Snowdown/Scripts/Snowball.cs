﻿//------------------------------------------------------------------------------------------
// Filename:        Snowball.cs
//
// Description:     The Snowball is an essential element of the game. 
//                  Players can pick it up, throw it, steal it and it also has 
//                  awesome particle effects!
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Mitchell Cattini-Schultz
//------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    private Rigidbody m_rb;
    //------------------------------------------------------------------------------------------
    // Damage Speed
    //------------------------------------------------------------------------------------------
    [LabelOverride("Damage Speed")]
    [Tooltip("This is the speed that will damage a player when they are hit by it.")]
    public float m_fDamageSpeed = 3.0f;

    //------------------------------------------------------------------------------------------
    // Too Fast
    //------------------------------------------------------------------------------------------
    [LabelOverride("Too Fast")]
    [Tooltip("A bool that checks whether the snowball is moving too fast or not.")]
    public bool m_bTooFast = false;

    //------------------------------------------------------------------------------------------
    // Materials Array
    //------------------------------------------------------------------------------------------
    [LabelOverride("Material Array")]
    [Tooltip("An array to store materials.")]
    public Material[] m_materials;
    private Renderer m_rend;

    //------------------------------------------------------------------------------------------
    // Score Value
    //------------------------------------------------------------------------------------------
    [LabelOverride("Score Value")]
    [Tooltip("A static int that represents how much health a player loses when they are hit by the snowball.")]
    public static int m_nScoreValue = 1;

    //------------------------------------------------------------------------------------------
    // Spark Particle System
    //------------------------------------------------------------------------------------------
    [LabelOverride("Spark Particle System")]
    [Tooltip("Drag and drop the respective particle system onto here.")]
    private ParticleSystem m_ParticleSparks;

    //private Color mainColor = Color.white;
    private MeshRenderer m_mr = null;

    //--------------------------------------------------------------------------------------
    // Use this for initialization, called when the script is first accessed.
    //------------------------------------------------------------------------------------------
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        m_mr = GetComponent<MeshRenderer>();
        // Grabs the particle system which is on the first child.
        m_ParticleSparks = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame.
    //------------------------------------------------------------------------------------------
    void Update()
    {

        //if the ball is moving at a dangerous speed, let the player know!
        if (m_bTooFast)
        {
            // Red material.
            m_mr.material = m_materials[1];
            // plays the particle system sparks.
            m_ParticleSparks.Play();
        }
        else
        {
            // Default material.
            m_mr.material = m_materials[0];
            // Stops the particle system sparks.
            m_ParticleSparks.Stop();
        }

        // If the snowball going too high up, cap it.
        if (m_rb.velocity.y > 5.0f)
        {
            // Doing a quick dirty hack to access the read-me only variable
            // you cannot access a single axis at a time from rigidBody
            // so we have to make a new vector for it.
            Vector3 v3 = m_rb.velocity;
            v3.y = 5.0f;
            m_rb.velocity = v3;
        }

        // If moving too fast the damage is 1.
        if (m_rb.velocity.magnitude >= m_fDamageSpeed)
        {
            m_nScoreValue = 1;
            m_bTooFast = true;
        }
        // If not moving too fast the damage is 0.
        else if (m_rb.velocity.magnitude < m_fDamageSpeed)
        {
            m_nScoreValue = 0;
            m_bTooFast = false;
        }
    }

    //------------------------------------------------------------------------------------------
    // Collision for the snowball object. If the snowball collides with anything that
    // is not the players or the ground it slows the ball a little. If the ball 
    // collides with anything that is not the ground then the snowball hit sound 
    // plays.
    //
    // Param:
    //      col: The col that the snowball hit.
    //------------------------------------------------------------------------------------------
    private void OnCollisionEnter(Collision col)
    {
        // If the snowball collides with non ground objects play hit sound.
        if (col.gameObject.tag != "Ground")
        {
            AudioManager.m_SharedInstance.PlaySnowBallHit();
        }
    }
}