using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    private Rigidbody m_rb;
    //-------------
    // Damage Speed
    //-------------
    [LabelOverride("Damage Speed")]
    [Tooltip("This is the speed that will damage a player when they are hit by it.")]
    public float m_fDamageSpeed = 3.0f;
    //---------
    // Too Fast
    //---------
    [LabelOverride("Too Fast")]
    [Tooltip("A bool that checks whether the snowball is moving too fast or not.")]
    public bool m_bTooFast = false;
    //----------------
    // Materials Array
    //----------------
    [LabelOverride("Material Array")]
    [Tooltip("An array to store materials.")]
    public Material[] m_materials;
    private Renderer m_rend;

    //-------
    // Score
    //-------
    //------------
    // Score Value
    //------------
    [LabelOverride("Score Value")]
    [Tooltip("A static int that represents how much health a player loses when they are hit by the snowball.")]
    public static int m_nScoreValue = 1;

    public ParticleSystem m_ParticleSparks;

    private Color mainColor = Color.white;
    private MeshRenderer mr = null;
    
    public Gradient mstet = null;

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        mr = GetComponent<MeshRenderer>();
        mainColor = mr.material.color;
        m_ParticleSparks = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        //var main = m_ParticleSparks.main;
        //main.startColor = mstet;
        
        if (m_ParticleSparks == null)
        {
            Debug.Log("NO SPARKS");
        }
        //if the ball is moving at a dangerous speed, let the player know!
        if (m_bTooFast)
        {
            mr.material = m_materials[1];
            m_ParticleSparks.Play();
        }
        else
        {
            mr.material = m_materials[0];
            m_ParticleSparks.Stop();
        }

        if (m_rb.velocity.y > 5.0f)
        {
            Vector3 v3 = m_rb.velocity;
            v3.y = 5.0f;
            m_rb.velocity = v3;
        }

        if (m_rb.velocity.magnitude >= m_fDamageSpeed)
        {
            m_nScoreValue = 1;
            m_bTooFast = true;
        }

        else if (m_rb.velocity.magnitude < m_fDamageSpeed)
        {
            m_nScoreValue = 0;
            m_bTooFast = false;
        }


    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Ground" && col.gameObject.tag != "Character")
        {
            if (m_rb)
                m_rb.velocity = m_rb.velocity * 1f;
        }

        if (col.gameObject.tag != "Ground")
        {
            AudioManager.m_SharedInstance.PlaySnowBallHit();
        }
    }
}
