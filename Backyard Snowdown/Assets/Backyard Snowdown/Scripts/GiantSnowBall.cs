//-------------------------------------------------------------------------------
// Filename:        GiantSnowBall.cs
//
// Description:     GiantSnowBall is used to apply knockback to players.
//                  When the jumping snowman lands this script is utilized.
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Mitchell Cattini-Schultz
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSnowBall : MonoBehaviour
{
    //----------------
    // Knockback Force
    //----------------
    [LabelOverride("Knockback Force")]
    [Tooltip("The force that is applied to knockback when hit by giant snowball.")]
    public float m_fKnockbackForce = 0.0f;
    //---------------
    // Area Of Effect
    //---------------
    [LabelOverride("Area of Effect")]
    [Tooltip("A float to change the area effect of the giant snowball.")]
    public float m_fAreaOfEffect = 3.0f;

    public GameObject m_goImpactParticles;
    public GameObject m_goImpactParticlesCloud;

    private ParticleSystem psSnowflake;
    private ParticleSystem psCloud;


    //--------------------------------------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------------------------------------
    void Start()
    {
        psSnowflake = m_goImpactParticles.GetComponent<ParticleSystem>();
        psCloud = m_goImpactParticlesCloud.GetComponent<ParticleSystem>();
    }

    //--------------------------------------------------------------------------------------
    // Update is called once per frame
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
            ParticleSystem.EmitParams myParams = new ParticleSystem.EmitParams();
            myParams.startLifetime = 1.0f;
            Knockback();

            psSnowflake.Play();
            psCloud.Play();

        }
    }

    //--------------------------------------------------------------------------------------
    // Knockback pushes players away when they are caught in the area of effect.
    // If the jumping snowman collides with the ground at this point his radius of effect is
    // taken into account and checks to see if there are any player nearby, then if there 
    // are any it adds them to a player layer via bitshift and uses the rigidBody function
    // 'AddExplosionForce' to push them away in all directions.
    //--------------------------------------------------------------------------------------
    void Knockback()
    {
        Vector3 hit = transform.position; //ignore these numbers, get position from collision impact
        int playerLayer = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("PlayerDash");
        Collider[] players = Physics.OverlapSphere(hit, m_fAreaOfEffect, playerLayer);

        for (int i = 0; i < players.Length; ++i)
        {
            Rigidbody rb = players[i].gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddExplosionForce(m_fKnockbackForce, hit, m_fAreaOfEffect, 1.0f, ForceMode.Impulse);
            Player scpPlayer = players[i].GetComponent<Player>();
            scpPlayer.m_bHitByGiantSnowBall = true;
            scpPlayer.GetComponent<Dash>().m_bDashing = false;
            scpPlayer.GetComponent<Dash>().m_bCoolDown = false;
            scpPlayer.GetComponent<Dash>().m_fDashTimer = scpPlayer.GetComponent<Dash>().m_fDashDuration;
            scpPlayer.m_goPlayerModel.GetComponent<Animator>().SetBool("dashing", false);
        }
    }
}
