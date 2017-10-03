using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Player : MonoBehaviour
{
    //----------   
    // Movement
    //----------
    public XboxController controller;
    public float m_fSpeed = 5.0f;
    public float m_fMaxSpeed = 5.0f;
    public float m_fDashSpeed = 10.0f;
    public float fDashDuration = 0.5f;
    private float m_fCurrentSpeed = 5.0f;
    public GameObject m_PlayerModel = null;
    private float fDashCount = 0.0f;
    bool bSpacePressed = false;
    bool bMovementLock = false;
    bool bLeftTriggerPressed = false;
    Vector3 v3DashDir;
    Vector3 v3MovePos;
    Rigidbody rb;

    //-----------------------
    // Shooting / TennisBall
    //-----------------------
    public GameObject m_TennisBall = null;
    public int nTennisBallSpeed = 1000;
    // xbox max scale of trigger when pressed down
    private const float MAX_TRG_SCL = 1.21f;
    private bool bHasBall = false;

    //-----------------
    // Ability Snowball
    //-----------------
    public GameObject m_SnowBall = null;
    public float m_fSnowballSpeed = 1000.0f;
    public float m_fSlowSpeed = 2.5f;
    private bool m_bSlow = false;
    private float m_fSlowTimer = 0.0f;

    //-----------------
    // Ability Snowman
    //-----------------
    public GameObject m_SnowMan = null;

    //--------
    // Health
    //--------
    public Text txtHealth;
    public int nSpawnHealth = 20;
    public int nCurrentHealth;
    private bool bAlive = true;

    //-------------
    // Ball Pickup
    //-------------
    private bool bBallPickUp = false;
    private bool bRightTriggerPressed = false;

    //--------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------
    void Start()
    {
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
        float rightTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.RightTrigger, controller));
        bool bShoot = (Input.GetKeyDown(KeyCode.Mouse0) && controller == XboxController.First) || (rightTrigHeight < 1.0f);

        //----------
        // Movement
        //----------
        Vector3 v3VerticalAxis = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && controller == XboxController.First)
            v3VerticalAxis.z = 1.0f;
        else if (Input.GetKey(KeyCode.S) && controller == XboxController.First)
            v3VerticalAxis.z = -1.0f;
        else
            v3VerticalAxis.z = XCI.GetAxis(XboxAxis.LeftStickY, controller);


        Vector3 v3HorizontalAxis = Vector3.zero;

        if (Input.GetKey(KeyCode.D) && controller == XboxController.First)
            v3HorizontalAxis.x = 1.0f;
        else if (Input.GetKey(KeyCode.A) && controller == XboxController.First)
            v3HorizontalAxis.x = -1.0f;
        else
            v3HorizontalAxis.x = XCI.GetAxis(XboxAxis.LeftStickX, controller);

        Vector3 v3Pos;
        v3Pos.x = transform.position.x;
        v3Pos.z = transform.position.z;

        //xbox
        float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controller);
        float axisY = XCI.GetAxis(XboxAxis.LeftStickY, controller);

        //-------------------------
        // Xbox Right Stick Aiming
        //-------------------------
        axisX = XCI.GetAxis(XboxAxis.RightStickX, controller);
        axisY = XCI.GetAxis(XboxAxis.RightStickY, controller);
        //Debug.Log("Right Stick X: " + axisX + " Right Stick Y: " + axisY);

        Vector3 v3XboxDashDir = transform.forward;
        if (axisX != 0.0f || axisY != 0.0f)
        {
            v3XboxDashDir = new Vector3(axisX, 0.0f, axisY);
            transform.forward = v3XboxDashDir;
        }

        //------
        // Dash
        //------
        float leftTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.LeftTrigger, controller));

        if (leftTrigHeight < 1.0f || bLeftTriggerPressed || Input.GetKeyDown(KeyCode.Space) || bSpacePressed)
        {
            bLeftTriggerPressed = true;
            bSpacePressed = true;
            bMovementLock = true;

            if (fDashDuration > fDashCount)
            {
                Dash();
                fDashCount += Time.deltaTime;
                m_PlayerModel.GetComponent<Animator>().SetBool("dashing", true);
            }
            else
            {
                bLeftTriggerPressed = false;
                bSpacePressed = false;
                fDashCount = 0.0f;
                bMovementLock = false;
                m_PlayerModel.GetComponent<Animator>().SetBool("dashing", false);
            }
        }

        if (leftTrigHeight < 1.0f)
        {
            v3DashDir = v3XboxDashDir;
            v3DashDir.y = 0.0f;
            v3DashDir.Normalize();
            v3MovePos.Normalize();
        }

        //------------------
        // Ability Snowball
        //------------------
        if (XCI.GetButtonDown(XboxButton.LeftBumper, controller))
        {
            GameObject copy = Instantiate(m_SnowBall);
            copy.transform.position = transform.position + (transform.forward * 0.5f) + transform.up;
            Rigidbody rb = copy.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * m_fSnowballSpeed, ForceMode.Acceleration);
        }

        //----------
        // Movement
        //----------
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

        //-------------- 
        // Mouse Aiming
        //--------------
        if (!XCI.IsPluggedIn(1))
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

        //----------------
        // Mouse Shooting
        //----------------
        if (bShoot)
        {
            if (bBallPickUp)
            {
                GameObject copy = Instantiate(m_TennisBall);
                copy.transform.position = transform.position + transform.forward;
                Rigidbody rb = copy.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * nTennisBallSpeed, ForceMode.Acceleration);

                // The ball is thrown so it becomes false
                bBallPickUp = false;
                bHasBall = false;
            }
        }

        //------------------
        // Ability Snowball
        //------------------
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject copy = Instantiate(m_SnowBall);
            copy.transform.position = transform.position + transform.forward + transform.up;
            Rigidbody rb = copy.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * m_fSnowballSpeed, ForceMode.Acceleration);
        }

        //-----------------
        // Ability Snowman
        //-----------------
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameObject copy = Instantiate(m_SnowMan);
            copy.transform.position = transform.position + transform.forward;

        }
        //--------
        // Health
        //--------
        if (nCurrentHealth <= 0)
        {
            bAlive = false;
            nCurrentHealth = 0;
            {
                GameObject copy = Instantiate(m_SnowMan);
                copy.transform.position = transform.position + transform.forward;
                //Rigidbody rb = copy.GetComponent<Rigidbody>();
                //rb.AddForce(transform.forward * m_fSnowballSpeed, ForceMode.Acceleration);
            }

            ////--------
            //// Health
            ////--------
            //if (nCurrentHealth <= 0)
            //{
            //    bAlive = false;
            //    nCurrentHealth = 0;

            //    // updating the health value onscreen
            //    SetHealthText();
            //}

            //// if player is dead
            //if (!bAlive)
            //{
            //    Destroy(gameObject);
            //}
            
            //stop sliding
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (m_bSlow)
        {
            m_fSlowSpeed += Time.deltaTime;
            if (m_fSlowSpeed > 2.0f)
            {
                m_bSlow = false;
                m_fCurrentSpeed = m_fSpeed;
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
        if (col.gameObject.tag == "TennisBall")
        {
            nCurrentHealth = nCurrentHealth - TennisBall.nScoreValue;

            // updating the health value onscreen
            SetHealthText();

            TennisBall script = col.gameObject.GetComponent<TennisBall>();


            if (!script.bTooFast && !bHasBall)
            {
                // The player has picked it up
                bBallPickUp = true;
                Destroy(col.gameObject);
                bHasBall = true;
            }
        }
    }

    //--------------------------------------------------------
    // 
    //--------------------------------------------------------
    private void Dash()
    {
        // this is bad, need to somehow get the player's last direction as a vector
        //transform.Translate(Vector3.forward * m_fDashSpeed * Time.deltaTime);
        transform.position += v3DashDir * Time.deltaTime * m_fCurrentSpeed;
    }

    //--------------------------------------------------------
    // Updates the health value displayed onscreen
    //--------------------------------------------------------
    void SetHealthText()
    {
        txtHealth.text = "HP:" + nCurrentHealth.ToString();
    }

    // This is the function that slows the player when hit by SnowBall
    public void Slow()
    {
        //float fSlowTemp = 0.0f;
        //float fOldSpeed = 0.0f;
        //fOldSpeed = m_fSpeed;
        //fSlowTemp = (m_fSpeed * m_fSlowSpeed);
        //m_fSpeed = fSlowTemp;
        m_bSlow = true;
        m_fSlowTimer = 0.0f;
        m_fCurrentSpeed = m_fSlowSpeed;
        //Wait for 2 seconds here
        //m_fSpeed = fOldSpeed;
    }
}

