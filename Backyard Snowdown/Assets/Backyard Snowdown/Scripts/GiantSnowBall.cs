//--------------------------------------------------------------------------------------
// Filename:        GiantSnowBall.cs
//
// Description:     GiantSnowBall is used to apply knockback to players.
//                  When the jumping snowman lands this script is utilized.
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Mitchell Cattini-Schultz
//--------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSnowBall : MonoBehaviour
{
    //--------------------------------------------------------------------------------------
    // Knockback Force
    //--------------------------------------------------------------------------------------
    [LabelOverride("Knockback Force")]
    [Tooltip("The force that is applied to knockback when hit by giant snowball.")]
    public float m_fKnockbackForce = 0.0f;
    
    //--------------------------------------------------------------------------------------
    // Area Of Effect
    //--------------------------------------------------------------------------------------
    [LabelOverride("Area of Effect")]
    [Tooltip("A float to change the area effect of the giant snowball.")]
    public float m_fAreaOfEffect = 3.0f;

    //--------------------------------------------------------------------------------------
    // Impact Particle System
    //--------------------------------------------------------------------------------------
    [LabelOverride("Impact Particle System")]
    [Tooltip("Drag and drop the respective particle system onto here.")]
    public GameObject m_goImpactParticles;

    //--------------------------------------------------------------------------------------
    // Impact Cloud Particle System
    //--------------------------------------------------------------------------------------
    [LabelOverride("Impact Cloud Particle System")]
    [Tooltip("Drag and drop the respective particle system onto here.")]
    public GameObject m_goImpactParticlesCloud;

    private ParticleSystem m_psCloud;
    private ParticleSystem m_psSnowflake;


    //--------------------------------------------------------------------------------------
    // Use this for initialization, called when the script is first accessed.
    //--------------------------------------------------------------------------------------
    void Start()
    {
        // assigning the Impact particle system to the variable psSnowflake
        m_psSnowflake = m_goImpactParticles.GetComponent<ParticleSystem>();
        // assigning the Impact particle system to the variable psCloud
        m_psCloud = m_goImpactParticlesCloud.GetComponent<ParticleSystem>();
    }

    //--------------------------------------------------------------------------------------
    // Update is called once per frame.
    //--------------------------------------------------------------------------------------
    void Update()
    {
    }

    //--------------------------------------------------------------------------------------
    // Collision for the jumping snowman.
    //
    // Param:
    //      other: The object that the Jumping Snowman touched.
    //--------------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnowmanKnockBack")
        {
            // Setting the emit parameters for the particle system.
            ParticleSystem.EmitParams myParams = new ParticleSystem.EmitParams();
            myParams.startLifetime = 1.0f;

            Knockback();

            // Play the particle effect.
            m_psSnowflake.Play();
            // Play the particle effect.
            m_psCloud.Play();

        }
    }

    //--------------------------------------------------------------------------------------
    // Knockback pushes players away when they are caught in the area of effect.
    // If the jumping snowman collides with the ground at this point his radius of effect is
    // taken into account and checks to see if there are any player nearby, then if there 
    // are any it uses bitshift to check the affect players and then uses the rigidBody 
    // function the 'AddExplosionForce' to push them away in all directions.
    //--------------------------------------------------------------------------------------
    void Knockback()
    {
        Vector3 hit = transform.position; // Ignore these numbers, get position from collision impact,
        // Bitshifting with the or operator the 2 layers,
        int playerLayer = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("PlayerDash");
        // Checking if the players are affected.
        Collider[] players = Physics.OverlapSphere(hit, m_fAreaOfEffect, playerLayer);

        for (int i = 0; i < players.Length; ++i)
        {
            Rigidbody rb = players[i].gameObject.GetComponent<Rigidbody>();
            // Stopping the movement on the rigidbody.
            rb.velocity = Vector3.zero;
            // Pushing players back with explosive force.
            rb.AddExplosionForce(m_fKnockbackForce, hit, m_fAreaOfEffect, 1.0f, ForceMode.Impulse);
            // Changing some bools in the player to restrict movement, dashing and throwing.
            Player scpPlayer = players[i].GetComponent<Player>();
            scpPlayer.m_bHitByGiantSnowBall = true;
            scpPlayer.GetComponent<Dash>().m_bDashing = false;
            scpPlayer.GetComponent<Dash>().m_bCoolDown = false;
            // If the player was dashing cut that short.
            scpPlayer.GetComponent<Dash>().m_fDashTimer = scpPlayer.GetComponent<Dash>().m_fDashDuration;
            scpPlayer.m_goPlayerModel.GetComponent<Animator>().SetBool("dashing", false);
        }
    }
}
