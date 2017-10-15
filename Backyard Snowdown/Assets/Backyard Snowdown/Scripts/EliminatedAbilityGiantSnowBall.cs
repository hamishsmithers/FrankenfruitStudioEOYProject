using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class EliminatedAbilityGiantSnowBall : MonoBehaviour
{
    GameObject copy;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoEliminatedAbilityGiantSnowBall()
    {
        Player scpPlayer = gameObject.GetComponent<Player>();
        GiantSnowBall scpGiantSnowBall = GetComponent<GiantSnowBall>();

        if (XCI.GetButtonDown(XboxButton.LeftBumper, scpPlayer.controller) && !scpGiantSnowBall.bExists)
        {
            Vector3 spawn = scpPlayer.transform.position;
            spawn.y = 60.0f;
            copy = Instantiate(scpPlayer.m_GiantSnowBall, spawn, Quaternion.identity);
            scpGiantSnowBall.bExists = true;
        }

        if (scpGiantSnowBall.bExists)
        {
            copy.transform.position -= copy.transform.up * 1;
        }
    }
}
