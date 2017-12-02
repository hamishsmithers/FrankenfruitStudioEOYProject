//--------------------------------------------------------------------------------
// Filename:        Dash.cs
//
// Description:     Dash allows the players to dash, which in turn allows players
//                  to pickup snowballs, steal them from other players and catch
//                  them mid-flight.
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Mitchell Cattini-Schultz
//--------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Dash : MonoBehaviour
{
    //--------------------------------------------------------------------------------------
    // XInput float that defines where the trigger is pressed down to. 1.21 is not pressed 
    // and 0 is fully pressed.
    //--------------------------------------------------------------------------------------
    private const float m_MaxTriggerHeight = 1.21f;

    //--------------------------------------------------------------------------------------
    // A float to store the speed that the player dashes at
    //--------------------------------------------------------------------------------------
    [LabelOverride("Dash Speed")]
    [Tooltip("The speed that the player dashes at in seconds.")]
    public float m_fDashSpeed = 2.0f;

    //--------------------------------------------------------------------------------------
    // A float to store the duration of the dash
    //--------------------------------------------------------------------------------------
    [LabelOverride("Dash Duration")]
    [Tooltip("A float to store the duration of the dash in seconds.")]
    public float m_fDashDuration = 0.5f;

    //--------------------------------------------------------------------------------------
    // A float to store the cooldown on the dash
    //--------------------------------------------------------------------------------------
    [LabelOverride("Dash Cooldown")]
    [Tooltip("A float to store the duration of the dash cooldown in seconds.")]
    public float m_fCoolDown = 0.8f;

    //--------------------------------------------------------------------------------------
    // A bool to check whether the player is dashing or not
    //--------------------------------------------------------------------------------------
    [HideInInspector]
    public bool m_bDashing = false;

    //--------------------------------------------------------------------------------------
    // A Vector3 to store the direction of the player while they dash
    //--------------------------------------------------------------------------------------
    [HideInInspector]
    public Vector3 m_v3DashDir;

    //--------------------------------------------------------------------------------------
    // A float to store the timer of the dash
    //--------------------------------------------------------------------------------------
    [HideInInspector]
    public float m_fDashTimer = 0.0f;

    private float m_fCoolDownTimer = 0.0f;

    [HideInInspector]
    public bool m_bCoolDown = false;

    private bool m_bStartTimer = false;

    //--------------------------------------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------------------------------------
    void Start()
    {

    }

    //--------------------------------------------------------------------------------------
    // Update is called once per frame
    //-------------------------------------------------------------------------------------- 
    public void Update()
    {

    }
    //--------------------------------------------------------------------------------------
    // Makes the player dash forward a certain distance allowing for them to pickup, steal 
    // and catch snowballs.
    //--------------------------------------------------------------------------------------
    public void DoDash()
    {
        // cooldown timer
        if (m_bStartTimer && m_fCoolDownTimer <= m_fCoolDown)
        {
            m_fCoolDownTimer += Time.deltaTime;
            m_bCoolDown = true;
            m_bDashing = false;
        }
        else
        {
            m_fCoolDownTimer = 0.0f;
            m_bStartTimer = false;
            m_bCoolDown = false;
        }

        if (!m_bCoolDown)
        {
            //  we get the player script because we use it for various things here
            Player scpPlayer = gameObject.GetComponent<Player>();
            float leftTrigHeight = m_MaxTriggerHeight * (1.0f - XCI.GetAxisRaw(XboxAxis.LeftTrigger, scpPlayer.controller));

            // if the left trigger is pressed down
            if (leftTrigHeight < 1.0f || scpPlayer.m_bLeftTriggerPressed || Input.GetKeyDown(KeyCode.Space))
            {                
                scpPlayer.m_bLeftTriggerPressed = true;
                // players cant move when they dash
                scpPlayer.m_bMovementLock = true;

                // how long a dash can last
                if (m_fDashDuration > m_fDashTimer)
                {
                    if (!scpPlayer.m_bHitByGiantSnowBall)
                    {
                        // plays dash audio
                        if (!m_bDashing)
                            AudioManager.m_SharedInstance.PlayDashAudio();

                        m_bDashing = true;
                        // push the player in the aim stick direction to dash
                        scpPlayer.m_rb.AddForce(m_v3DashDir * m_fDashSpeed * scpPlayer.m_fCurrentSpeed, ForceMode.Impulse);
                        m_fDashTimer += Time.deltaTime;
                        // play dash animaiton
                        scpPlayer.m_goPlayerModel.GetComponent<Animator>().SetBool("dashing", true);
                    }
                }
                else
                {
                    m_bStartTimer = true;
                    scpPlayer.m_bLeftTriggerPressed = false;
                    m_fDashTimer = 0.0f;
                    // restore movement ot the player
                    scpPlayer.m_bMovementLock = false;
                    // finish dash animation
                    scpPlayer.m_goPlayerModel.GetComponent<Animator>().SetBool("dashing", false);
                }
            }

            if (leftTrigHeight < 1.0f)
            {
                // the dash direction is set here from the players dash direction vector3
                m_v3DashDir = scpPlayer.m_v3XboxDashDir;
                // make sure they don't dash into the sky
                m_v3DashDir.y = 0.0f;
                // normalize so we can add force and not have it go crazy (too powerful)
                m_v3DashDir.Normalize();
                scpPlayer.m_v3MovePos.Normalize();
            }
        }
    }
}
