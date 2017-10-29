using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    Rigidbody m_rb;
    public float m_fDamageSpeed = 3.0f;
    public bool m_bTooFast = false;

    public Material[] m_materials;
    private Renderer m_rend;

    private bool m_bOnce = true;

    //-------
    // Score
    //-------
    public static int m_nScoreValue = 2;


    private Color mainColor = Color.white;
    private MeshRenderer mr = null;


    // Use this for initialization
    void Start()
    {
        //gameObject.SetActive(true);
    }

    // Use this for initialization each time the object is enabled
    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();

        mr = GetComponent<MeshRenderer>();
        mainColor = mr.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //if the ball is moving at a dangerous speed, let the player know!
        if (m_bTooFast)
            mr.material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        else
            mr.material.color = mainColor;

        //Debug.Log(rb.velocity);

        //if (fVelCount > 1.0f || Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    fVelCount += Time.deltaTime;
        //    Debug.Log("ACTIVE");
        //}
        //else
        //{
        //    if (rb.velocity.x <= 0.5f && fVelCount != 0)
        //    {
        //        rb.velocity = Vector3.zero;
        //        fVelCount = 0.0f;
        //    }

        //    if (rb.velocity.y <= 0.5f && fVelCount != 0)
        //    {
        //        rb.velocity = Vector3.zero;
        //        fVelCount = 0.0f;
        //    }
        //}

        if (m_rb.velocity.magnitude >= m_fDamageSpeed)
        {
            m_nScoreValue = 2;
            m_bTooFast = true;
        }

        else if (m_rb.velocity.magnitude < m_fDamageSpeed)
        {
            m_nScoreValue = 0;
            m_bTooFast = false;
        }

        //if (rb.velocity.magnitude > fDamageSpeed)
        //{
        //    nScoreValue = 2;
        //    bTooFast = true;
        //}

        //else if (rb.velocity.magnitude < fDamageSpeed)
        //{
        //    nScoreValue = 0;
        //    bTooFast = false;
        //}

        // now i need to do the same for the negitive axis but then conflicts happen.

        //if (rb.velocity.x + rb.velocity.z < -fDamageSpeed)
        //{
        //    nScoreValue = 2;
        //    bTooFast = true;
        //}

        //if (rb.velocity.x + rb.velocity.z < -fDamageSpeed)
        //{
        //    nScoreValue = 2;
        //    bTooFast = true;
        //}

        //Debug.Log(nScoreValue);

        //if (rb.velocity.x < 2.0f && rb.velocity.x > 0.0f || rb.velocity.x > -2.0f && rb.velocity.x < 0.0f || rb.velocity.z < 2.0f && rb.velocity.z > 0.0f || rb.velocity.z > -2.0f && rb.velocity.z < 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //if (rb.velocity.x < 2.0f && rb.velocity.x > 0.0f && rb.velocity.z == 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //if (rb.velocity.z < 2.0f && rb.velocity.z > 0.0f && rb.velocity.x == 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //if (rb.velocity.x > -2.0f && rb.velocity.x < 0.0f && rb.velocity.z == 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //if (rb.velocity.z > -2.0f && rb.velocity.z < 0.0f && rb.velocity.x == 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //Debug.Log(nScoreValue);

        //stop sliding
        //rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Ground" && col.gameObject.tag != "Character")
        {
            if (m_rb)
                m_rb.velocity = m_rb.velocity * 1f;
        }

        else
        {

        }

        //Player scpPlayer = gameObject.GetComponent<Player>();

        //float fBallMagnitude = rb.velocity.magnitude;


        //fBallMagnitude = (rb.velocity.magnitude - 2.0f);

        //rb.velocity.magnitude = fBallMagnitude;



        //if (col.gameObject.tag == "Snowball")
        //{
        //    scpAbilitySnowMan.bASnowManExists = false;
        //    Destroy(gameObject);
        //}
    }
}
