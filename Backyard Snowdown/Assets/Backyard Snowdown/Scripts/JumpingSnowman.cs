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

    private Vector3 m_v3SpawnPos;
    private float m_xLoc = 0.0f;
    private float m_yLoc = 0.0f;
    
    
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
        m_yLoc = Random.Range(m_TL.transform.position.y, m_BL.transform.position.y);

        m_v3SpawnPos.Set(m_xLoc, 1.0f, m_yLoc);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_fSpawnTime);
        if (m_fSpawnCount <= m_fSpawnTime)
        {
            m_fSpawnCount += Time.deltaTime;
        }
        else if (m_fSpawnCount > m_fSpawnTime)
        {
            gameObject.transform.position = m_v3SpawnPos;
            c.enabled = true;
            mr.enabled = true;
        }
    }
}
