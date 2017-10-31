using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpingSnowman : MonoBehaviour
{
    //----------------------
    // 
    //----------------------
    [LabelOverride("tet")]
    [Tooltip("tet")]
    public float m_fMin = 20.0f;

    //----------------------
    // 
    //----------------------
    [LabelOverride("tet")]
    [Tooltip("tet")]
    public float m_fMax = 30.0f;

    private float m_fSpawnTime = 0.0f;
    private float m_fSpawnCount = 0.0f;

    private Collider c;
    private MeshRenderer mr;

    private GameObject m_TL;
    private GameObject m_TR;
    private GameObject m_BL;
    private GameObject m_BR;

    private Vector3 m_v3Pos;
    private float m_xLoc = 0.0f;
    private float m_zLoc = 0.0f;

    private float m_fJumpCounter = 0.0f;
    public float m_fTimeBetweenJumps = 2.0f;
    private bool m_bCanJump = false;


    // Use this for initialization
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            //rb = GetComponent<Rigidbody>();
            m_TR = GameObject.Find("Reticle Bounds Top Right");
            m_TL = GameObject.Find("Reticle Bounds Top Left");
            m_BR = GameObject.Find("Reticle Bounds Bottom Right");
            m_BL = GameObject.Find("Reticle Bounds Bottom Left");
        }
    }

    // Use this for initialization
    void Start()
    {
        c = gameObject.GetComponent<Collider>();
        mr = gameObject.GetComponent<MeshRenderer>();

        c.enabled = false;
        mr.enabled = false;

        m_fSpawnTime = Random.Range(m_fMin, m_fMax);

        m_xLoc = Random.Range(m_TL.transform.position.x, m_TR.transform.position.x);
        m_zLoc = Random.Range(m_TL.transform.position.z, m_BL.transform.position.z);

        m_v3Pos.Set(m_xLoc, 1.1f, m_zLoc);
        Debug.Log(m_fSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {

        if (m_fSpawnCount <= m_fSpawnTime)
        {
            m_fSpawnCount += Time.deltaTime;
        }
        else if (m_fSpawnCount > m_fSpawnTime)
        {
            transform.position = m_v3Pos;
            c.enabled = true;
            mr.enabled = true;
            m_bCanJump = true;

            m_xLoc = Random.Range(m_TL.transform.position.x, m_TR.transform.position.x);
            m_zLoc = Random.Range(m_TL.transform.position.z, m_BL.transform.position.z);
        }

        if (m_fJumpCounter <= m_fTimeBetweenJumps && m_bCanJump)
        {
            m_fJumpCounter += Time.deltaTime;
        }
        else if (m_fJumpCounter > m_fTimeBetweenJumps)
        {
            transform.position = m_v3Pos;

            m_xLoc = Random.Range(m_TL.transform.position.x, m_TR.transform.position.x);
            m_zLoc = Random.Range(m_TL.transform.position.z, m_BL.transform.position.z);
            m_v3Pos.Set(m_xLoc, 1.1f, m_zLoc);

            m_fJumpCounter = 0.0f;
        }

    }
}
