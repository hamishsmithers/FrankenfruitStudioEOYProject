//-------------------------------------------------------------------------------
// Filename:        PlayerRetical.cs
//
// Description:     .
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Mitchell Cattini-Schultz
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class PlayerRetical : MonoBehaviour
{
    ////Rigidbody rb;
    //[HideInInspector]
    //public Vector3 m_v3MovePos;
    //[HideInInspector]
    //public GameObject m_player;

    //public GameObject TopLeft = null;
    //public GameObject TopRight = null;
    //public GameObject BotLeft = null;
    //public GameObject BotRight = null;

    //private GameObject m_TL;
    //private GameObject m_TR;
    //private GameObject m_BL;
    //private GameObject m_BR;


    // Use this for initialization
    void Awake()
    {
        //if (SceneManager.GetActiveScene().buildIndex != 2)
        //{
        //    //rb = GetComponent<Rigidbody>();
        //    m_TR = GameObject.Find("Reticle Bounds Top Right");
        //    m_TL = GameObject.Find("Reticle Bounds Top Left");
        //    m_BR = GameObject.Find("Reticle Bounds Bottom Right");
        //    m_BL = GameObject.Find("Reticle Bounds Bottom Left");
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    //--------------------------------------------------------
    // Movement
    //--------------------------------------------------------
    //public void Movement()
    //{
    //    if (SceneManager.GetActiveScene().buildIndex != 2)
    //    {
    //        Player scpPlayer = m_player.GetComponent<Player>();

    //        Vector3 v3VerticalAxis = Vector3.zero;

    //        if (Input.GetKey(KeyCode.W) && scpPlayer.controller == XboxController.First)
    //            v3VerticalAxis.z = 1.0f;
    //        else if (Input.GetKey(KeyCode.S) && scpPlayer.controller == XboxController.First)
    //            v3VerticalAxis.z = -1.0f;
    //        else
    //            v3VerticalAxis.z = XCI.GetAxisRaw(XboxAxis.LeftStickY, scpPlayer.controller);


    //        Vector3 v3HorizontalAxis = Vector3.zero;

    //        if (Input.GetKey(KeyCode.D) && scpPlayer.controller == XboxController.First)
    //            v3HorizontalAxis.x = 1.0f;
    //        else if (Input.GetKey(KeyCode.A) && scpPlayer.controller == XboxController.First)
    //            v3HorizontalAxis.x = -1.0f;
    //        else
    //            v3HorizontalAxis.x = XCI.GetAxisRaw(XboxAxis.LeftStickX, scpPlayer.controller);

    //        Vector3 v3Pos;
    //        v3Pos.x = transform.position.x;
    //        v3Pos.z = transform.position.z;

    //        if (!scpPlayer.m_bMovementLock)
    //        {
    //            // Up and down movement
    //            m_v3MovePos = Vector3.zero;

    //            m_v3MovePos += v3VerticalAxis * Time.deltaTime * scpPlayer.m_fCurrentSpeed;

    //            if (m_v3MovePos.magnitude > scpPlayer.m_fMaxSpeed * Time.deltaTime)
    //            {
    //                m_v3MovePos.Normalize();
    //                m_v3MovePos *= scpPlayer.m_fMaxSpeed * Time.deltaTime;
    //            }

    //            // Left and right movement
    //            m_v3MovePos += v3HorizontalAxis * Time.deltaTime * scpPlayer.m_fCurrentSpeed;

    //            if (m_v3MovePos.magnitude > scpPlayer.m_fMaxSpeed * Time.deltaTime)
    //            {
    //                m_v3MovePos.Normalize();
    //                m_v3MovePos *= scpPlayer.m_fMaxSpeed * Time.deltaTime;
    //            }

    //            transform.position += m_v3MovePos;
    //            //rb.MovePosition(rb.position + v3MovePos);

    //            //Constrain to camera
    //            //Vector3[] frustumCorners = new Vector3[4];

    //            //int layerMask = 1 << LayerMask.NameToLayer("Ground");
    //            //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
    //            //RaycastHit hit;
    //            //if (Physics.Raycast(ray, out hit, 9999.0f, layerMask))
    //            //{
    //            //Camera.main.CalculateFrustumCorners(new Rect(0, 0, 1, 1), hit.distance, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

    //            Vector3 newvec = transform.position;

    //            //Vector3 topRight = Camera.main.transform.TransformVector(frustumCorners[2]) + Camera.main.transform.position;
    //            //Vector3 bottomLeft = Camera.main.transform.TransformVector(frustumCorners[0]) + Camera.main.transform.position;

    //            // Bot Left
    //            //if (newvec.x < m_BL.transform.position.x)
    //            //    newvec.x = m_BL.transform.position.x;
                

    //            if (newvec.z < m_BL.transform.position.z)
    //                newvec.z = m_BL.transform.position.z;

    //            // Bot Right
    //            if (newvec.x > m_BR.transform.position.x)
    //                newvec.x = m_BR.transform.position.x;

    //            if (newvec.z < m_BR.transform.position.z)
    //                newvec.z = m_BR.transform.position.z;

    //            // Top Left
    //            if (newvec.x < m_TL.transform.position.x)
    //                newvec.x = m_TL.transform.position.x;

    //            if (newvec.z > m_TL.transform.position.z)
    //                newvec.z = m_TL.transform.position.z;

    //            // Top Right
    //            if (newvec.x > m_TR.transform.position.x)
    //                newvec.x = m_TR.transform.position.x;

    //            if (newvec.z > m_TR.transform.position.z)
    //                newvec.z = m_TR.transform.position.z;

    //            transform.position = newvec;

    //            //}

    //            //Debug.DrawRay(Camera.main.transform.position, bottomLeft, Color.blue);
    //            //Debug.DrawRay(Camera.main.transform.position, topRight, Color.red);
    //        }
    //    }
    //}
}
