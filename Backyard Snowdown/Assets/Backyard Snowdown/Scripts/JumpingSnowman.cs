// Franzi
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
    [Tooltip("(Seconds) Time between the snowman jumps.")]
    public float m_fTimeBetweenJumps = 2.0f;

    private float m_fSpawnTime = 0.0f;
    private float m_fSpawnCount = 0.0f;

    private Collider c;
    private MeshRenderer mr;

    private GameObject m_goTL;
    private GameObject m_goTR;
    private GameObject m_goBL;
    private GameObject m_goBR;

    private Vector3 m_v3CurveMiddle;

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
    private bool m_bBoingSound = true;
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
    public float m_fHeightOfJump = 5.0f;

    private bool m_bSpawned = false;
    private int m_nHealthPoints = 2;
    private bool m_bDead = false;

    private float m_fDeathTimer = 0.0f;
    private float m_fDeathTime = 3.0f;

    // Use this for initialization
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 6)
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
        AudioManager.m_SharedInstance.PlaySnowmanSummon();
    }

    private bool spawned = false;

    // Update is called once per frame
    void Update()
    {
        if (!m_bSpawned)
        {
            //Before spawn, happens once
            if (m_fSpawnCount <= m_fSpawnTime && !m_bCanJump)
            {
                m_fSpawnCount += Time.deltaTime;
            }
            else if (m_fSpawnCount > m_fSpawnTime && !m_bCanJump)
            {
                m_xLoc = Random.Range(m_goTL.transform.position.x, m_goTR.transform.position.x) - transform.position.x;
                // we minus the current location to keep the snowman in the map
                m_zLoc = Random.Range(m_goBL.transform.position.z, m_goTL.transform.position.z) - transform.position.z;
                // set the snowmans next location
                m_v3Pos.Set(m_xLoc, 0.9f, m_zLoc);
                // move the snowman to the next location
                transform.position = m_v3Pos;

                // Snowman spawns
                c.enabled = true;
                mr.enabled = true;
                m_bSpawned = true;
            }
        }

        // Between Jumps
        if (!m_bDead && m_bSpawned && m_fJumpCounter <= m_fTimeUntilNextJump && !m_bJumping)
        {
            //Debug.Log("between");
            m_fJumpCounter += Time.deltaTime;
            m_bBetweenJumps = true;
        }
        else if (!m_bDead && m_bSpawned && m_fJumpCounter > m_fTimeUntilNextJump)
        {
            m_bBetweenJumps = false;
            m_fJumpCounter = 0.0f;
            // Setting the next pos
            m_xLoc = Random.Range(m_goTL.transform.position.x, m_goTR.transform.position.x) - transform.position.x;
            // we minus the current location to keep the snowman in the map
            m_zLoc = Random.Range(m_goBL.transform.position.z, m_goTL.transform.position.z) - transform.position.z;
            // set the snowmans next location
            m_v3NextPos.Set(m_xLoc, 0.0f, m_zLoc);

            int layer = 1 << LayerMask.NameToLayer("Obstacle");

            while (Physics.CheckSphere(m_v3NextPos, 0.5682563f, layer))
            {
                // Setting the next pos
                m_xLoc = Random.Range(m_goTL.transform.position.x, m_goTR.transform.position.x) - transform.position.x;
                // we minus the current location to keep the snowman in the map
                m_zLoc = Random.Range(m_goBL.transform.position.z, m_goTL.transform.position.z) - transform.position.z;
                // set the snowmans next location
                m_v3NextPos.Set(m_xLoc, 0.9f, m_zLoc);
            }

        }

        // Jumping
        if (!m_bDead && m_bSpawned && m_fJumpingTimer <= m_fJumpingTime && !m_bBetweenJumps)
        {
            //Debug.Log("jumping");
            m_fJumpingTimer += Time.deltaTime;

            // boing sound
            if (m_bBoingSound)
            {
                AudioManager.m_SharedInstance.PlaySnowmanBoingAudio();
                m_bBoingSound = false;
            }

            CreateCurve();

            // move the snowman to the next location smoothly
            switch (m_nCurve)
            {
                // snowman moves to the peak of its jump
                case 1:
                    transform.position += m_v3CurveMiddle * Time.deltaTime;
                    if (m_fJumpingTimer > m_fJumpingTime / 2)
                        m_nCurve = 2;
                    break;

                case 2:
                    // good job hamo nad mitcho
                    // snowman moves from the peak of its jump to the landing point
                    transform.position += (m_v3NextPos * Time.deltaTime) + m_fHeightOfJump * (-transform.up * Time.deltaTime);
                    if (m_fJumpingTimer >= m_fJumpingTime)
                    {
                        m_nCurve = 1;
                        transform.position = new Vector3(transform.position.x, 0.9f, transform.position.z);
                    }
                    break;

                default:
                    break;
            }

            m_bJumping = true;
        }
        else if (!m_bDead && m_bSpawned && m_fJumpingTimer > m_fJumpingTime)
        {
            m_bJumping = false;
            m_fJumpingTimer = 0.0f;
            m_bBoingSound = true;
        }

        if (m_nHealthPoints <= 0)
        {
            // snowman is dead
            c.enabled = false;
            mr.enabled = false;
            m_bSpawned = false;
            m_bDead = true;
        }

        if (m_bDead && m_fDeathTimer <= m_fDeathTime)
        {
            m_fDeathTimer += Time.deltaTime;
        }
        else if (m_bDead && m_fDeathTimer > m_fDeathTime)
        {
            c.enabled = true;
            mr.enabled = true;
            m_bSpawned = true;
            m_bDead = false;
            m_fDeathTimer = 0.0f;

            // restore life to franzi
            m_nHealthPoints = 2;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Snowball")
        {
            m_nHealthPoints = m_nHealthPoints - 1;
        }
    }

    private void CreateCurve()
    {
        // creating the centre of the curve
        m_v3CurveMiddle = m_v3StartPos + m_v3NextPos / 2;
        m_v3CurveMiddle += transform.up * m_fHeightOfJump;
    }
}
