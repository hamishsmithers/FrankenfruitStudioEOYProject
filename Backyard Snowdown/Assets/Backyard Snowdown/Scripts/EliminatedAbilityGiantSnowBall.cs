using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class EliminatedAbilityGiantSnowBall : MonoBehaviour
{
    public GameObject m_goGiantSnowBall = null;

    GameObject m_goCopy = null;
    GameObject m_goPlayerReticle;

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

    public void DoEliminatedAbilityGiantSnowBall()
    {
        if (m_goCopy.activeInHierarchy)
            return;

        Player scpPlayer = GetComponent<Player>();

        if (XCI.GetButtonDown(XboxButton.LeftBumper, scpPlayer.controller))
        {
            Player scpPlayerReticleGetter = gameObject.GetComponent<Player>();
            Vector3 spawn = scpPlayerReticleGetter.m_goPlayerReticleCopy.transform.position;
            spawn.y = 60.0f;
            m_goCopy.transform.position = spawn;

            m_goCopy.SetActive(true);
        }
    }
}
