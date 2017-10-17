﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class PlayerRetical : MonoBehaviour
{
    //Rigidbody rb;
    [HideInInspector]
    public Vector3 v3MovePos;
    [HideInInspector]
    public GameObject player;
    public GameObject TopLeft = null;
    public GameObject TopRight = null;
    public GameObject BotLeft = null;
    public GameObject BotRight = null;


    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
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

            transform.position += v3MovePos;
            //rb.MovePosition(rb.position + v3MovePos);

            //Constrain to camera
            //Vector3[] frustumCorners = new Vector3[4];

            //int layerMask = 1 << LayerMask.NameToLayer("Ground");
            //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, 9999.0f, layerMask))
            //{
            //Camera.main.CalculateFrustumCorners(new Rect(0, 0, 1, 1), hit.distance, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

            Vector3 newvec = transform.position;

            //Vector3 topRight = Camera.main.transform.TransformVector(frustumCorners[2]) + Camera.main.transform.position;
            //Vector3 bottomLeft = Camera.main.transform.TransformVector(frustumCorners[0]) + Camera.main.transform.position;

            // Bot Left
            if (newvec.x < BotLeft.transform.position.x)
                newvec.x = BotLeft.transform.position.x;

            if (newvec.z < BotLeft.transform.position.z)
                newvec.z = BotLeft.transform.position.z;

            // Bot Right
            if (newvec.x > BotRight.transform.position.x)
                newvec.x = BotRight.transform.position.x;

            if (newvec.z < BotRight.transform.position.z)
                newvec.z = BotRight.transform.position.z;

            // Top Left
            if (newvec.x < TopLeft.transform.position.x)
                newvec.x = TopLeft.transform.position.x;

            if (newvec.z > TopLeft.transform.position.z)
                newvec.z = TopLeft.transform.position.z;

            // Top Right
            if (newvec.x > TopRight.transform.position.x)
                newvec.x = TopRight.transform.position.x;

            if (newvec.z > TopRight.transform.position.z)
                newvec.z = TopRight.transform.position.z;

            transform.position = newvec;
            //transform.position = newvec;
            //}

            //Debug.DrawRay(Camera.main.transform.position, bottomLeft, Color.blue);
            //Debug.DrawRay(Camera.main.transform.position, topRight, Color.red);



        }
    }
}
