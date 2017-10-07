using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminatedAbilityGiantSnowBall : MonoBehaviour
{

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

        if (!scpPlayer.bAlive)
        {

        }
    }
}
