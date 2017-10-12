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
    public GameObject copy;

    public float fSnowManBeforeSpawn = 0.3f;
    private float fSnowManBeforeSpawnTimer = 0.0f;
    public float fSnowManAfterSpawn = 0.3f;
    private float fSnowManAfterSpawnTimer = 0.0f;
    private bool bCreateSnowManBefore = false;
    private bool bCreateSnowManAfter = false;
    [HideInInspector]
    public bool bASnowManExists = false;

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

        if (Input.GetKeyDown(KeyCode.LeftShift) || XCI.GetButtonDown(XboxButton.RightBumper, scpPlayer.controller) || bCreateSnowManBefore || bCreateSnowManAfter)
        {
            if (!bASnowManExists)
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
                    copy = Instantiate(m_SnowMan);
                    copy.GetComponent<SnowMan>().player = gameObject;
                    copy.transform.position = transform.position + transform.forward;
                    //bCreateSnowManBefore = false;
                    //fSnowManBeforeSpawnTimer = 0.0f;
                    bASnowManExists = true;
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
