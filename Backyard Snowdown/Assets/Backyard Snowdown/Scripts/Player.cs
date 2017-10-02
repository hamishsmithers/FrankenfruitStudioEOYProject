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
    public GameObject m_PlayerModel = null;
    private float fDashCount = 0.0f;
    bool bSpacePressed = false;
    bool bKeyboardMovementLock = false;
    bool bXboxMovementLock = false;
    bool bLeftTriggerPressed = false;
    Vector3 v3DashDir;
    Vector3 v3MovePos;
    Rigidbody rb;

    //----------
    // Shooting
    //----------
    public GameObject m_TennisBall = null;
    public int nTennisBallSpeed = 1000;
    // xbox max scale of trigger when pressed down
    private const float MAX_TRG_SCL = 1.21f;

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
        //// Setting up multiple xbox controller input
        //switch (controller)
        //{
        //    case XboxController.First: gameObject.name = "Character1"; break;
        //        //case XboxController.Second: GetComponent<Renderer>().name = "Characterp2"; break;
        //        //case XboxController.Third: GetComponent<Renderer>().name = "Characterp3"; break;
        //        //case XboxController.Fourth: GetComponent<Renderer>().name = "Characterp4"; break;
        //}

        rb = GetComponent<Rigidbody>();

        //--------
        // Health
        //--------
        nCurrentHealth = nSpawnHealth;
        SetHealthText();

    }

    //--------------------------------------------------------
    // Update is called once per frame
    //--------------------------------------------------------
    void FixedUpdate()
    {
        float rightTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.RightTrigger, controller));
        bool bShoot = (Input.GetKeyDown(KeyCode.Mouse0) && controller == XboxController.First) || (rightTrigHeight < 1.0f);

        Vector3 v3LeftAxis = Vector3.zero;
        if (Input.GetKey(KeyCode.D) && controller == XboxController.First)
            v3LeftAxis.x = 1.0f;
        else if(Input.GetKey(KeyCode.A) && controller == XboxController.First)
            v3LeftAxis.x = -1.0f;
        else
            v3LeftAxis.x = XCI.GetAxis(XboxAxis.LeftStickX, controller);

        //----------
        // Movement
        //----------
        Vector3 v3Pos;
        v3Pos.x = transform.position.x;

        if (Global.bXboxControls)
        {
            //------------------------
            // Xbox Movement Controls
            //
            // Left Stick Movement
            //------------------------ 
            v3Pos = transform.position;
            float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controller);
            float axisY = XCI.GetAxis(XboxAxis.LeftStickY, controller);
            //Debug.Log("Left Stick X: " + axisX + " Left Stick Y: " + axisY);

            if (!bXboxMovementLock)
            {
                // Cap Movement
                if (m_fSpeed > m_fMaxSpeed)
                {
                    m_fSpeed = m_fMaxSpeed;
                }

                float newPosX = v3Pos.x + (axisX * m_fSpeed * Time.deltaTime);
                float newPosZ = v3Pos.z + (axisY * m_fSpeed * Time.deltaTime);
                v3Pos = new Vector3(newPosX, transform.position.y, newPosZ);
                transform.position = v3Pos;
            }

            axisX = XCI.GetAxis(XboxAxis.RightStickX, controller);
            axisY = XCI.GetAxis(XboxAxis.RightStickY, controller);
            //Debug.Log("Right Stick X: " + axisX + " Right Stick Y: " + axisY);

            //-------------------------
            // Xbox Right Stick Aiming
            //-------------------------
            Vector3 dir = new Vector3(axisX, 0.0f, axisY);
            transform.forward = dir;

            //// trying to store the last direction facing and apply that when no left stick input is read.
            //{
            //    Vector3 dirStore = new Vector3();
            //    dirStore = dir;

            //    if (axisX == 0.0f && axisY == 0.0f)
            //    {
            //        transform.forward = dirStore;
            //    }
            //}

            //------
            // Dash
            //------
            float leftTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.LeftTrigger, controller));

            if (leftTrigHeight < 1.0f || bLeftTriggerPressed)
            {
                bLeftTriggerPressed = true;
                bXboxMovementLock = true;

                if (fDashDuration > fDashCount)
                {
                    Dash();
                    fDashCount += Time.deltaTime;
                    m_PlayerModel.GetComponent<Animator>().SetBool("dashing", true);
                }
                else
                {
                    bLeftTriggerPressed = false;
                    fDashCount = 0.0f;
                    bXboxMovementLock = false;
                    m_PlayerModel.GetComponent<Animator>().SetBool("dashing", false);
                }
            }

            //---------------
            // Xbox Shooting
            //---------------
            //float rightTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.RightTrigger, controller));

            //if (rightTrigHeight < 1.0f && bRightTriggerPressed)
            //{
            //    //Debug.Log("Right Trigger Pressed");

            //    if (bBallPickUp)
            //    {
            //        GameObject copy = Instantiate(m_TennisBall);
            //        copy.transform.position = transform.position + transform.forward;
                    
            //        rb.AddForce(transform.forward * nTennisBallSpeed, ForceMode.Acceleration);

            //        // The ball is thrown so it becomes false
            //        bBallPickUp = false;
            //    }

            //    bRightTriggerPressed = false;
            //}

            //if (rightTrigHeight > 1.0f)
            //{
            //    bRightTriggerPressed = true;
            //}
        }

        //----------------------------
        // Keyboard Movement Controls
        //----------------------------
        if (Global.bKeyboardControls)
        {
            if (!bKeyboardMovementLock)
            {
                v3MovePos = Vector3.zero;

                //if (Input.GetKey(KeyCode.W))
                //{
                //    v3MovePos += Vector3.forward * Time.fixedDeltaTime * m_fSpeed;
                //}

                //if (Input.GetKey(KeyCode.S))
                //{
                //    v3MovePos += Vector3.forward * Time.fixedDeltaTime * -m_fSpeed;
                //}

                v3MovePos += v3LeftAxis * Time.fixedDeltaTime * m_fSpeed;

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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            Vector3 v3Target = hit.point;
            v3Target.y = transform.position.y;
            transform.LookAt(v3Target);

            Debug.Log(v3Target);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                v3DashDir = hit.point - transform.position;
                v3DashDir.y = 0.0f;
                v3DashDir.Normalize();
                v3MovePos.Normalize();
            }

            //Debug.Log(v3DashDir);

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
                }
            }

            //------
            // Dash
            //------
            if (Input.GetKeyDown(KeyCode.Space) || bSpacePressed)
            {
                bSpacePressed = true;
                bKeyboardMovementLock = true;

                if (fDashDuration > fDashCount)
                {
                    Dash();
                    fDashCount += Time.deltaTime;
                    m_PlayerModel.GetComponent<Animator>().SetBool("dashing", true);
                }
                else
                {
                    bSpacePressed = false;
                    fDashCount = 0.0f;
                    bKeyboardMovementLock = false;
                    m_PlayerModel.GetComponent<Animator>().SetBool("dashing", false);
                }
            }
        }

        //--------
        // Health
        //--------
        if (nCurrentHealth <= 0)
        {
            bAlive = false;
            nCurrentHealth = 0;

            // updating the health value onscreen
            SetHealthText();
        }

        // if player is dead
        if (!bAlive)
        {
            Destroy(gameObject);
        }

        //stop sliding
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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

            // The player has picked it up
            bBallPickUp = true;
            
            Destroy(col.gameObject);
        }
    }

    //--------------------------------------------------------
    // 
    //--------------------------------------------------------
    private void Dash()
    {
        // this is bad, need to somehow get the player's last direction as a vector
        //transform.Translate(Vector3.forward * m_fDashSpeed * Time.deltaTime);
        transform.position += v3DashDir * Time.deltaTime * m_fSpeed;
    }

    //--------------------------------------------------------
    // Updates the health value displayed onscreen
    //--------------------------------------------------------
    void SetHealthText()
    {
        txtHealth.text = "HP:" + nCurrentHealth.ToString();
    }
}
