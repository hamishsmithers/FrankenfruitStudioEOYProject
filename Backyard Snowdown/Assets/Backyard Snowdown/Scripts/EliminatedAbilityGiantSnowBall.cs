using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class EliminatedAbilityGiantSnowBall : MonoBehaviour
{
    //---------------
    // Giant Snowball
    //---------------
    [LabelOverride("Giant Snowball")]
    [Tooltip("A GameObject to store the Giant Snowball.")]
    public GameObject m_goGiantSnowBall = null;

    //--------------
    // Snowball Copy
    //--------------
    private GameObject m_goCopy = null;
    //---------------
    // Player Reticle
    //---------------
    //private GameObject m_goPlayerReticle;

    //----------
    // Cooldown
    //----------
    //private float m_fCoolDownCount = 0.0f;
    //public float m_fCoolDownTime = 6.0f;
    //private bool bSummonable = false;

    // Use this for initialization
    void Start()
    {
        m_goCopy = Instantiate(m_goGiantSnowBall);
        m_goCopy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void DoEliminatedAbilityGiantSnowBall()
    //{
    //    if (m_goCopy.activeInHierarchy)
    //        return;

    //    Player scpPlayer = GetComponent<Player>();

    //    if (m_fCoolDownCount <= m_fCoolDownTime)
    //    {
    //        m_fCoolDownCount += Time.deltaTime;
    //    }
    //    else if (m_fCoolDownCount > m_fCoolDownTime)
    //    {
    //        bSummonable = true;
    //    }

    //    if (XCI.GetButtonDown(XboxButton.LeftBumper, scpPlayer.controller))
    //    {
    //        //if (bSummonable)
    //        //{
    //        //    Player scpPlayerReticleGetter = gameObject.GetComponent<Player>();
    //        //    Vector3 spawn = scpPlayerReticleGetter.m_goPlayerReticleCopy.transform.position;
    //        //    spawn.y = 60.0f;
    //        //    m_goCopy.transform.position = spawn;

    //        //    m_goCopy.SetActive(true);

    //        //    m_fCoolDownCount = 0.0f;
    //        //    bSummonable = false;
    //        //}
    //    }
    //}
}
