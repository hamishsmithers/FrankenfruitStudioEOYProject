using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Player : MonoBehaviour
{
    public XboxController controller;

    //----------   
    // Movement
    //----------
    public float m_fSpeed = 5.0f;
    public float m_fMaxSpeed = 5.0f;
    public float m_fDashSpeed = 10.0f;
    public float fDashDuration = 0.5f;
    [HideInInspector]
    public float m_fCurrentSpeed = 5.0f;
    public GameObject m_PlayerModel = null;
    bool bSpacePressed = false;
    [HideInInspector]
    public bool bMovementLock = false;
    [HideInInspector]
    public bool bLeftTriggerPressed = false;
    Vector3 v3DashDir;
    [HideInInspector]
    public Vector3 v3MovePos;
    Rigidbody rb;
    float axisX;
    float axisY;
    [HideInInspector]
    public Vector3 v3XboxDashDir;

    //-----------------------
    // Shooting / TennisBall
    //-----------------------
    public GameObject m_TennisBall = null;
    public int nTennisBallSpeed = 1750;
    // xbox max scale of trigger when pressed down
    private const float MAX_TRG_SCL = 1.21f;
    private bool bHasBall = false;
    private GameObject goTennisBall;
    [HideInInspector]
    public bool bCanShoot = true;
    private bool bWasHit = false;

    //--------
    // Health
    //--------
    public Text txtHealth;
    public int nSpawnHealth = 20;
    public int nCurrentHealth;
    [HideInInspector]
    public bool bAlive = true;
    public float fStun = 0.2f;
    private float fStunTimer = 0.0f;
    //private bool bHit = false;

    //-------------
    // Ball Pickup
    //-------------
    //private bool bBallPickUp = false;
    private bool bRightTriggerPressed = false;

    //--------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------
    void Start()
    {
        //Xbox Stick Axis'
        axisX = XCI.GetAxisRaw(XboxAxis.LeftStickX, controller);
        axisY = XCI.GetAxisRaw(XboxAxis.LeftStickY, controller);

        rb = GetComponent<Rigidbody>();
        m_fCurrentSpeed = m_fSpeed;

        //--------
        // Health
        //--------
        nCurrentHealth = nSpawnHealth;
        SetHealthText();
    }

    //--------------------------------------------------------
    // FixedUpdate is called once per frame
    //--------------------------------------------------------
    void FixedUpdate()
    {
        Dash scpDash = gameObject.GetComponent<Dash>();
        AbilitySnowMan scpSnowMan = gameObject.GetComponent<AbilitySnowMan>();

        Movement();

        Aiming();

        scpDash.DoDash();

        Shoot();

        scpSnowMan.CreateSnowMan();

        Health();

        EliminatedAbilityGiantSnowBall scpGiantSnowBall = gameObject.GetComponent<EliminatedAbilityGiantSnowBall>();

        scpGiantSnowBall.DoEliminatedAbilityGiantSnowBall();

        //stop sliding
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //--------------------------------------------------------
    //
    //--------------------------------------------------------
    void Update()
    {
        TennisBall scpTennisBall = m_TennisBall.GetComponent<TennisBall>();
        Dash scpDash = gameObject.GetComponent<Dash>();

        if (!scpTennisBall.bTooFast && bHasBall && !scpDash.bDashing)
        {
            /*if (GameObject.Find("Tennisball"))
            {
                goTennisBall = GameObject.Find("Tennisball");
                Physics.IgnoreCollision(goTennisBall.GetComponent<Collider>(), GetComponent<Collider>(), false);
            }

            if (GameObject.Find("Tennisball (1)"))
            {
                goTennisBall = GameObject.Find("Tennisball (1)");
                Physics.IgnoreCollision(goTennisBall.GetComponent<Collider>(), GetComponent<Collider>(), false);
            }

            if (GameObject.Find("Tennisball(Clone)"))
            {
                goTennisBall = GameObject.Find("Tennisball(Clone)");
                Physics.IgnoreCollision(goTennisBall.GetComponent<Collider>(), GetComponent<Collider>(), false);
            }*/

            TennisBall[] tennisBalls = FindObjectsOfType<TennisBall>();
            for(int i = 0; i < tennisBalls.Length; i++)
            {
                Physics.IgnoreCollision(tennisBalls[i].gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>(),false);
            }
        }

        if (fStunTimer > 0.0f)
        {
            fStunTimer -= Time.deltaTime;
            bMovementLock = true;
        }
        else
        {
            fStunTimer = 0.0f;
            bMovementLock = false;
        }
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

        if (!bMovementLock)
        {
            // Up and down movement
            v3MovePos = Vector3.zero;

            v3MovePos += v3VerticalAxis * Time.fixedDeltaTime * m_fCurrentSpeed;

            if (v3MovePos.magnitude > m_fMaxSpeed * Time.fixedDeltaTime)
            {
                v3MovePos.Normalize();
                v3MovePos *= m_fMaxSpeed * Time.fixedDeltaTime;
            }

            // Left and right movement
            v3MovePos += v3HorizontalAxis * Time.fixedDeltaTime * m_fCurrentSpeed;

            if (v3MovePos.magnitude > m_fMaxSpeed * Time.fixedDeltaTime)
            {
                v3MovePos.Normalize();
                v3MovePos *= m_fMaxSpeed * Time.fixedDeltaTime;
            }

            rb.MovePosition(rb.position + v3MovePos);
        }
    }

    //--------------------------------------------------------
    // Aiming
    //--------------------------------------------------------
    private void Aiming()
    {
        //-------------------------
        // Xbox Right Stick Aiming
        //-------------------------
        v3XboxDashDir = transform.forward;
        if (!bLeftTriggerPressed)
        {
            axisX = XCI.GetAxisRaw(XboxAxis.RightStickX, controller);
            axisY = XCI.GetAxisRaw(XboxAxis.RightStickY, controller);

            //Debug.Log("Right Stick X: " + axisX + " Right Stick Y: " + axisY);

            if (axisX != 0.0f || axisY != 0.0f)
            {
                v3XboxDashDir = new Vector3(axisX, 0.0f, axisY);
                transform.forward = v3XboxDashDir;
            }
        }

        //-------------- 
        // Mouse Aiming
        //--------------
        if (!XCI.IsPluggedIn(1) && !bLeftTriggerPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            Vector3 v3Target = hit.point;
            v3Target.y = transform.position.y;
            transform.LookAt(v3Target);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                v3DashDir = hit.point - transform.position;
                v3DashDir.y = 0.0f;
                v3DashDir.Normalize();
                v3MovePos.Normalize();
            }
        }
    }

    //--------------------------------------------------------
    // Shoot
    //--------------------------------------------------------
    private void Shoot()
    {
        if (bCanShoot)
        {
            float rightTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxisRaw(XboxAxis.RightTrigger, controller));
            bool bShoot = (Input.GetKeyDown(KeyCode.Mouse0) && controller == XboxController.First) || (rightTrigHeight < 1.0f);

            Dash scpDash = gameObject.GetComponent<Dash>();

            if (bShoot && !scpDash.bDashing)
            {
                if (bHasBall)
                {
                    RaycastHit hit;
                    if(Physics.Raycast(gameObject.transform.position,gameObject.transform.forward, out hit, 0.5f))
                    {
                        GameObject copy = Instantiate(m_TennisBall);
                        copy.transform.position = transform.position + transform.forward * -0.5f;
                        Rigidbody rb = copy.GetComponent<Rigidbody>();
                        rb.AddForce(transform.forward * nTennisBallSpeed * -1, ForceMode.Acceleration);
                        copy.transform.parent = GameObject.FindGameObjectWithTag("Projectiles").transform;
                        // The ball is thrown so it becomes false
                        bHasBall = false;
                        Debug.DrawLine(gameObject.transform.position, hit.point, Color.red);
                    }
                    else
                    {
                        GameObject copy = Instantiate(m_TennisBall);
                        copy.transform.position = transform.position + transform.forward * 0.5f;
                        Rigidbody rb = copy.GetComponent<Rigidbody>();
                        rb.AddForce(transform.forward * nTennisBallSpeed, ForceMode.Acceleration);
                        copy.transform.parent = GameObject.FindGameObjectWithTag("Projectiles").transform;
                        // The ball is thrown so it becomes false
                        bHasBall = false;
                    }
                }
            }
        }
    }

    //--------------------------------------------------------
    // Health
    //--------------------------------------------------------
    private void Health()
    {
        if (nCurrentHealth <= 0 && bAlive)
        {
            if (bHasBall)
            {
                GameObject copy = Instantiate(m_TennisBall);
                copy.transform.position = transform.position + transform.forward;
                bHasBall = false;
            }

            
            bAlive = false;

            //ScoreManager scpScoreManager = gameObject.GetComponent<ScoreManager>();

            ScoreManager.PlayerFinish(((int)controller) - 1);

            Destroy(gameObject);
            nCurrentHealth = 0;

            //updating the health value onscreen
            SetHealthText();

            //GameObject copy = Instantiate(scpSnowMan.m_SnowMan);
            //copy.transform.position = transform.position;
        }
    }

    //--------------------------------------------------------
    // Updates the health value displayed onscreen
    //--------------------------------------------------------
    void SetHealthText()
    {
        txtHealth.text = "HP:" + nCurrentHealth.ToString();
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
        if (col.gameObject.tag == "TennisBall")
        {
            TennisBall scpTennisBall = col.gameObject.GetComponent<TennisBall>();
            Dash scpDash = gameObject.GetComponent<Dash>();

            if (scpTennisBall.bTooFast)
            {
                if (bHasBall)
                {
                    //Drop ball
                    bHasBall = false;
                    bWasHit = true;
                    GameObject copy = Instantiate(m_TennisBall);
                    copy.transform.position = transform.position + transform.forward;
                }

                if (!scpDash.bDashing)
                {
                    nCurrentHealth = nCurrentHealth - TennisBall.nScoreValue;

                    // updating the health value onscreen
                    SetHealthText();

                    fStunTimer = fStun;
                }
            }
            else
            {
                if (scpDash.bDashing)
                {
                    Physics.IgnoreCollision(col.collider, GetComponent<Collider>(), true);
                }

                if (!bHasBall)
                {
                    // The player has picked it up
                    Destroy(col.gameObject);
                    bHasBall = true;
                }
            }
        }
    }
}




////-----------------
//// Ability Snowball
////-----------------
//public GameObject m_SnowBall = null;
//public float m_fSnowballSpeed = 1000.0f;
//public float m_fSlowSpeed = 2.5f;
//private bool m_bSlow = false;
//private float m_fSlowTimer = 0.0f;

////------------------
//// Ability Snowball
////------------------
//if (XCI.GetButtonDown(XboxButton.LeftBumper, controller))
//{
//    GameObject copy = Instantiate(m_SnowBall);
//    copy.transform.position = transform.position + (transform.forward * 0.5f) + transform.up;
//    Rigidbody rb = copy.GetComponent<Rigidbody>();
//    rb.AddForce(transform.forward * m_fSnowballSpeed, ForceMode.Acceleration);
//}

////------------------
//// Ability Snowball
////------------------
//if (Input.GetKeyDown(KeyCode.Mouse1))
//{
//    GameObject copy = Instantiate(m_SnowBall);
//    copy.transform.position = transform.position + transform.forward + transform.up;
//    Rigidbody rb = copy.GetComponent<Rigidbody>();
//    rb.AddForce(transform.forward * m_fSnowballSpeed, ForceMode.Acceleration);
//}

//// This is the function that slows the player when hit by SnowBall
//public void Slow()
//{
//    //float fSlowTemp = 0.0f;
//    //float fOldSpeed = 0.0f;
//    //fOldSpeed = m_fSpeed;
//    //fSlowTemp = (m_fSpeed * m_fSlowSpeed);
//    //m_fSpeed = fSlowTemp;
//    m_bSlow = true;
//    m_fSlowTimer = 0.0f;
//    m_fCurrentSpeed = m_fSlowSpeed;
//    //Wait for 2 seconds here
//    //m_fSpeed = fOldSpeed;
//}

//    if (m_bSlow)
//    {
//        m_fSlowSpeed += Time.deltaTime;
//        if (m_fSlowSpeed > 2.0f)
//        {
//            m_bSlow = false;
//            m_fCurrentSpeed = m_fSpeed;
//        }
//    }