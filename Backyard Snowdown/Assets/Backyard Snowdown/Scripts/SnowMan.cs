using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class SnowMan : MonoBehaviour
{
    // Counts how many times it's been hit
    //public int nSnowManHitCount = 2;
    public Player player;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        //Player scpSnowMan = GameObject.FindObjectOfType<Player>();
        AbilitySnowMan scpAbilitySnowMan = player.gameObject.GetComponent<AbilitySnowMan>();
        if (col.gameObject.tag == "TennisBall")
        {
            scpAbilitySnowMan.bASnowManExists = false;
            Destroy(gameObject);
        }
    }
}
