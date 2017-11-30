//-------------------------------------------------------------------------------
// Filename:        JumpingSnowman.cs
//
// Description:     The jumping snowman is a cool feature of the game, he spawns
//                  in a couple minutes after the game has started, and with him 
//                  the death snowballs spawn, this is the deathmatch section.
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Mitchell Cattini-Schultz
//-------------------------------------------------------------------------------

using UnityEngine;

public class JumpingSnowman : MonoBehaviour
{
    //------------------
    // Initial Spawn Time
    //------------------
    [LabelOverride("Initial Spawn Time")]
    [Tooltip("How long until the snowman spawns for the first time.")]
    public float m_fInitialSpawnTime = 5.0f;

    //------------------
    // Death spawn time
    //------------------
    [LabelOverride("Death Spawn Time")]
    [Tooltip("Minimum wait time between next spawn.")]
    public float m_fDeathTime = 0.0f;

    //----------------------
    // Time between jumps
    //----------------------
    [LabelOverride("Time Between Jumps")]
    [Tooltip("(Seconds) Time between the snowman jumps.")]
    public float m_fTimeBetweenJumps = 2.0f;

    private float m_fSpawnTime = 0.0f;
    private float m_fSpawnCount = 0.0f;

    private Collider m_Collider;
    private MeshRenderer m_MeshRenderer;

    private GameObject m_goTL;
    private GameObject m_goTR;
    private GameObject m_goBL;
    //private GameObject m_goBR;

    private Vector3 m_v3CurveMiddle;

    //private Vector3 m_v3Pos;
    //private float m_xLoc = 0.0f;
    //private float m_zLoc = 0.0f;

    private float m_fJumpCounter = 0.0f;
    //----------------------
    // Time Until Next Jump
    //----------------------
    [LabelOverride("Time Until Next Jump")]
    [Tooltip("The time until next jump")]
    public float m_fTimeUntilNextJump = 2.0f;

    //private bool m_bCanJump = false;
    private bool m_bBetweenJumps = false;
    private bool m_bJumping = false;
    private bool m_bBoingSound = true;
    private float m_fJumpingTimer = 0.0f;

    //----------------------
    // Jump Time
    //----------------------
    [LabelOverride("Jump Time")]
    [Tooltip("Time the Snowman spends jumping from position to destination.")]
    public float m_fJumpingTime = 1.0f;
    private Vector3 m_v3StartPos;
    private Vector3 m_v3NextPos;
    private float m_fStartY;

    //----------------------
    // Height of Jump
    //----------------------
    [LabelOverride("Height of Jump")]
    [Tooltip("The height at which the Snowman reaches at the peak of it's jump.")]
    public float m_fHeightOfJump = 5.0f;

    private int m_nHealthPoints = 2;
    private bool m_bDead = true;

    //----------------------
    // Spawn Particle System
    //----------------------
    [LabelOverride("Spawn Particle System")]
    [Tooltip("Drag and drop the respective particle system onto here.")]
    public GameObject m_goSpawnParticles;

    private ParticleSystem psSpawn;

    private bool bSpawnParticleEffectPlayed = false;
    private bool bChooseNewSpawn = true;
    private bool m_bOnce = true;
    private bool m_bStart = false;


    //--------------------------------------------------------------------------------------
    // Use this for initialization, called even if the script is disabled.
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        //if (SceneManager.GetActiveScene().name == "Main_Default" || SceneManager.GetActiveScene().buildIndex == 11)
        {
            //rb = GetComponent<Rigidbody>();
            m_goTR = GameObject.Find("Reticle Bounds Top Right");
            m_goTL = GameObject.Find("Reticle Bounds Top Left");
            //m_goBR = GameObject.Find("Reticle Bounds Bottom Right");
            m_goBL = GameObject.Find("Reticle Bounds Bottom Left");
        }

        m_fStartY = transform.position.y;
    }

    //--------------------------------------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------------------------------------
    void Start()
    {
        // getting and assigning the particle system
        psSpawn = m_goSpawnParticles.GetComponent<ParticleSystem>();

        // getting and assigning the collider and mesh renderer
        m_Collider = gameObject.GetComponent<Collider>();
        m_MeshRenderer = gameObject.GetComponent<MeshRenderer>();

        // setting the collider and mesh renderer to false, to make the jumping snowman appear dead
        m_Collider.enabled = false;
        m_MeshRenderer.enabled = false;

        // assigning the initial spawn time
        m_fSpawnTime = m_fInitialSpawnTime;
    }

    //--------------------------------------------------------------------------------------
    // Update is called once per frame
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // resets the script without having to destroy and instatiate a new Jumping Snowman each time
        if (m_bStart)
        {
            m_fSpawnCount = 0.0f;
            m_fJumpCounter = 0.0f;
            m_bBetweenJumps = false;
            m_bJumping = false;
            m_bBoingSound = true;
            m_fJumpingTimer = 0.0f;
            m_nHealthPoints = 2;
            bSpawnParticleEffectPlayed = false;
            bChooseNewSpawn = true;
            m_bOnce = true;

            m_bStart = false;
        }

        if (m_bDead)
        {
            //Before spawn, happens once
            if (m_fSpawnCount <= m_fSpawnTime)
            {
                m_fSpawnCount += Time.deltaTime;

                if (bChooseNewSpawn)
                {
                    // move the snowman to the next location
                    transform.position = FindValidSpawnPos();
                    bChooseNewSpawn = false;
                }
            }

            if (!bSpawnParticleEffectPlayed && m_fSpawnCount > (m_fSpawnTime - 1.0f))
            {
                // particle system play
                psSpawn.Play();

                bSpawnParticleEffectPlayed = true;
            }
            else if (m_fSpawnCount > m_fSpawnTime)
            {
                // Snowman spawns
                m_Collider.enabled = true;
                m_MeshRenderer.enabled = true;
                m_bDead = false;
                bChooseNewSpawn = true;
                m_fSpawnTime = m_fDeathTime;
            }

            if (m_bOnce && m_fSpawnCount > (m_fSpawnTime - 1.5f))
            {
                // summon sound
                AudioManager.m_SharedInstance.PlaySnowmanSummon();
                m_bOnce = false;
            }
        }

        // Between Jumps
        if (!m_bDead && m_fJumpCounter <= m_fTimeUntilNextJump && !m_bJumping)
        {
            m_fJumpCounter += Time.deltaTime;
            m_bBetweenJumps = true;
        }
        else if (!m_bDead && m_fJumpCounter > m_fTimeUntilNextJump)
        {
            // sets the start pos so the curve can be created
            m_v3StartPos = transform.position;

            m_bBetweenJumps = false;
            m_fJumpCounter = 0.0f;

            // finds a valid spawn position
            m_v3NextPos = FindValidSpawnPos();

            // creates a curve based on the Jumping Snowmans pos and destination pos
            CreateCurve();
        }

        // Jumping
        if (!m_bDead && m_fJumpingTimer <= m_fJumpingTime && !m_bBetweenJumps)
        {
            m_fJumpingTimer += Time.deltaTime;

            if (m_bBoingSound)
            {
                // boing sound
                AudioManager.m_SharedInstance.PlaySnowmanBoingAudio();
                m_bBoingSound = false;
            }

            // move the snowman to the next location smoothly over a curve (lerp)
            Vector3 v3P1 = Vector3.Lerp(m_v3StartPos, m_v3CurveMiddle, m_fJumpingTimer);
            Vector3 v3P2 = Vector3.Lerp(m_v3CurveMiddle, m_v3NextPos, m_fJumpingTimer);
            transform.position = Vector3.Lerp(v3P1, v3P2, m_fJumpingTimer);

            m_bJumping = true;
        }
        else if (!m_bDead && m_fJumpingTimer > m_fJumpingTime)
        {
            m_bJumping = false;
            m_fJumpingTimer = 0.0f;
            m_bBoingSound = true;
        }

        if (m_nHealthPoints <= 0)
        {
            // snowman is dead
            m_Collider.enabled = false;
            m_MeshRenderer.enabled = false;
            m_bDead = true;
            m_bStart = true;
        }
    }

    //--------------------------------------------------------------------------------------
    // Checks if the Jumping Snowman was hit by a Snowball and deducts 1 health point.
    //
    // Param:
    //      col: The object that the Jumping Snowman collided with.
    //--------------------------------------------------------------------------------------
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Snowball" && col.gameObject.GetComponent<Snowball>().m_bTooFast)
        {
            m_nHealthPoints = m_nHealthPoints - 1;
        }
    }

    //--------------------------------------------------------------------------------------
    // Create the 3 points of the curve that the lerps need to create a beizer for the jump.
    //--------------------------------------------------------------------------------------
    private void CreateCurve()
    {
        // creating the centre of the curve
        m_v3CurveMiddle = m_v3StartPos + m_v3NextPos / 2;
        m_v3CurveMiddle += transform.up * m_fHeightOfJump;
    }

    //--------------------------------------------------------------------------------------
    // Checks if the Jumping Snowman plotted next destination would be inside an obstacle
    // and generates a new location, preventing weird jumps.
    //
    // Return:
    //      Returns a Vector 3 which is the next destination he will jump to.
    //--------------------------------------------------------------------------------------
    private Vector3 FindValidSpawnPos()
    {
        Vector3 v3Result = Vector3.zero;
        int layer = 1 << LayerMask.NameToLayer("Obstacle");

        // checks if the destination location is valid and if not finds a new one, rinse repeat
        do
        {
            // finds a random float value based on the range of the x values that belong to the 
            // top left, top right, bottom left and top left reticle bounds, then uses those values 
            // to create a new vector 3 location for the jumping snowman to jump to.
            v3Result.x = Random.Range(m_goTL.transform.position.x, m_goTR.transform.position.x);
            v3Result.y = m_fStartY;
            v3Result.z = Random.Range(m_goBL.transform.position.z, m_goTL.transform.position.z);
        }
        while (Physics.CheckSphere(v3Result, 0.5682563f, layer));

        return v3Result;
    }
}

/*
 * How to Lerp!
 *
 * Start (snowmans pos)
 * Randomly calculate an end
 * Height
 * 
 * MidPoint = (Start + end) / 2
 * Midpoint.y = height
 * 
 * Vector3 a = lerp (start, midpoint, t)
 * Vector3 b = lerp(midpoint, end, t)
 * Vector3 result = lerp (a, b, t)
*/
