using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Player : MonoBehaviour
{
    public XboxController controller;

    //-------------------------
    // Player's Movement Speed
    //-------------------------
    [LabelOverride("Speed")]
    [Tooltip("Player's Movement Speed.")]
    public float m_fSpeed = 5.0f;
    //-----------------------------
    // Player's Max Movement Speed
    //-----------------------------
    [LabelOverride("Max Speed")]
    [Tooltip("Player's maximum movement speed.")]
    public float m_fMaxSpeed = 5.0f;
    //------------
    // Dash Speed
    //------------
    [LabelOverride("Dash Speed")]
    [Tooltip("The speed that the player moves while dashing.")]
    public float m_fDashSpeed = 10.0f;
    //---------------
    // Dash Duration
    //---------------
    [LabelOverride("Dash Duration")]
    [Tooltip("The duration of the dash.")]
    public float m_fDashDuration = 0.5f;
    //--------------
    // Player Model
    //--------------
    [LabelOverride("Player Model")]
    [Tooltip("Stores the player model")]
    public GameObject m_goPlayerModel = null;

    //-----------------------------------------------------------
    // A float to store the current movement speed of the player
    //-----------------------------------------------------------
    [HideInInspector]
    public float m_fCurrentSpeed = 5.0f;
    //--------------------------------------
    // Stops the players being able to move
    //--------------------------------------
    [HideInInspector]
    public bool m_bMovementLock = false;
    //---------------------------------------------------------
    // Bool to check whether the left trigger has been pressed
    //---------------------------------------------------------
    [HideInInspector]
    public bool m_bLeftTriggerPressed = false;
    //----------------------------------------------------------------
    // A Vector3 to store the current movement position of the player
    //----------------------------------------------------------------
    [HideInInspector]
    public Vector3 m_v3MovePos;
    //-----------------------------------------
    // Vector3 to hold the Xbox dash direction
    //-----------------------------------------
    [HideInInspector]
    public Vector3 m_v3XboxDashDir;
    //-----------------------------
    // A float to store the axis X
    //-----------------------------
    private float m_axisX;
    //-----------------------------
    // A float to store the axis Y
    //-----------------------------
    private float m_axisY;
    //-----------
    // RIGIDBODY
    //-----------
    [HideInInspector]
    public Rigidbody m_rb;
    //------------------------------------------------------
    // Vector3 to store the direction of the player to dash
    //------------------------------------------------------
    private Vector3 m_v3DashDir;

    //----------
    // Snowball
    //----------
    [LabelOverride("Snowball")]
    [Tooltip("Stores the Snowball GameObject.")]
    public GameObject m_goSnowball = null;
    //---------------
    // Player Circle
    //---------------
    [LabelOverride("Player Circle")]
    [Tooltip("A GameObject that stores the player circle.")]
    public GameObject m_goPlayerCircle = null;
    //-------------------------
    // Character Ring Material
    //-------------------------
    [LabelOverride("Character Ring Material")]
    [Tooltip("A material that stores the character ring material.")]
    public Material m_matCharacterRing = null;
    //---------------------
    // Character Ring Full
    //---------------------
    [LabelOverride("Full Character Ring Material")]
    [Tooltip("A material that stores the full character ring material.")]
    public Material m_matCharacterRingFull = null;
    //----------------
    // Snowball Speed
    //----------------
    [LabelOverride("Snowball Speed")]
    [Tooltip("A float that stores the snowball's shoot speed.")]
    public float m_fSnowballSpeed = 1750.0f;
    //------------
    // Max Charge
    //------------
    [LabelOverride("Max Charge")]
    [Tooltip("A float that stores the maximum charge length in seconds.")]
    public float m_fMaxCharge = 2.0f;
    //-----------
    // Min Power
    //-----------
    [LabelOverride("Min Power")]
    [Tooltip("A float that stores the minimum power of a throw at lowest charge.")]
    public float m_fPowerMin = 250.0f;
    //-----------
    // Max Power
    //-----------
    [LabelOverride("Max Power")]
    [Tooltip("A float that stores the maximum power of a throw at full charge.")]
    public float m_fPowerMax = 1750.0f;
    //------------
    // Slow Speed
    //------------
    [LabelOverride("Slow Speed")]
    [Tooltip("A float that represents how much slower the player is while charging up a throw.")]
    public float m_fSlowSpeed = 2.5f;

    //--------------------------------
    // Power Towards Centre of Screen
    //--------------------------------
    [LabelOverride("Power Towards Centre of Screen")]
    [Tooltip("When the player is hit whilst holding a ball, it throws it towards the SnowballTarget. This is that force.")]
    public float m_fPowerOfTowardsCentre = 5.0f;

    //-----------------
    // Snowball Target
    //-----------------
    [LabelOverride("Snowball Target")]
    [Tooltip("Snowball target that the ball will be thrown towards.")]
    public GameObject m_goSnowBallTarget = null;
    private Vector3 v3SnowballTarget;

    [HideInInspector]
    public bool m_bThrowBall = false;

    [HideInInspector]
    public bool m_bHasBall = false;

    // xbox max scale of trigger when pressed down
    private const float m_MaxTriggerHeight = 1.21f;
    // private bool m_bWasHit = false;
    // charge power throw
    private bool m_bThrow = false;
    private float m_fChargeModifier = 0.0f;
    [HideInInspector]
    public float m_fChargeTimer = 0.0f;
    private float m_fPowerRange = 0.0f;
    private bool m_bCharging = false;
    private bool m_bGo = false;
    private float m_fIsChargedTimer = 0.0f;
    private float m_fIsChargedTimerLimit = 2.0f;
    private bool bChargedLimitReached = false;
    private bool bDuringMaxCharge = false;
    // xbox controller trigger release
    private bool m_bHolding = false;
    private bool m_bReleased = false;
    private bool bAimOverride = false;
    //--------
    // IFrame
    //--------
    //----------------
    // Iframe Timer
    //----------------
    [LabelOverride("iFrame Timer")]
    [Tooltip("This is the amount of time in seconds that the player is invincible for after being hit. Default is 0.5 seconds")]
    public float m_fIFrame;
    private bool m_bIFrame = false;
    private float m_fIFrameTimer = 5f;
    //private bool m_bCanPickUp = true;

    //----------------
    // Health UI Text
    //----------------
    //[LabelOverride("UI Health Text")]
    //[Tooltip("This stores the UI text of the player's health.")]
    //public Text m_txtHealth;
    //--------------
    // Spawn Health
    //--------------
    [LabelOverride("Spawn Health")]
    [Tooltip("An int that is how much health the players spawn with.")]
    public int m_nSpawnHealth = 20;
    //----------------
    // Current Health
    //----------------
    [LabelOverride("Current Health")]
    [Tooltip("An int that represents how much health the player currently has.")]
    public int m_nCurrentHealth;
    //-----------
    // Stun Time
    //-----------
    [LabelOverride("Stun Time On Hit")]
    [Tooltip("This allocates how long the stun is when the players get hit by the snowball.")]
    public float m_fStun = 0.2f;

    [HideInInspector]
    public bool m_bAlive = true;

    //---------------
    // Player Damage
    //---------------
    private Color m_mainColor = Color.white;
    private bool m_tookDmg = false;
    private float m_DamageTimer = 0.3f;

    //--------------
    // Player Death
    //--------------
    [LabelOverride("The Player's Mesh")]
    [Tooltip("This is so the code knows which mesh to turn red when hurt.")]
    public SkinnedMeshRenderer m_smrCharacterMesh = null;

    //----------------
    // Giant SnowBall
    //----------------
    [HideInInspector]
    public bool m_bHitByGiantSnowBall = false;
    private float m_fHitTimer = 0.0f;

    //--------------------------
    // Giant Snowball Stun Time
    //--------------------------
    [LabelOverride("Snowman Stun Time")]
    [Tooltip("The time players are stunned after being knocked back by the Snowman.")]
    public float m_fGiantSnowballStunTime = 0.25f;

    //----------------
    // Player Reticle
    //----------------
    //[LabelOverride("Player Reticle")]
    //[Tooltip("This stores the GameObject of the player reticle.")]
    //public GameObject m_goPlayerReticle = null;

    //[HideInInspector]
    //public GameObject m_goPlayerReticleCopy = null;
    //[HideInInspector]
    //public PlayerRetical m_scpPlayerReticle;

    //----------
    // Animator
    //----------
    private Animator m_Animator;

    //---------------------------
    // Color of Thrown Snowballs
    //---------------------------
    Color m_clrTeddyBear = Color.green;
    Color m_clrSuperGirl = Color.magenta;
    Color m_clrWinterClothes = Color.blue;
    Color m_clrHelicopterHat = Color.red;


    //-----------------------------
    // Use this for initialization
    //-----------------------------
    void Start()
    {
        //m_goPlayerReticleCopy = Instantiate(m_goPlayerReticle, new Vector3(10.0f, 1.01f, -7.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
        //m_goPlayerReticleCopy.GetComponent<PlayerRetical>().m_player = gameObject;
        //m_scpPlayerReticle = m_goPlayerReticleCopy.GetComponent<PlayerRetical>();
        //m_goPlayerReticleCopy.SetActive(false);

        //Xbox Stick Axis'
        m_axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, controller);
        m_axisY = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);

        m_rb = GetComponent<Rigidbody>();
        m_fCurrentSpeed = m_fSpeed;

        //--------
        // Health
        //--------
        m_nCurrentHealth = m_nSpawnHealth;
        //SetHealthText();

        m_Animator = transform.GetChild(0).GetComponent<Animator>();
    }

    //--------------------------------------------------------
    // FixedUpdate is called once per frame
    //--------------------------------------------------------
    //void FixedUpdate()
    //{

    //}
    void OnDrawGizmosSelected()
    {

        //Debug.Log(transform.position);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position + m_v3XboxDashDir, 0.3f);
    }
    //--------------------------------------------------------
    //
    //--------------------------------------------------------
    void Update()
    {
        Dash scpDash = gameObject.GetComponent<Dash>();
        AbilitySnowMan scpSnowMan = gameObject.GetComponent<AbilitySnowMan>();
        EliminatedAbilityGiantSnowBall scpGiantSnowBall = gameObject.GetComponent<EliminatedAbilityGiantSnowBall>();



        if (m_bAlive)
        {
            transform.LookAt(transform.position + m_v3XboxDashDir * Time.deltaTime);

            Movement();

            Aiming();

            if (!m_bCharging)
                scpDash.DoDash();

            if (scpDash.m_bDashing && m_bHasBall)
                gameObject.layer = LayerMask.NameToLayer("PlayerDash");
            else
                gameObject.layer = LayerMask.NameToLayer("Player");

            Shoot();

            scpSnowMan.CreateSnowMan();

            Projectiles();
        }

        Health();

        if (m_bIFrame)
        {
            m_fIFrameTimer += Time.deltaTime;
            //m_bHasBall = false;
            if (m_fIFrameTimer > m_fIFrame)
            {
                m_bIFrame = false;
                m_fIFrameTimer = 0f;
            }
        }
        if (!m_bAlive)
        {
            //scpGiantSnowBall.DoEliminatedAbilityGiantSnowBall();
        }

        if (m_bHitByGiantSnowBall)
        {
            //try to make their velocity completely 0 and then knockback.
            //if (scpDash.m_bDashing)
            // scpDash.m_bDashing = false;

            m_fHitTimer += Time.deltaTime;
            m_bMovementLock = true;
        }

        if (m_fHitTimer > m_fGiantSnowballStunTime && m_rb.velocity.magnitude < 2.0f && m_bHitByGiantSnowBall)
        {
            m_bHitByGiantSnowBall = false;
            m_fHitTimer = 0.0f;
            m_bMovementLock = false;
            scpDash.m_fDashTimer = scpDash.m_fDashDuration + 1.0f;
        }

        if (m_bHasBall)
        {
            m_goPlayerCircle.GetComponent<MeshRenderer>().material = m_matCharacterRingFull; //set to new mat
        }

        else if (!m_bHasBall)
        {
            m_goPlayerCircle.GetComponent<MeshRenderer>().material = m_matCharacterRing; //set to new mat
        }


        if (!m_bHitByGiantSnowBall)
        {
            //stop sliding
            m_rb.velocity = Vector3.zero;
            m_rb.angularVelocity = Vector3.zero;

        }
        //Debug.Log(m_rb.velocity.magnitude);
        //m_Anim.SetFloat("running", m_rb.velocity.magnitude);
    }

    //--------------------------------------------------------
    // Movement
    //--------------------------------------------------------
    private void Movement()
    {
        Vector3 v3VerticalAxis = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && controller == XboxController.First)
            v3VerticalAxis.z = 1.0f;
        else if (Input.GetKey(KeyCode.S) && controller == XboxController.First)
            v3VerticalAxis.z = -1.0f;
        else
            v3VerticalAxis.z = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);


        Vector3 v3HorizontalAxis = Vector3.zero;

        if (Input.GetKey(KeyCode.D) && controller == XboxController.First)
            v3HorizontalAxis.x = 1.0f;
        else if (Input.GetKey(KeyCode.A) && controller == XboxController.First)
            v3HorizontalAxis.x = -1.0f;
        else
            v3HorizontalAxis.x = XCI.GetAxisRaw(XboxAxis.LeftStickX, controller);

        Vector3 v3Pos;
        v3Pos.x = transform.position.x;
        v3Pos.z = transform.position.z;

        if (!m_bMovementLock)
        {
            if (m_bCharging)
                m_fCurrentSpeed = m_fSlowSpeed;
            else
                m_fCurrentSpeed = m_fMaxSpeed;

            // Up and down movement
            m_v3MovePos = Vector3.zero;

            m_v3MovePos += v3VerticalAxis * Time.deltaTime * m_fCurrentSpeed;

            if (m_v3MovePos.magnitude > m_fMaxSpeed * Time.deltaTime)
            {
                m_v3MovePos.Normalize();
                m_v3MovePos *= m_fMaxSpeed * Time.deltaTime;
            }

            // Left and right movement
            m_v3MovePos += v3HorizontalAxis * Time.deltaTime * m_fCurrentSpeed;

            if (m_v3MovePos.magnitude > m_fMaxSpeed * Time.deltaTime)
            {
                m_v3MovePos.Normalize();
                m_v3MovePos *= m_fMaxSpeed * Time.deltaTime;
            }

            m_rb.AddForce(m_v3MovePos * 60, ForceMode.Impulse);
            //m_rb.MovePosition(m_rb.position + m_v3MovePos);
        }

        m_Animator.SetFloat("running", m_v3MovePos.magnitude);
    }

    //--------------------------------------------------------
    // Aiming
    //--------------------------------------------------------
    private void Aiming()
    {
        //if (m_bMovementLock)
        //    Debug.Log("Hamo");
        if (!m_bMovementLock)
        {
            if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) != 0 || XCI.GetAxisRaw(XboxAxis.RightStickY, controller) != 0)
                bAimOverride = true;
            else
                bAimOverride = false;


            if (!bAimOverride)
            {
                //-------------------------
                // Xbox Left Stick Aiming
                //-------------------------
                m_v3XboxDashDir = transform.forward;
                if (!m_bLeftTriggerPressed)
                {
                    m_axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, controller);
                    m_axisY = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);

                    //Debug.Log("Right Stick X: " + axisX + " Right Stick Y: " + axisY);

                    if (m_axisX != 0.0f || m_axisY != 0.0f)
                    {
                        m_v3XboxDashDir = new Vector3(m_axisX, 0.0f, m_axisY);
                        transform.forward = m_v3XboxDashDir;
                    }
                }
            }

            else
            {
                //-------------------------
                // Xbox Right Stick Aiming
                //-------------------------
                m_v3XboxDashDir = transform.forward;
                if (!m_bLeftTriggerPressed)
                {
                    m_axisX = XCI.GetAxisRaw(XboxAxis.RightStickX, controller);
                    m_axisY = XCI.GetAxisRaw(XboxAxis.RightStickY, controller);

                    //Debug.Log("Right Stick X: " + axisX + " Right Stick Y: " + axisY);

                    if (m_axisX != 0.0f || m_axisY != 0.0f)
                    {
                        m_v3XboxDashDir = new Vector3(m_axisX, 0.0f, m_axisY);
                        transform.forward = m_v3XboxDashDir;
                    }
                }
            }
        }

        //-------------- 
        // Mouse Aiming
        //--------------
        if (!XCI.IsPluggedIn(1) && !m_bLeftTriggerPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            Vector3 v3Target = hit.point;
            v3Target.y = transform.position.y;
            transform.LookAt(v3Target);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_v3DashDir = hit.point - transform.position;
                m_v3DashDir.y = 0.0f;
                m_v3DashDir.Normalize();
                m_v3MovePos.Normalize();
            }
        }
        //}
    }

    //--------------------------------------------------------
    // Shoot
    //--------------------------------------------------------
    private void Shoot()
    {
        //if the player don't have a ball don't let them charge!
        if (!m_bHasBall)
            ResetChargeThrow();

        // if not throwing set animation to false
        m_Animator.SetBool("throwing", false);

        // calculate the range of power to do the math on the charge power throw.
        m_fPowerRange = m_fPowerMax - m_fPowerMin;

        float rightTrigHeight = m_MaxTriggerHeight * (1.0f - XCI.GetAxisRaw(XboxAxis.RightTrigger, controller));
        m_bThrow = (Input.GetKey(KeyCode.Mouse0) && controller == XboxController.First) || (rightTrigHeight < 1.0f);

        if (rightTrigHeight < 1.0f && m_bHasBall)
        {
            m_bHolding = true;
            m_Animator.SetBool("throwing", true);
        }

        if (m_bHolding && rightTrigHeight > 1.0f)
        {
            m_bHolding = false;
            m_bReleased = true;
            m_Animator.SetBool("throwing", false);
        }
        else
            m_bReleased = false;

        m_bGo = ((Input.GetKeyUp(KeyCode.Mouse0) && controller == XboxController.First) || m_bReleased);

        Dash scpDash = gameObject.GetComponent<Dash>();
        ChargeSlider scpSlider = gameObject.GetComponent<ChargeSlider>();

        if (m_bThrow && !scpDash.m_bDashing && m_bHasBall || m_bGo || bDuringMaxCharge || m_bThrowBall)
        {


            if (m_fChargeTimer < m_fMaxCharge && !m_bThrowBall)
            {
                m_fChargeTimer += Time.deltaTime;
                //scpSlider.m_sliChargeSlider.value = m_fChargeTimer;
                m_bCharging = true;
                m_Animator.SetBool("maxcharge", false);
            }
            else if (m_fChargeTimer >= m_fMaxCharge && !m_bThrowBall)
            {
                m_fChargeTimer = m_fMaxCharge;
            }

            if (m_fChargeTimer >= m_fMaxCharge && m_bThrow && !m_bThrowBall)
            {
                m_fIsChargedTimer += Time.deltaTime;
                //Debug.Log("AT MAX POWER " + m_fIsChargedTimer);

                if (m_fIsChargedTimer <= m_fIsChargedTimerLimit && !m_bThrowBall)
                {
                    bDuringMaxCharge = true;
                }
                else if (m_fIsChargedTimer >= m_fIsChargedTimerLimit && !m_bThrowBall)
                {
                    m_bCharging = false;
                    bChargedLimitReached = true;
                    m_Animator.SetBool("maxcharge", true);
                }
            }

            if (m_bGo || bChargedLimitReached && !m_bThrowBall)
            {
                m_bCharging = false;
            }

            if (m_bHasBall && !m_bCharging || m_bThrowBall)
            {
                m_fChargeModifier = m_fChargeTimer / m_fMaxCharge;
                m_fSnowballSpeed = m_fChargeModifier * m_fPowerRange + m_fPowerMin;
                //Debug.Log("throwing");

                if (m_bThrowBall && m_bHasBall)
                {
                    AudioManager.m_SharedInstance.PlayThrowAudio();
                    RaycastHit hit;
                    // shoots out back
                    if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 1.2f) && (hit.collider.gameObject.tag != "Character" && hit.collider.gameObject.tag != "Snowball"))
                    {
                        // get access to the snowball in the object pool
                        GameObject copy = ObjectPool.m_SharedInstance.GetPooledObject();
                        // change the snowballs material to the players material

                        // set the spawn point of the snowball at the players arm position
                        copy.transform.position = transform.position + -transform.forward + (transform.up * 0.6f);
                        // get the snowballs rigidbody
                        Rigidbody rb = copy.GetComponent<Rigidbody>();
                        // apply a 'throwing' force to the snowball
                        rb.AddForce(transform.forward * m_fSnowballSpeed * -0.5f, ForceMode.Acceleration);
                        // move the snowball to the 'Projectiles' gameobject in the heirarcy to make things a bit neater
                        copy.transform.parent = GameObject.FindGameObjectWithTag("Projectiles").transform;
                        // The ball is thrown so it becomes false
                        m_bHasBall = false;
                        // tell the animator the ball is finished being thrown
                        m_Animator.SetBool("throwing", false);

                        ResetChargeThrow();
                    }
                    // shoots out front
                    else
                    {
                        // get access to the snowball in the object pool
                        GameObject copy = ObjectPool.m_SharedInstance.GetPooledObject();
                        // change the snowballs material to the players material
                        Snowball scpSnowball = copy.GetComponent<Snowball>();

                        if (gameObject.name == "CharacterTeddyBear")
                            scpSnowball.m_materials[1].color = m_clrTeddyBear;

                        if (gameObject.name == "CharacterSupergirl")
                            scpSnowball.m_materials[1].color = m_clrSuperGirl;

                        if (gameObject.name == "CharacterWinterClothes")
                            scpSnowball.m_materials[1].color = m_clrWinterClothes;

                        if (gameObject.name == "CharacterHelicopterHat")
                            scpSnowball.m_materials[1].color = m_clrHelicopterHat;

                        // set the spawn point of the snowball at the players arm position
                        copy.transform.position = transform.position + transform.forward + (transform.up * 0.6f) * 1;
                        // get the snowballs rigidbody
                        Rigidbody rb = copy.GetComponent<Rigidbody>();
                        // apply a 'throwing' force to the snowball
                        rb.AddForce(transform.forward * m_fSnowballSpeed, ForceMode.Acceleration);
                        // move the snowball to the 'Projectiles' gameobject in the heirarcy to make things a bit neater
                        copy.transform.parent = GameObject.FindGameObjectWithTag("Projectiles").transform;
                        // The ball is thrown so it becomes false
                        m_bHasBall = false;
                        // tell the animator the ball is finished being thrown
                        m_Animator.SetBool("throwing", false);

                        ResetChargeThrow();
                    }
                }
            }
        }
        else
            m_fChargeTimer = 0.0f;
    }

    private void ResetChargeThrow()
    {
        // resets everything required for the charge throw to work
        m_fChargeModifier = 0.0f;
        m_bThrow = false;
        m_fChargeTimer = 0.0f;
        m_bCharging = false;
        m_bGo = false;
        bDuringMaxCharge = false;
        m_fIsChargedTimer = 0.0f;
        bChargedLimitReached = false;
        m_bThrowBall = false;
    }

    //--------------------------------------------------------
    // Health
    //--------------------------------------------------------
    private void Health()
    {
        SnowMan scpSnowMan = gameObject.GetComponent<SnowMan>();

        if (m_tookDmg)
        {
            m_DamageTimer -= Time.deltaTime;

            if (m_DamageTimer <= 0.3f)
            {
                // color red hurt
                m_smrCharacterMesh.material.color = new Color(1.0f, 0.0f, 0.0f, 255.0f);
            }
            if (m_DamageTimer < 0.0f)
            {
                m_tookDmg = false;
                m_smrCharacterMesh.material.color = m_mainColor;
                m_DamageTimer = 0.3f;
            }
        }

        if (m_nCurrentHealth <= 0 && m_bAlive)
        {
            //m_txtHealth.text = null;


            //scpSnowMan.m_bASnowManExists = false;

            if (m_bHasBall)
            {
                GameObject copy = ObjectPool.m_SharedInstance.GetPooledObject();
                copy.transform.position = transform.position + transform.forward;
                m_bHasBall = false;
            }

            Destroy(gameObject.GetComponent<SnowMan>());

            m_bAlive = false;

            gameObject.AddComponent<PlayerAI>();


            //ScoreManager scpScoreManager = gameObject.GetComponent<ScoreManager>();

            ScoreManager.PlayerFinish(((int)controller) - 1);

            //kill the player
            //mrCharacterMesh.enabled = false;
            //colPlayer.enabled = false;
            //mrWeapon.enabled = false;
            //mrPlayerCircle.enabled = false;
            //mrReticleCol.enabled = true;
            //rb.rotation = Quaternion.identity;
            //m_goPlayerReticleCopy.SetActive(true);

            //gameObject.transform.position = new Vector3(10.2f, 1.0f, -7.0f);

            m_nCurrentHealth = 0;

            //updating the health value onscreen
            //SetHealthText();

            //GameObject copy = Instantiate(scpSnowMan.SnowMan);
            //copy.transform.position = transform.position;
        }

        if (!m_bAlive)
        {
            m_fCurrentSpeed = m_fSpeed;
            //m_scpPlayerReticle.Movement();
        }
    }

    //--------------------------------------------------------
    // Updates the health value displayed onscreen
    //--------------------------------------------------------
    //void SetHealthText()
    //{
    //    if (m_txtHealth)
    //        m_txtHealth.text = "HP:" + m_nCurrentHealth.ToString();
    //}

    private void TakeDamage()
    {
        m_bIFrame = true;
        m_tookDmg = true;
        m_nCurrentHealth = m_nCurrentHealth - Snowball.m_nScoreValue;
        // updating the health value onscreen
        //SetHealthText();
        AudioManager.m_SharedInstance.PlayHurtAudio();
    }

    private void Projectiles()
    {
        Dash scpDash = gameObject.GetComponent<Dash>();
        Snowball scpSnowball = m_goSnowball.GetComponent<Snowball>();
        //Dash scpDash = gameObject.GetComponent<Dash>();

        if (!scpSnowball.m_bTooFast && m_bHasBall && !scpDash.m_bDashing)
        {
            Snowball[] arrSnowballs = FindObjectsOfType<Snowball>();
            for (int i = 0; i < arrSnowballs.Length; i++)
            {
                Physics.IgnoreCollision(arrSnowballs[i].gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
            }
        }
    }

    //--------------------------------------------------------
    //
    //
    //    Param:
    //          col:
    //
    //--------------------------------------------------------
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Character")
        {
            Player scpColPlayer = col.gameObject.GetComponent<Player>();
            Dash scpColPlayerDash = col.gameObject.GetComponent<Dash>();

            if (scpColPlayerDash.m_bDashing)
            {
                //Debug.Log("Hit whilst dashing");
                if (m_bHasBall && !scpColPlayer.m_bHasBall)
                {
                    m_bHasBall = false;
                    scpColPlayer.m_bHasBall = true;
                }
            }
        }

        if (col.gameObject.tag == "Snowball")
        {
            Snowball scpSnowball = col.gameObject.GetComponent<Snowball>();
            Dash scpDash = gameObject.GetComponent<Dash>();

            // Ball is moving fast
            if (scpSnowball.m_bTooFast)
            {
                if (m_bHasBall)
                {
                    if (!m_bIFrame)
                        TakeDamage();

                    //Drop ball
                    m_bHasBall = false;
                    //m_bWasHit = true;
                    GameObject copy = ObjectPool.m_SharedInstance.GetPooledObject();

                    // Snowball is thrown towards centre
                    Vector3 dir = m_goSnowBallTarget.transform.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    dir.y = 0.5f;
                    dir.Normalize();
                    copy.transform.position = transform.position + (transform.up * 2.0f);
                    copy.GetComponent<Rigidbody>().AddForce(dir * m_fPowerOfTowardsCentre, ForceMode.Impulse);
                }
                else if (!m_bHasBall && scpDash.m_bDashing)
                {
                    //TakeDamage();
                    // The player has picked it up
                    col.gameObject.SetActive(false);
                    //Destroy(col.gameObject);
                    m_bHasBall = true;
                }
                else if (!scpDash.m_bDashing && !m_bHasBall && !m_bIFrame)
                {
                    TakeDamage();
                }
            }
            else // Ball is moving slow
            {
                //if (scpDash.m_bDashing)
                //{
                //    Physics.IgnoreCollision(col.collider, GetComponent<Collider>(), true);
                //}
                if (!m_bIFrame)
                {
                    if (!m_bHasBall)
                    {
                        // The player has picked it up
                        col.gameObject.SetActive(false);
                        //Destroy(col.gameObject);
                        m_bHasBall = true;
                    }
                }

            }
        }
    }
}




////-----------------
//// Ability Snowball
////-----------------
//public GameObject SnowBall = null;
//public float fSnowballSpeed = 1000.0f;
//public float fSlowSpeed = 2.5f;
//private bool bSlow = false;
//private float fSlowTimer = 0.0f;

////------------------
//// Ability Snowball
////------------------
//if (XCI.GetButtonDown(XboxButton.LeftBumper, controller))
//{
//    GameObject copy = Instantiate(SnowBall);
//    copy.transform.position = transform.position + (transform.forward * 0.5f) + transform.up;
//    Rigidbody rb = copy.GetComponent<Rigidbody>();
//    rb.AddForce(transform.forward * fSnowballSpeed, ForceMode.Acceleration);
//}

////------------------
//// Ability Snowball
////------------------
//if (Input.GetKeyDown(KeyCode.Mouse1))
//{
//    GameObject copy = Instantiate(SnowBall);
//    copy.transform.position = transform.position + transform.forward + transform.up;
//    Rigidbody rb = copy.GetComponent<Rigidbody>();
//    rb.AddForce(transform.forward * fSnowballSpeed, ForceMode.Acceleration);
//}

//// This is the function that slows the player when hit by SnowBall
//public void Slow()
//{
//    //float fSlowTemp = 0.0f;
//    //float fOldSpeed = 0.0f;
//    //fOldSpeed = fSpeed;
//    //fSlowTemp = (fSpeed * fSlowSpeed);
//    //fSpeed = fSlowTemp;
//    bSlow = true;
//    fSlowTimer = 0.0f;
//    fCurrentSpeed = fSlowSpeed;
//    //Wait for 2 seconds here
//    //fSpeed = fOldSpeed;
//}

//    if (bSlow)
//    {
//        fSlowSpeed += Time.deltaTime;
//        if (fSlowSpeed > 2.0f)
//        {
//            bSlow = false;
//            fCurrentSpeed = fSpeed;
//        }
//    }