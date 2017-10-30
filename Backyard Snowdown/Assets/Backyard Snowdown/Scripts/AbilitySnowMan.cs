using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class AbilitySnowMan : MonoBehaviour
{
    //-----------------
    // Ability SnowMan
    //-----------------
    public GameObject m_SnowMan = null;
    [HideInInspector]
    public GameObject m_Copy;

    public float m_fSnowManBeforeSpawn = 0.3f;
    //private float m_fSnowManBeforeSpawnTimer = 0.0f;
    public float m_fSnowManAfterSpawn = 0.3f;
    //private float m_fSnowManAfterSpawnTimer = 0.0f;
    private bool m_bCreateSnowManBefore = false;
    private bool m_bCreateSnowManAfter = false;
    [HideInInspector]
    public bool m_bASnowManExists = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //--------------------------------------------------------
    // SnowMan, creates a snowman infront of the player
    //--------------------------------------------------------
    public void CreateSnowMan()
    {
        Player scpPlayer = gameObject.GetComponent<Player>();

        //Debug.Log(bASnowManExists);

        if (Input.GetKeyDown(KeyCode.LeftShift) || XCI.GetButtonDown(XboxButton.RightBumper, scpPlayer.controller) || XCI.GetButtonDown(XboxButton.LeftBumper, scpPlayer.controller) || m_bCreateSnowManBefore || m_bCreateSnowManAfter)
        {
            if (!m_bASnowManExists)
            {
                //if (fSnowManBeforeSpawnTimer <= fSnowManBeforeSpawn)
                //{
                //    fSnowManBeforeSpawnTimer += Time.deltaTime;
                //    bCreateSnowManBefore = true;
                //    scpPlayer.bMovementLock = true;
                //    scpPlayer.bCanShoot = false;
                //}
                //else if (fSnowManBeforeSpawnTimer >= fSnowManBeforeSpawn)
                //{
                    m_Copy = Instantiate(m_SnowMan);
                    m_Copy.GetComponent<SnowMan>().m_GoPlayer = gameObject;
                    m_Copy.transform.position = transform.position + transform.forward;
                    //bCreateSnowManBefore = false;
                    //fSnowManBeforeSpawnTimer = 0.0f;
                    m_bASnowManExists = true;
                //}

                //if (!bCreateSnowManBefore || bCreateSnowManAfter)
                //{
                //    fSnowManAfterSpawnTimer += Time.deltaTime;
                //    bCreateSnowManAfter = true;
                //}

                //if (fSnowManAfterSpawnTimer >= fSnowManAfterSpawn)
                //{
                //    fSnowManBeforeSpawnTimer = 0.0f;
                //    fSnowManAfterSpawnTimer = 0.0f;
                //    bCreateSnowManBefore = false;
                //    bCreateSnowManAfter = false;
                //    scpPlayer.bMovementLock = false;
                //    scpPlayer.bCanShoot = true;
                //    bASnowManExists = true;
                //}
            }
        }
    }
}
