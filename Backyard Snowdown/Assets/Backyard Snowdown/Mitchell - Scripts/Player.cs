using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Player : MonoBehaviour {

    //----------   
    // Movement
    //----------
    public float m_fSpeed = 10;
    public XboxController controller;

    //----------   
    // Rotation
    //----------
    public Vector3 DirVector { get; set; }
    
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

    //--------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------
    void Start ()
    {
        // Setting up multiple xbox controller input
        switch (controller)
        {
            case XboxController.First: GetComponent<Renderer>().name = "Characterp1"; break;
            //case XboxController.Second: GetComponent<Renderer>().name = "Characterp2"; break;
            //case XboxController.Third: GetComponent<Renderer>().name = "Characterp3"; break;
            //case XboxController.Fourth: GetComponent<Renderer>().name = "Characterp4"; break;
        }

        //--------
        // Health
        //--------
        nCurrentHealth = nSpawnHealth;
        SetHealthText();
    }

    //--------------------------------------------------------
    // Update is called once per frame
    //--------------------------------------------------------
    void Update ()
    {
        //----------
        // Movement
        //----------
        Vector3 v3Pos;
        v3Pos.x = transform.position.x;

        //---------------
        // Mouse raycast
        //---------------
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hit;
        //Physics.Raycast(ray, out hit);

        //Vector3 target = hit.point;
        //target.y = transform.position.y;
        //transform.LookAt(target);

        //------------------------
        // Xbox Movement Controls
        //
        // Left stick movement
        //------------------------ 
        v3Pos = transform.position;
        float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controller);
        float axisY = XCI.GetAxis(XboxAxis.LeftStickY, controller);
        Debug.Log("Left Stick X: " + axisX + " Left Stick Y: " + axisY);
        float newPosX = v3Pos.x + (axisX * m_fSpeed * Time.deltaTime);
        float newPosZ = v3Pos.z + (axisY * m_fSpeed * Time.deltaTime);
        v3Pos = new Vector3(newPosX, transform.position.y, newPosZ);
        transform.position = v3Pos;
        
        axisX = XCI.GetAxis(XboxAxis.RightStickX, controller);
        axisY = XCI.GetAxis(XboxAxis.RightStickY, controller);
        //Debug.Log("Right Stick X: " + axisX + " Right Stick Y: " + axisY);

        // attempt at rotation via right analog stick 
        // transform.eulerAngles = new Vector3(axisX * 25, 0, axisY * 25);
        Vector3 Direction;
        Direction = new Vector3(0, 0, 0);
        Direction += new Vector3(axisX, 0, axisY);

        transform.rotation = Quaternion.LookRotation(DirVector);
        
        //----------------------------
        // Keyboard Movement Controls
        //----------------------------
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * m_fSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * m_fSpeed * Time.deltaTime);
        }

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Rotate(0, Time.deltaTime * 200.0f, 0);
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Rotate(0, Time.deltaTime * -200.0f, 0);
        //}

        //----------
        // Shooting
        //----------
        float rightTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.RightTrigger, controller));

        if (rightTrigHeight < 1.0f)
        {
            //Debug.Log("Right Trigger Pressed");
            GameObject copy = Instantiate(m_TennisBall);
            copy.transform.position = transform.position + transform.forward;
            Rigidbody rb = copy.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * nTennisBallSpeed, ForceMode.Acceleration);
        }

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    GameObject copy = Instantiate(m_TennisBall);
        //    copy.transform.position = transform.position + transform.forward;
        //    Rigidbody rb = copy.GetComponent<Rigidbody>();
        //    rb.AddForce(transform.forward * nTennisBallSpeed, ForceMode.Acceleration);
        //}

        //--------
        // Health
        //--------
        if(nCurrentHealth <=0)
        {
            bAlive = false;
            nCurrentHealth = 0;
        }

        // if player is dead
        if(!bAlive)
        {
            Destroy(gameObject);
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
        }
    }

    //--------------------------------------------------------
    // Updates the health value displayed onscreen
    //--------------------------------------------------------
    void SetHealthText()
    {
        txtHealth.text = "HP:" + nCurrentHealth.ToString();
    }
}
