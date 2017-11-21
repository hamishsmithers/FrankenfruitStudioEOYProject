﻿using System.Collections;
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


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position -= Vector3.up * 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("MARC");
        if (other.gameObject.tag == "SnowmanKnockBack")
        {
            Knockback();
            ParticleSystem psSnowflake = m_goImpactParticles.GetComponent<ParticleSystem>();
            ParticleSystem psCloud = m_goImpactParticles.transform.GetChild(0).GetComponent<ParticleSystem>();

            //ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
            //emitOverride.startLifetime = 1f;
            psSnowflake.Emit(1);
            psCloud.Emit(1);
        }
    }

    void Knockback()
    {
        Vector3 hit = transform.position; //ignore these numbers, get position from collision impact
        int playerLayer = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("PlayerDash");
        Collider[] players = Physics.OverlapSphere(hit, m_fAreaOfEffect, playerLayer);

        for (int i = 0; i < players.Length; ++i)
        {
            Rigidbody rb = players[i].gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(m_fKnockbackForce, hit, m_fAreaOfEffect, 1.0f, ForceMode.Impulse);
            Player scpPlayer = players[i].GetComponent<Player>();
            scpPlayer.m_bHitByGiantSnowBall = true;
        }
    }
}
