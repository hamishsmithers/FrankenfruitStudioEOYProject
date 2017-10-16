using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class EliminatedAbilityGiantSnowBall : MonoBehaviour
{
    public GameObject m_GiantSnowBall = null;

    GameObject copy = null;
    GiantSnowBall scpGiantSnowBall;
    GameObject goPlayerReticle;

    // Use this for initialization
    void Start()
    {
        copy = Instantiate(m_GiantSnowBall);
        copy.SetActive(false);
        scpGiantSnowBall = copy.GetComponent<GiantSnowBall>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoEliminatedAbilityGiantSnowBall()
    {
        if (copy.activeInHierarchy)
            return;

        Player scpPlayer = GetComponent<Player>();

        if (XCI.GetButtonDown(XboxButton.LeftBumper, scpPlayer.controller))
        {
            goPlayerReticle = GameObject.Find("Reticle");
            Vector3 spawn = goPlayerReticle.transform.position;
            spawn.y = 60.0f;
            copy.transform.position = spawn;

            copy.SetActive(true);
        }
    }
}
