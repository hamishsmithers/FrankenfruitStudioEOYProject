using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class SnowMan : MonoBehaviour
{
    // Counts how many times it's been hit
    //------------------
    // Snowman Hit Count
    //------------------
    [LabelOverride("Snowman Hit Count")]
    [Tooltip("Stores how many times a snowman has been hit.")]
    public int m_nSnowManHitCount = 2;

    public bool m_bASnowManExists;



    //[HideInInspector]
    //public GameObject m_GoPlayer = null;
    //private int nCaseSwitch = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //switch (nCaseSwitch)
        //{
        //    case 1:
        //        player = GameObject.Find("Characterp1");
        //        nCaseSwitch = 2;
        //        break;
        //    case 2:
        //        player = GameObject.Find("Characterp2");
        //        nCaseSwitch = 3;
        //        break;
        //    case 3:
        //        player = GameObject.Find("Characterp3");
        //        nCaseSwitch = 4;
        //        break;
        //    case 4:
        //        player = GameObject.Find("Characterp4");
        //        nCaseSwitch = 1;
        //        break;
        //    default:
        //        Debug.Log("No Players Found");
        //        break;
        //}
    }

    private void OnCollisionEnter(Collision col)
    {
        //AbilitySnowMan scpAbilitySnowMan = gameObject.GetComponent<AbilitySnowMan>();

    //    if (col.gameObject.tag == "Snowball")
    //    {
    //        m_nSnowManHitCount -= 1;
    //        if (m_nSnowManHitCount == 0)
    //        {
    //            scpAbilitySnowMan.m_bASnowManExists = false;
    //            Destroy(gameObject);
    //            m_nSnowManHitCount = 2;
    //        }
    //    }
    }
}
