using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class PlayerRetical : MonoBehaviour
{
    Rigidbody rb;
    [HideInInspector]
    public Vector3 v3MovePos;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //--------------------------------------------------------
    // Movement
    //--------------------------------------------------------
    public void Movement()
    {
        GameObject player = GameObject.Find("CharacterTeddyBear");
        Player scpPlayer = player.GetComponent<Player>();

        Vector3 v3VerticalAxis = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && scpPlayer.controller == XboxController.First)
            v3VerticalAxis.z = 1.0f;
        else if (Input.GetKey(KeyCode.S) && scpPlayer.controller == XboxController.First)
            v3VerticalAxis.z = -1.0f;
        else
            v3VerticalAxis.z = XCI.GetAxisRaw(XboxAxis.LeftStickY, scpPlayer.controller);


        Vector3 v3HorizontalAxis = Vector3.zero;

        if (Input.GetKey(KeyCode.D) && scpPlayer.controller == XboxController.First)
            v3HorizontalAxis.x = 1.0f;
        else if (Input.GetKey(KeyCode.A) && scpPlayer.controller == XboxController.First)
            v3HorizontalAxis.x = -1.0f;
        else
            v3HorizontalAxis.x = XCI.GetAxisRaw(XboxAxis.LeftStickX, scpPlayer.controller);

        Vector3 v3Pos;
        v3Pos.x = transform.position.x;
        v3Pos.z = transform.position.z;

        if (!scpPlayer.bMovementLock)
        {
            // Up and down movement
            v3MovePos = Vector3.zero;

            v3MovePos += v3VerticalAxis * Time.deltaTime * scpPlayer.m_fCurrentSpeed;

            if (v3MovePos.magnitude > scpPlayer.m_fMaxSpeed * Time.deltaTime)
            {
                v3MovePos.Normalize();
                v3MovePos *= scpPlayer.m_fMaxSpeed * Time.deltaTime;
            }

            // Left and right movement
            v3MovePos += v3HorizontalAxis * Time.deltaTime * scpPlayer.m_fCurrentSpeed;

            if (v3MovePos.magnitude > scpPlayer.m_fMaxSpeed * Time.deltaTime)
            {   
                v3MovePos.Normalize();
                v3MovePos *= scpPlayer.m_fMaxSpeed * Time.deltaTime;
            }

            rb.MovePosition(rb.position + v3MovePos);
        }
    }
}
