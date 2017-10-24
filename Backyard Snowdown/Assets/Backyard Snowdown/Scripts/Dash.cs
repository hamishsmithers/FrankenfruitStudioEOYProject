using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Dash : MonoBehaviour
{
    private const float m_MaxTriggerHeight = 1.21f;

    public float m_fDashSpeed = 2.0f;
    public float m_fDashDuration = 0.5f;
    private float m_fDashTimer = 0.0f;
    public float m_fCoolDown = 0.8f;
    private float m_fCoolDownTimer = 0.0f;
    private bool m_bCoolDown = false;
    [HideInInspector]
    public bool m_bDashing = false;
    [HideInInspector]
    public Vector3 m_v3DashDir;
    private bool m_bStartTimer = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void DoDash()
    {
        if(m_bStartTimer && m_fCoolDownTimer <= m_fCoolDown)
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
            Player scpPlayer = gameObject.GetComponent<Player>();

            float leftTrigHeight = m_MaxTriggerHeight * (1.0f - XCI.GetAxisRaw(XboxAxis.LeftTrigger, scpPlayer.controller));

            if (leftTrigHeight < 1.0f || scpPlayer.m_bLeftTriggerPressed || Input.GetKeyDown(KeyCode.Space))
            {
                scpPlayer.m_bLeftTriggerPressed = true;
                scpPlayer.m_bMovementLock = true;

                if (m_fDashDuration > m_fDashTimer)
                {
                    m_bDashing = true;
                    if (m_bDashing)
                    {
                        transform.position += m_v3DashDir * m_fDashSpeed * Time.deltaTime * scpPlayer.m_fCurrentSpeed;
                        m_fDashTimer += Time.deltaTime;
                        scpPlayer.m_goPlayerModel.GetComponent<Animator>().SetBool("dashing", true);
                    }
                }
                else
                {
                    m_bStartTimer = true;
                    scpPlayer.m_bLeftTriggerPressed = false;
                    m_fDashTimer = 0.0f;
                    scpPlayer.m_bMovementLock = false;
                    scpPlayer.m_goPlayerModel.GetComponent<Animator>().SetBool("dashing", false);
                }
            }

            if (leftTrigHeight < 1.0f)
            {
                m_v3DashDir = scpPlayer.m_v3XboxDashDir;
                m_v3DashDir.y = 0.0f;
                m_v3DashDir.Normalize();
                scpPlayer.m_v3MovePos.Normalize();
            }
        }
    }
}
