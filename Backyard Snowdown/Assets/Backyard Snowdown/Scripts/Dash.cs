using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class Dash : MonoBehaviour
{
    private const float MAX_TRG_SCL = 1.21f;

    public float m_fDashSpeed = 2.0f;
    public float fDashDuration = 0.5f;
    private float fDashTimer = 0.0f;
    public float fCoolDown = 0.8f;
    private float fCoolDownTimer = 0.0f;
    private bool bCoolDown = false;
    [HideInInspector]
    public bool bDashing = false;
    [HideInInspector]
    public Vector3 v3DashDir;
    private bool bStartTimer = false;


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
        if(bStartTimer && fCoolDownTimer <= fCoolDown)
        {
            fCoolDownTimer += Time.deltaTime;
            bCoolDown = true;
            bDashing = false;
        }
        else
        {
            fCoolDownTimer = 0.0f;
            bStartTimer = false;
            bCoolDown = false;
        }

        if (!bCoolDown)
        {
            Player scpPlayer = gameObject.GetComponent<Player>();

            float leftTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxisRaw(XboxAxis.LeftTrigger, scpPlayer.controller));

            if (leftTrigHeight < 1.0f || scpPlayer.bLeftTriggerPressed || Input.GetKeyDown(KeyCode.Space))
            {
                scpPlayer.bLeftTriggerPressed = true;
                scpPlayer.bMovementLock = true;

                if (fDashDuration > fDashTimer)
                {
                    bDashing = true;
                    if (bDashing)
                    {
                        transform.position += v3DashDir * m_fDashSpeed * Time.deltaTime * scpPlayer.m_fCurrentSpeed;
                        fDashTimer += Time.deltaTime;
                        scpPlayer.m_PlayerModel.GetComponent<Animator>().SetBool("dashing", true);
                    }
                }
                else
                {
                    bStartTimer = true;
                    scpPlayer.bLeftTriggerPressed = false;
                    fDashTimer = 0.0f;
                    scpPlayer.bMovementLock = false;
                    scpPlayer.m_PlayerModel.GetComponent<Animator>().SetBool("dashing", false);
                }
            }

            if (leftTrigHeight < 1.0f)
            {
                v3DashDir = scpPlayer.v3XboxDashDir;
                v3DashDir.y = 0.0f;
                v3DashDir.Normalize();
                scpPlayer.v3MovePos.Normalize();
            }
        }
    }
}
