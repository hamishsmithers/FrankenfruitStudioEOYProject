using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class SnowMan : MonoBehaviour
{
    // Counts how many times it's been hit
    public int nSnowManHitCount = 2;
    public GameObject player;

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
    //    if (GameObject.Find("Characterp1"))
    //    {
    //        player = GameObject.Find("Characterp1");
    //        //Player scpSnowMan = GameObject.FindObjectOfType<Player>();
    //    }

    //    if (GameObject.Find("Characterp2"))
    //    {
    //        player = GameObject.Find("Characterp2");
    //        //Player scpSnowMan = GameObject.FindObjectOfType<Player>();
    //    }

        AbilitySnowMan scpAbilitySnowMan = player.GetComponent<AbilitySnowMan>();

        if (col.gameObject.tag == "Snowball")
        {
            nSnowManHitCount -= 1;
            if (nSnowManHitCount == 0)
            {
                scpAbilitySnowMan.bASnowManExists = false;
                Destroy(gameObject);
                nSnowManHitCount = 2;
            }
        }
    }
}
