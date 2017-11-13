using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpingSnowman : MonoBehaviour
{
    //----------------------
    // Min spawn time
    //----------------------
    [LabelOverride("Min Spawn Time")]
    [Tooltip("Minimum wait time between next spawn")]
    public float m_fMin = 20.0f;

    //----------------------
    // Max spawn time
    //----------------------
    [LabelOverride("Max Spawn Time")]
    [Tooltip("Maximum wait time between next spawn")]
    public float m_fMax = 30.0f;

    //----------------------
    // Time between jumps
    //----------------------
    [LabelOverride("Time Between Jumps")]
    [Tooltip("(Seconds) The time between the snowman's next jump.")]
    public float m_fTimeBetweenJumps = 2.0f;

    private float m_fSpawnTime = 0.0f;
    private float m_fSpawnCount = 0.0f;

    private Collider c;
    private MeshRenderer mr;

    private GameObject m_goTL;
    private GameObject m_goTR;
    private GameObject m_goBL;
    private GameObject m_goBR;

    private Vector3 m_v3Cm;

    private Vector3 m_v3Pos;
    private float m_xLoc = 0.0f;
    private float m_zLoc = 0.0f;

    private float m_fJumpCounter = 0.0f;
    //----------------------
    // 
    //----------------------
    [LabelOverride("Time Until Next Jump")]
    [Tooltip("The time until next jump")]
    public float m_fTimeUntilNextJump = 2.0f;

    private bool m_bCanJump = false;
    private bool m_bBetweenJumps = false;
    private bool m_bJumping = false;
    private float m_fJumpingTimer = 0.0f;
    //----------------------
    // 
    //----------------------
    [LabelOverride("Jump Time")]
    [Tooltip("Time the Snowman spends jumping from position to destination.")]
    public float m_fJumpingTime = 1.0f;
    private Vector3 m_v3StartPos;
    private Vector3 m_v3NextPos;

    private int m_nCurve = 1;

    //----------------------
    // 
    //----------------------
    [LabelOverride("Height of Jump")]
    [Tooltip("The height at which the Snowman reaches at the peak of it's jump.")]
    public float m_fHeightOfJump = 2.0f;


    // Use this for initialization
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            //rb = GetComponent<Rigidbody>();
            m_goTR = GameObject.Find("Reticle Bounds Top Right");
            m_goTL = GameObject.Find("Reticle Bounds Top Left");
            m_goBR = GameObject.Find("Reticle Bounds Bottom Right");
            m_goBL = GameObject.Find("Reticle Bounds Bottom Left");
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

    }

    // Update is called once per frame
    void Update()
    {
        //Before spawn, happens once
        if (m_fSpawnCount <= m_fSpawnTime && !m_bCanJump)
        {
            m_fSpawnCount += Time.deltaTime;
        }
        else if (m_fSpawnCount > m_fSpawnTime && !m_bCanJump)
        {
            // Snowman spawns
            m_xLoc = Random.Range(m_goTL.transform.position.x, m_goTR.transform.position.x) - transform.position.x;
            // we minus the current location to keep the snowman in the map
            m_zLoc = Random.Range(m_goBL.transform.position.z, m_goTL.transform.position.z) - transform.position.z;
            // set the snowmans next location
            m_v3Pos.Set(m_xLoc, 1.0f, m_zLoc);
            // move the snowman to the next location
            transform.position = m_v3Pos;

            c.enabled = true;
            mr.enabled = true;
            m_bCanJump = true;
        }

        // Between Jumps
        if (m_fJumpCounter <= m_fTimeUntilNextJump && m_bCanJump && !m_bJumping)
        {
            //Debug.Log("between");
            m_fJumpCounter += Time.deltaTime;
            m_bBetweenJumps = true;
        }
        else if (m_fJumpCounter > m_fTimeUntilNextJump)
        {
            m_bBetweenJumps = false;
            m_fJumpCounter = 0.0f;

            // Setting the next pos
            m_xLoc = Random.Range(m_goTL.transform.position.x, m_goTR.transform.position.x) - transform.position.x;
            // we minus the current location to keep the snowman in the map
            m_zLoc = Random.Range(m_goBL.transform.position.z, m_goTL.transform.position.z) - transform.position.z;
            // set the snowmans next location
            m_v3NextPos.Set(m_xLoc, 0.0f, m_zLoc);
        }

        // Jumping
        if (m_fJumpingTimer <= m_fJumpingTime && !m_bBetweenJumps)
        {
            //Debug.Log("jumping");
            m_fJumpingTimer += Time.deltaTime;

            CreateCurve();

            // move the snowman to the next location smoothly
            switch (m_nCurve)
            {
                case 1:
                    transform.position += m_v3Cm * Time.deltaTime;
                    if (m_fJumpingTimer > m_fJumpingTime / 2)
                        m_nCurve = 2;
                    break;

                case 2:
                    // good job hamo nad mitcho
                    transform.position += (m_v3NextPos * Time.deltaTime) + (-transform.up * m_fHeightOfJump * Time.deltaTime);
                    if (m_fJumpingTimer >= m_fJumpingTime)
                        m_nCurve = 1;
                    break;

                default:
                    break;
            }

            m_bJumping = true;
        }
        else if (m_fJumpingTimer > m_fJumpingTime)
        {
            m_bJumping = false;
            m_fJumpingTimer = 0.0f;
        }
    }

    private void CreateCurve()
    {
        //creating the centre of the curve
        m_v3Cm = m_v3StartPos + m_v3NextPos / 2;
        m_v3Cm += transform.up * m_fHeightOfJump;
    }
}
