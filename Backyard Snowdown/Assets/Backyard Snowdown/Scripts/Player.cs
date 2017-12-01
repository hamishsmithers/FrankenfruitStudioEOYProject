//------------------------------------------------------------------------------------------
// Filename:        Player.cs
//
// Description:     Players are the main feature of the game, they have health, can pick 
//                  up snowballs and throw them to hurt other players, they have 
//                  collision detection with multiple objects. 
//                  This script holds all logic for the player minus the dash mechanic.
//                  The player also handles changing the color of snowballs that it shoots.
//
// Authors:         Mitchell Cattini-Schultz, Nathan Nette
// Editors:         Mitchell Cattini-Schultz, Nathan Nette
//------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Player : MonoBehaviour
{
    public XboxController controller;

    //------------------------------------------------------------------------------------------
    // Player's Movement Speed
    // we have exposed m_fSpeed to the designers so they can bug test
    //------------------------------------------------------------------------------------------
    [LabelOverride("Speed")]
    [Tooltip("Player's Movement Speed.")]
    public float m_fSpeed = 5.0f;

    //------------------------------------------------------------------------------------------
    // Player's Max Movement Speed
    //------------------------------------------------------------------------------------------
    [LabelOverride("Max Speed")]
    [Tooltip("Player's maximum movement speed.")]
    public float m_fMaxSpeed = 5.0f;

    //------------------------------------------------------------------------------------------
    // Dash Speed
    //------------------------------------------------------------------------------------------
    [LabelOverride("Dash Speed")]
    [Tooltip("The speed that the player moves while dashing.")]
    public float m_fDashSpeed = 10.0f;

    //------------------------------------------------------------------------------------------
    // Dash Duration
    //------------------------------------------------------------------------------------------
    [LabelOverride("Dash Duration")]
    [Tooltip("The duration of the dash.")]
    public float m_fDashDuration = 0.5f;

    //------------------------------------------------------------------------------------------
    // Player Model
    //------------------------------------------------------------------------------------------
    [LabelOverride("Player Model")]
    [Tooltip("Stores the player model")]
    public GameObject m_goPlayerModel = null;

    //------------------------------------------------------------------------------------------
    // A float to store the current movement speed of the player
    //------------------------------------------------------------------------------------------
    [HideInInspector]
    public float m_fCurrentSpeed = 5.0f;

    //------------------------------------------------------------------------------------------
    // Stops the players being able to move
    //------------------------------------------------------------------------------------------
    [HideInInspector]
    public bool m_bMovementLock = false;

    //------------------------------------------------------------------------------------------
    // Bool to check whether the left trigger has been pressed
    //------------------------------------------------------------------------------------------
    [HideInInspector]
    public bool m_bLeftTriggerPressed = false;

    //------------------------------------------------------------------------------------------
    // A Vector3 to store the current movement position of the player
    //------------------------------------------------------------------------------------------
    [HideInInspector]
    public Vector3 m_v3MovePos;

    //------------------------------------------------------------------------------------------
    // Vector3 to hold the Xbox dash direction
    //------------------------------------------------------------------------------------------
    [HideInInspector]
    public Vector3 m_v3XboxDashDir;

    //------------------------------------------------------------------------------------------
    // A float to store the axis X
    //------------------------------------------------------------------------------------------
    private float m_axisX;

    //------------------------------------------------------------------------------------------
    // A float to store the axis Y
    //------------------------------------------------------------------------------------------
    private float m_axisY;

    //------------------------------------------------------------------------------------------
    // RIGIDBODY
    //------------------------------------------------------------------------------------------
    [HideInInspector]
    public Rigidbody m_rb;

    //------------------------------------------------------------------------------------------
    // Vector3 to store the direction of the player to dash
    //------------------------------------------------------------------------------------------
    private Vector3 m_v3DashDir;

    //------------------------------------------------------------------------------------------
    // Snowball
    //------------------------------------------------------------------------------------------
    [LabelOverride("Snowball")]
    [Tooltip("Stores the Snowball GameObject.")]
    public GameObject m_goSnowball = null;

    //------------------------------------------------------------------------------------------
    // Player Circle
    //------------------------------------------------------------------------------------------
    [LabelOverride("Player Circle")]
    [Tooltip("A GameObject that stores the player circle.")]
    public GameObject m_goPlayerCircle = null;

    //------------------------------------------------------------------------------------------
    // Character Ring Material
    //------------------------------------------------------------------------------------------
    [LabelOverride("Character Ring Material")]
    [Tooltip("A material that stores the character ring material.")]
    public Material m_matCharacterRing = null;

    //------------------------------------------------------------------------------------------
    // Character Ring Full
    //------------------------------------------------------------------------------------------
    [LabelOverride("Full Character Ring Material")]
    [Tooltip("A material that stores the full character ring material.")]
    public Material m_matCharacterRingFull = null;

    //------------------------------------------------------------------------------------------
    // Snowball Speed
    //------------------------------------------------------------------------------------------
    [LabelOverride("Snowball Speed")]
    [Tooltip("A float that stores the snowball's shoot speed.")]
    public float m_fSnowballSpeed = 1750.0f;

    //------------------------------------------------------------------------------------------
    // Max Charge
    //------------------------------------------------------------------------------------------
    [LabelOverride("Max Charge")]
    [Tooltip("A float that stores the maximum charge length in seconds.")]
    public float m_fMaxCharge = 2.0f;

    //------------------------------------------------------------------------------------------
    // Min Power
    //------------------------------------------------------------------------------------------
    [LabelOverride("Min Power")]
    [Tooltip("A float that stores the minimum power of a throw at lowest charge.")]
    public float m_fPowerMin = 250.0f;

    //------------------------------------------------------------------------------------------
    // Max Power
    //------------------------------------------------------------------------------------------
    [LabelOverride("Max Power")]
    [Tooltip("A float that stores the maximum power of a throw at full charge.")]
    public float m_fPowerMax = 1750.0f;

    //------------------------------------------------------------------------------------------
    // Slow Speed
    //------------------------------------------------------------------------------------------
    [LabelOverride("Slow Speed")]
    [Tooltip("A float that represents how much slower the player is while charging up a throw.")]
    public float m_fSlowSpeed = 2.5f;

    //------------------------------------------------------------------------------------------
    // Power Towards Centre of Screen
    //------------------------------------------------------------------------------------------
    [LabelOverride("Power Towards Centre of Screen")]
    [Tooltip("When the player is hit whilst holding a ball, it throws it towards the SnowballTarget. This is that force.")]
    public float m_fPowerOfTowardsCentre = 5.0f;

    //------------------------------------------------------------------------------------------
    // Snowball Target
    //------------------------------------------------------------------------------------------
    [LabelOverride("Snowball Target")]
    [Tooltip("Snowball target that the ball will be thrown towards.")]
    public GameObject m_goSnowBallTarget = null;

    [HideInInspector]
    public bool m_bThrowBall = false;

    [HideInInspector]
    public bool m_bHasBall = false;

    [HideInInspector]
    public float m_fChargeTimer = 0.0f;

    // xbox max scale of trigger when pressed down
    private const float m_MaxTriggerHeight = 1.21f;
    private bool m_bThrow = false;
    private float m_fChargeModifier = 0.0f;
    private float m_fPowerRange = 0.0f;
    private bool m_bCharging = false;
    private float m_fIsChargedTimer = 0.0f;
    private float m_fIsChargedTimerLimit = 2.0f;
    private bool bDuringMaxCharge = false;
    private bool m_bReleased = false;
    private bool bAimOverride = false;

    //------------------------------------------------------------------------------------------
    // Iframe Timer
    //------------------------------------------------------------------------------------------
    [LabelOverride("iFrame Timer")]
    [Tooltip("This is the amount of time in seconds that the player is invincible for after being hit. Default is 0.5 seconds")]
    public float m_fIFrame;
    private bool m_bIFrame = false;
    private float m_fIFrameTimer = 0.0f;
    //private bool m_bCanPickUp = true;


    //------------------------------------------------------------------------------------------
    // Spawn Health
    // we have exposed this variable to the desginers so that they can test with different start healths
    //------------------------------------------------------------------------------------------
    [LabelOverride("Spawn Health")]
    [Tooltip("An int that is how much health the players spawn with.")]
    public int m_nSpawnHealth = 20;

    //------------------------------------------------------------------------------------------
    // Current health
    // this variable is so they can kill players at anytime for simpler testing
    //------------------------------------------------------------------------------------------
    [LabelOverride("Current Health")]
    [Tooltip("An int that represents how much health the player currently has.")]
    public int m_nCurrentHealth;

    //------------------------------------------------------------------------------------------
    // Stun Time
    //------------------------------------------------------------------------------------------
    [LabelOverride("Stun Time On Hit")]
    [Tooltip("This allocates how long the stun is when the players get hit by the snowball.")]
    public float m_fStun = 0.2f;

    [HideInInspector]
    public bool m_bAlive = true;

    //------------------------------------------------------------------------------------------
    // Player Damage
    //------------------------------------------------------------------------------------------
    private Color m_mainColor = Color.white;
    private bool m_tookDmg = false;
    private float m_DamageTimer = 0.3f;

    //------------------------------------------------------------------------------------------
    // Player Death
    //------------------------------------------------------------------------------------------
    [LabelOverride("The Player's Mesh")]
    [Tooltip("This is so the code knows which mesh to turn red when hurt.")]
    public SkinnedMeshRenderer m_smrCharacterMesh = null;

    //------------------------------------------------------------------------------------------
    // Giant SnowBall
    //------------------------------------------------------------------------------------------
    [HideInInspector]
    public bool m_bHitByGiantSnowBall = false;
    private float m_fHitTimer = 0.0f;

    //------------------------------------------------------------------------------------------
    // Giant Snowball Stun Time
    //------------------------------------------------------------------------------------------
    [LabelOverride("Snowman Stun Time")]
    [Tooltip("The time players are stunned after being knocked back by the Snowman.")]
    public float m_fGiantSnowballStunTime = 0.25f;

    //------------------------------------------------------------------------------------------
    // Player Reticle
    //------------------------------------------------------------------------------------------
    //[LabelOverride("Player Reticle")]
    //[Tooltip("This stores the GameObject of the player reticle.")]
    //public GameObject m_goPlayerReticle = null;

    //[HideInInspector]
    //public GameObject m_goPlayerReticleCopy = null;
    //[HideInInspector]
    //public PlayerRetical m_scpPlayerReticle;

    //------------------------------------------------------------------------------------------
    // Animator
    //------------------------------------------------------------------------------------------
    private Animator m_Animator;

    //------------------------------------------------------------------------------------------
    // Material of Thrown Snowballs
    //------------------------------------------------------------------------------------------
    [LabelOverride("Material of Thrown Snowball")]
    [Tooltip("Drag and drop the respective material here.")]
    public Material m_matSnowball = null;

    //------------------------------------------------------------------------------------------
    // IFrame Flicker Rate
    //------------------------------------------------------------------------------------------
    //[LabelOverride("IFrame Flicker Rate")]
    //[Tooltip("Per Second.")]
    //public float m_bIFrameFlickerTime = 0.05f;

    //------------------------------------------------------------------------------------------
    // Dizzy Particle System
    //------------------------------------------------------------------------------------------
    [LabelOverride("Dizzy Particle System")]
    [Tooltip("Drag and drop the respective particle system here.")]
    public ParticleSystem m_psDizzy;


    //------------------------------------------------------------------------------------------
    // Use this for initialization
    //------------------------------------------------------------------------------------------
    void Start()
    {
        //m_goPlayerReticleCopy = Instantiate(m_goPlayerReticle, new Vector3(10.0f, 1.01f, -7.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
        //m_goPlayerReticleCopy.GetComponent<PlayerRetical>().m_player = gameObject;
        //m_scpPlayerReticle = m_goPlayerReticleCopy.GetComponent<PlayerRetical>();
        //m_goPlayerReticleCopy.SetActive(false);

        //Xbox Stick Axis'
        m_axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, controller);
        m_axisY = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);

        // getting the rigidBody of the players so we can move them
        m_rb = GetComponent<Rigidbody>();

        // setting current speed to original speed 
        m_fCurrentSpeed = m_fSpeed;


        m_nCurrentHealth = m_nSpawnHealth;
        //SetHealthText();

        m_Animator = transform.GetChild(0).GetComponent<Animator>();

        m_psDizzy.Stop();
    }


    //------------------------------------------------------------------------------------------
    // An update runs 50 frames per second vs the usual 60, used for physics calculations.
    //------------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        if (m_bAlive)
        {
            Dash scpDash = gameObject.GetComponent<Dash>();

            if (!m_bCharging)
                scpDash.DoDash();
        }
    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame
    //------------------------------------------------------------------------------------------
    void Update()
    {
        Dash scpDash = gameObject.GetComponent<Dash>();
        //AbilitySnowMan scpSnowMan = gameObject.GetComponent<AbilitySnowMan>();
        //EliminatedAbilityGiantSnowBall scpGiantSnowBall = gameObject.GetComponent<EliminatedAbilityGiantSnowBall>();

        if (m_bAlive)
        {
            // allows players to traverse the map
            Movement();

            // allows players to aim in any desired direction
            Aiming();

            // allows players to dash and phase right through snowballs
            if (scpDash.m_bDashing && m_bHasBall)
                gameObject.layer = LayerMask.NameToLayer("PlayerDash");
            else
                gameObject.layer = LayerMask.NameToLayer("Player");

            // allows players to throw snowballs
            Shoot();

            // allows the player to spawn obstacle snowmen
            //scpSnowMan.CreateSnowMan();

            // allows players to dash and phase right through snowballs
            Projectiles();
        }

        Health();

        // whilst iframes are active, aka when a player is hit by a snowball
        if (m_bIFrame && m_fIFrameTimer <= m_fIFrame)
        {
            m_fIFrameTimer += Time.deltaTime;

            // if dizzy is not playing play dizzy
            if (!m_psDizzy.isPlaying)
            {
                // dizzy particle system plays
                m_psDizzy.Play();
            }

            //if (m_bIFrameFlickerOn && m_bIframeFlickerCount <= m_bIFrameFlickerTime)
            //{
            //    m_bIframeFlickerCount += Time.deltaTime;
            //    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
            //}
            //else if (m_bIFrameFlickerOn && m_bIframeFlickerCount > m_bIFrameFlickerTime)
            //{
            //    m_bIFrameFlickerOn = false;
            //    m_bIframeFlickerCount = 0.0f;
            //}

            //if (!m_bIFrameFlickerOn && m_bIframeFlickerCount <= m_bIFrameFlickerTime)
            //{
            //    m_bIframeFlickerCount += Time.deltaTime;
            //    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
            //}
            //else if (!m_bIFrameFlickerOn && m_bIframeFlickerCount > m_bIFrameFlickerTime)
            //{
            //    m_bIFrameFlickerOn = true;
            //    m_bIframeFlickerCount = 0.0f;
            //}

        }
        else if (m_fIFrameTimer > m_fIFrame)
        {
            // get the mesh renderer
            //gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
            m_bIFrame = false;
            //m_bIframeFlickerCount = 0.0f;
            m_fIFrameTimer = 0.0f;
            // stop the particle system
            m_psDizzy.Stop();
        }

        if (!m_bAlive)
        {
            // if player is dead let them influence the game by dropping stunning snowballs on players
            //scpGiantSnowBall.DoEliminatedAbilityGiantSnowBall();
        }

        // if the player is hit by the jumping snowman, they are stunned
        if (m_bHitByGiantSnowBall)
        {
            //try to make their velocity completely 0 and then knockback
            //if (scpDash.m_bDashing)
            // scpDash.m_bDashing = false;

            m_fHitTimer += Time.deltaTime;
            // don't let players move if they are stunned
            m_bMovementLock = true;
        }

        // after the player has been stunned by the jumping snowman, if they are moving slowly enough 
        // movement controls are restored to them
        if (m_fHitTimer > m_fGiantSnowballStunTime && m_rb.velocity.magnitude < 2.0f && m_bHitByGiantSnowBall)
        {
            m_bHitByGiantSnowBall = false;
            m_fHitTimer = 0.0f;
            // restore movement to the player
            m_bMovementLock = false;
            // honestly I do not remember what this fixes, something to do with if you are dashing under 
            // the jumping snowman and he lands on you, stunning you and, but I cannot re-create it
            scpDash.m_fDashTimer = scpDash.m_fDashDuration + 1.0f;
        }

        // if the player has the snowball fill their player ring to show this.
        if (m_bHasBall)
        {
            m_goPlayerCircle.GetComponent<MeshRenderer>().material = m_matCharacterRingFull; //set to new mat
        }

        // if the player does not have the snowball empty(un-fill) their player ring to show this.
        else if (!m_bHasBall)
        {
            m_goPlayerCircle.GetComponent<MeshRenderer>().material = m_matCharacterRing; //set to new mat
        }

        // if a player is not hit by the jumping snowman then stop sliding, so controls are more predictable
        if (!m_bHitByGiantSnowBall)
        {
            //stop sliding
            m_rb.velocity = Vector3.zero;
            m_rb.angularVelocity = Vector3.zero;
        }
    }

    //------------------------------------------------------------------------------------------
    // The movement function controls the players by using xbox or keyboard input, you can 
    // move with the left analog stick.
    //------------------------------------------------------------------------------------------
    private void Movement()
    {
        Vector3 v3VerticalAxis = Vector3.zero;

        // keyboard and xbox upward and downward movement controls for the first player
        if (Input.GetKey(KeyCode.W) && controller == XboxController.First)
            v3VerticalAxis.z = 1.0f;
        else if (Input.GetKey(KeyCode.S) && controller == XboxController.First)
            v3VerticalAxis.z = -1.0f;
        else
            v3VerticalAxis.z = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);


        Vector3 v3HorizontalAxis = Vector3.zero;

        // keyboard and xbox right and left movement controls for the first player
        if (Input.GetKey(KeyCode.D) && controller == XboxController.First)
            v3HorizontalAxis.x = 1.0f;
        else if (Input.GetKey(KeyCode.A) && controller == XboxController.First)
            v3HorizontalAxis.x = -1.0f;
        else
            v3HorizontalAxis.x = XCI.GetAxisRaw(XboxAxis.LeftStickX, controller);

        // if movement is not locked
        if (!m_bMovementLock)
        {
            // if charging a snowball throw
            if (m_bCharging)
                // make the player move at a reduced speed
                m_fCurrentSpeed = m_fSlowSpeed;
            else
                // make the player move at the max speed
                m_fCurrentSpeed = m_fMaxSpeed;

            // if the player is not inputting a movement, don't move the player 
            m_v3MovePos = Vector3.zero;

            // Up and down movement
            m_v3MovePos += v3VerticalAxis * Time.deltaTime * m_fCurrentSpeed;

            // Left and right movement
            m_v3MovePos += v3HorizontalAxis * Time.deltaTime * m_fCurrentSpeed;

            // if the player is moving, normalize their movement and then times it by the 
            // max speed over delta time
            if (m_v3MovePos.magnitude > m_fMaxSpeed * Time.deltaTime)
            {
                m_v3MovePos.Normalize();
                m_v3MovePos *= m_fMaxSpeed * Time.deltaTime;
            }

            m_rb.AddForce(m_v3MovePos * 60, ForceMode.Impulse);
            //m_rb.MovePosition(m_rb.position + m_v3MovePos);
        }

        // if they are walking set play running animation
        m_Animator.SetFloat("running", m_v3MovePos.magnitude);
    }

    //------------------------------------------------------------------------------------------
    // The aiming function controls how player can be rotated to face enemies or anything 
    // they choose. You can aim with both sticks, if you are only using the left stick you 
    // aim and move with it, as soon as the right stick is being used that overrides the 
    // left stick and becomes an independent aiming stick.
    //------------------------------------------------------------------------------------------
    private void Aiming()
    {
        if (!m_bMovementLock)
        {
            // if the player is not touching the right stick = false, otherwise = true
            if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) != 0 || XCI.GetAxisRaw(XboxAxis.RightStickY, controller) != 0)
                bAimOverride = true;
            else
                bAimOverride = false;

            // when the player uses the right analog stick aswell
            if (!bAimOverride)
            {
                // Xbox Left Stick Aiming
                m_v3XboxDashDir = transform.forward;
                if (!m_bLeftTriggerPressed)
                {
                    // get the input of the left analog stick
                    m_axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, controller);
                    m_axisY = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);

                    //Debug.Log("Right Stick X: " + axisX + " Right Stick Y: " + axisY);

                    // if the player is not touching the left analog stick
                    if (m_axisX != 0.0f || m_axisY != 0.0f)
                    {
                        // turn the player to face the direction of the analog stick input
                        m_v3XboxDashDir = new Vector3(m_axisX, 0.0f, m_axisY);
                        transform.forward = m_v3XboxDashDir;
                    }
                }
            }
            else
            {
                // Xbox Right Stick Aiming
                m_v3XboxDashDir = transform.forward;
                if (!m_bLeftTriggerPressed)
                {
                    // get the input of the right analog stick
                    m_axisX = XCI.GetAxisRaw(XboxAxis.RightStickX, controller);
                    m_axisY = XCI.GetAxisRaw(XboxAxis.RightStickY, controller);

                    //Debug.Log("Right Stick X: " + axisX + " Right Stick Y: " + axisY);

                    // if the player is not touching the right analog stick
                    if (m_axisX != 0.0f || m_axisY != 0.0f)
                    {
                        // turn the player to face the direction of the analog stick input
                        m_v3XboxDashDir = new Vector3(m_axisX, 0.0f, m_axisY);
                        transform.forward = m_v3XboxDashDir;
                    }
                }
            }
        }

        // Mouse Aiming
        if (!XCI.IsPluggedIn(1) && !m_bLeftTriggerPressed)
        {
            // where the mouse is on the map projected form the screen, hits all collision
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            Vector3 v3Target = hit.point;
            // get the y position of the hit point
            v3Target.y = transform.position.y;
            // rotate the player to look towards the cursor
            transform.LookAt(v3Target);

            // (does not work) dash towards the cursor
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    m_v3DashDir = hit.point - transform.position;
            //    m_v3DashDir.y = 0.0f;
            //    m_v3DashDir.Normalize();
            //    m_v3MovePos.Normalize();
            //}
        }
    }

    //------------------------------------------------------------------------------------------
    // Shoot allows players to charge their snowballs and then throw them at other players
    //------------------------------------------------------------------------------------------
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

        // if the player is holding a snowball and is holding down the button to throw it
        if (rightTrigHeight < 1.0f && m_bHasBall)
        {
            // start throwing animation
            m_Animator.SetBool("throwing", true);
        }

        // if the player is holding a snowball but not holding down the throw button
        if (m_bHasBall && rightTrigHeight > 1.0f)
        {
            // snowball released
            m_bReleased = true;
            // stop throwing animation
            m_Animator.SetBool("throwing", false);
        }
        else
            m_bReleased = false;

        Dash scpDash = gameObject.GetComponent<Dash>();
        //ChargeSlider scpSlider = gameObject.GetComponent<ChargeSlider>();

        // if the player is intending to throw the ball and is not dashing and still has the ball,
        // or max charge has been reached, or the ball is thrown 
        if (m_bThrow && !scpDash.m_bDashing && m_bHasBall || bDuringMaxCharge || m_bThrowBall)
        {
            if (m_fChargeTimer < m_fMaxCharge && !m_bThrowBall)
            {
                m_fChargeTimer += Time.deltaTime;
                //scpSlider.m_sliChargeSlider.value = m_fChargeTimer;
                m_bCharging = true;
                // stop max charge animation
                m_Animator.SetBool("maxcharge", false);
            }
            // max charge reached and ball not thrown
            else if (m_fChargeTimer >= m_fMaxCharge && !m_bThrowBall)
            {
                m_fChargeTimer = m_fMaxCharge;
            }

            // when max power has been hit
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
                    // throw ball because player has been holding it for too long
                    m_bCharging = false;
                    // play the throw animation
                    m_Animator.SetBool("maxcharge", true);
                }
            }

            // if they have let go of the button that says throw the snowball and they have a snowball and they are not charing anymore
            if (m_bHasBall && !m_bCharging || m_bThrowBall)
            {
                m_fChargeModifier = m_fChargeTimer / m_fMaxCharge;
                m_fSnowballSpeed = m_fChargeModifier * m_fPowerRange + m_fPowerMin;
                //Debug.Log("throwing");

                // play throw audio if they ball has been thrown
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
                        Snowball scpSnowball = copy.GetComponent<Snowball>();

                        // change the color of the damage speed on the snowball to the players color
                        scpSnowball.m_materials[1] = m_matSnowball;

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

                        // run function that cleans up the charging when it is finshed
                        ResetChargeThrow();
                    }
                    // shoots out front
                    else
                    {
                        // get access to the snowball in the object pool
                        GameObject copy = ObjectPool.m_SharedInstance.GetPooledObject();
                        // change the snowballs material to the players material
                        Snowball scpSnowball = copy.GetComponent<Snowball>();

                        // change the color of the damage speed on the snowball to the players color
                        scpSnowball.m_materials[1] = m_matSnowball;

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

                        // run function that cleans up the charging when it is finshed
                        ResetChargeThrow();
                    }
                }
            }
        }
        else
            // not charging, don't remember the last charge power
            m_fChargeTimer = 0.0f;
    }
    //------------------------------------------------------------------------------------------
    // Resets the values used for charge throw, so that players can throw again and again
    //------------------------------------------------------------------------------------------
    private void ResetChargeThrow()
    {
        // resets everything required for the charge throw to work
        m_fChargeModifier = 0.0f;
        m_bThrow = false;
        m_fChargeTimer = 0.0f;
        m_bCharging = false;
        bDuringMaxCharge = false;
        m_fIsChargedTimer = 0.0f;
        m_bThrowBall = false;
    }

    //------------------------------------------------------------------------------------------
    // The health function keeps track of the health
    //------------------------------------------------------------------------------------------
    private void Health()
    {
        //SnowMan scpSnowMan = gameObject.GetComponent<SnowMan>();

        if (m_tookDmg)
        {
            m_DamageTimer -= Time.deltaTime;

            if (m_DamageTimer <= 0.3f)
            {
                // when a player is hit and damaged they turn red breifly
                m_smrCharacterMesh.material.color = new Color(1.0f, 0.0f, 0.0f, 255.0f);
            }
            if (m_DamageTimer < 0.0f)
            {
                // finished turning red for damage visual
                m_tookDmg = false;
                m_smrCharacterMesh.material.color = m_mainColor;
                m_DamageTimer = 0.3f;
            }
        }

        if (m_nCurrentHealth <= 0 && m_bAlive)
        {
            //scpSnowMan.m_bASnowManExists = false;

            if (m_bHasBall)
            {
                GameObject copy = ObjectPool.m_SharedInstance.GetPooledObject();
                copy.transform.position = transform.position + transform.forward;
                m_bHasBall = false;
            }

            //Destroy(gameObject.GetComponent<SnowMan>());

            m_bAlive = false;

            gameObject.AddComponent<PlayerAI>();

            //ScoreManager scpScoreManager = gameObject.GetComponent<ScoreManager>();

            ScoreManager.PlayerFinish(((int)controller) - 1);

            m_nCurrentHealth = 0;

            //kill the player, so the reticle can move without the player model and stuff appearing atop of it
            //mrCharacterMesh.enabled = false;
            //colPlayer.enabled = false;
            //mrWeapon.enabled = false;
            //mrPlayerCircle.enabled = false;
            //mrReticleCol.enabled = true;
            //rb.rotation = Quaternion.identity;
            //m_goPlayerReticleCopy.SetActive(true);

            //gameObject.transform.position = new Vector3(10.2f, 1.0f, -7.0f);
            
            //updating the health value onscreen
            //SetHealthText();

            //GameObject copy = Instantiate(scpSnowMan.SnowMan);
            //copy.transform.position = transform.position;
        }

        //if (!m_bAlive)
        //{
        //    // if the player is dead restrict movement speed
        //    m_fCurrentSpeed = m_fSpeed;
        //    //m_scpPlayerReticle.Movement();
        //}
    }

    //------------------------------------------------------------------------------------------
    // Updates the health value displayed onscreen (text)
    //------------------------------------------------------------------------------------------
    //void SetHealthText()
    //{
    //    if (m_txtHealth)
    //        m_txtHealth.text = "HP:" + m_nCurrentHealth.ToString();
    //}

    //------------------------------------------------------------------------------------------
    // TakeDamage deducts health points and plays the hurt audio
    //------------------------------------------------------------------------------------------
    private void TakeDamage()
    {
        m_tookDmg = true;
        // based on the snowballs damage value, deduct that much health points from the player
        m_nCurrentHealth = m_nCurrentHealth - Snowball.m_nScoreValue;
        // updating the health value onscreen
        //SetHealthText();
        AudioManager.m_SharedInstance.PlayHurtAudio();
    }

    //------------------------------------------------------------------------------------------
    // Allows players to dash and phase right through snowballs
    //------------------------------------------------------------------------------------------
    private void Projectiles()
    {
        Dash scpDash = gameObject.GetComponent<Dash>();
        Snowball scpSnowball = m_goSnowball.GetComponent<Snowball>();
        //Dash scpDash = gameObject.GetComponent<Dash>();

        // if you are dashing through a snowball that is stationary and have a ball
        if (!scpSnowball.m_bTooFast && m_bHasBall && !scpDash.m_bDashing)
        {
            // array of snowballs in the scene
            Snowball[] arrSnowballs = FindObjectsOfType<Snowball>();

            for (int i = 0; i < arrSnowballs.Length; i++)
            {
                // disable collider on snowball
                Physics.IgnoreCollision(arrSnowballs[i].gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
            }
        }
    }

    //------------------------------------------------------------------------------------------
    // This collision detection checks various things such as when players collide with
    // snowballs and players. If you dash into a player with a snowball you will steal it.
    // If you collide with a snowball you'll pick it up unless you have a snowball already.
    // If you throw a snowball and it hits you back, you wont take damage but Iframes will 
    // trigger.
    //
    //    Param:
    //          col: The object that the player collided with.
    //------------------------------------------------------------------------------------------
    private void OnCollisionEnter(Collision col)
    {
        // if the player collided with another player
        if (col.gameObject.tag == "Character")
        {
            // another player who this player touched, them
            Player scpColPlayer = col.gameObject.GetComponent<Player>();
            // another player who this player touched, their dashing component
            Dash scpColPlayerDash = col.gameObject.GetComponent<Dash>();

            // if the player was hit by another player who was dashing, get your snowball stolen
            if (scpColPlayerDash.m_bDashing)
            {
                //Debug.Log("Hit whilst dashing");
                if (m_bHasBall && !scpColPlayer.m_bHasBall)
                {
                    m_bHasBall = false;
                    // give the player who stole your snowball, your snowball
                    scpColPlayer.m_bHasBall = true;
                }
            }
        }

        // if this player collides with a snowball
        if (col.gameObject.tag == "Snowball")
        {
            Snowball scpSnowball = col.gameObject.GetComponent<Snowball>();
            Dash scpDash = gameObject.GetComponent<Dash>();

            // Ball is moving at damaging speed
            if (scpSnowball.m_bTooFast)
            {
                // damaged by snowball
                if (m_bHasBall && !m_bIFrame)
                {
                    // start Iframe
                    m_bIFrame = true;
                    
                    // if you hit yourself with the snowball you fired, worked out via material check between the snowball and the throwing player
                    if (scpSnowball.m_materials[1] == m_matSnowball)
                        Debug.Log("you hit your self you silly dufffer");
                    else
                        // if you are hit by any snowball that is moving at damaging speed and is not yours
                        TakeDamage();

                    //Drop ball
                    m_bHasBall = false;

                    GameObject copy = ObjectPool.m_SharedInstance.GetPooledObject();

                    // Snowball is thrown towards centre
                    Vector3 dir = m_goSnowBallTarget.transform.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    dir.y = 0.5f;
                    dir.Normalize();
                    // spawn the snowball above the players head
                    copy.transform.position = transform.position + (transform.up * 2.0f);
                    // throw the snowball towards the middle of the map
                    copy.GetComponent<Rigidbody>().AddForce(dir * m_fPowerOfTowardsCentre, ForceMode.Impulse);
                }
                else if (!m_bHasBall && scpDash.m_bDashing)
                {
                    // The player has picked it up
                    col.gameObject.SetActive(false);

                    m_bHasBall = true;
                }
                // hit by a snowball when you don't have one and is not during Iframe and you are not dashing
                else if (!scpDash.m_bDashing && !m_bHasBall && !m_bIFrame)
                {
                    // start Iframe
                    m_bIFrame = true;

                    // if you hit yourself with the snowball you fired, worked out via material check between the snowball and the throwing player
                    if (scpSnowball.m_materials[1] == m_matSnowball)
                        Debug.Log("you hit your self you silly dufffer");
                    else
                        // if you are hit by any snowball that is moving at damaging speed and is not yours
                        TakeDamage();
                }
            }
            // snowball is moving too slow too damage
            else
            {
                if (!m_bHasBall)
                {
                    // The player has picked it up
                    col.gameObject.SetActive(false);
                    m_bHasBall = true;
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