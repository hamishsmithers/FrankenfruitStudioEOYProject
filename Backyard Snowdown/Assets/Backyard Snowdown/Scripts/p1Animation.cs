using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p1Animation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ThrowBall()
    {
        Player scpPlayer = GetComponentInParent<Player>();
        scpPlayer.m_bThrowBall = true;
    }

    public void StartThrowBall()
    {
        //Player scpPlayer = GetComponentInParent<Player>();
        //scpPlayer.m_bThrowBall = true;
    }
}
