﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySnowBall : MonoBehaviour
{
    //------------------------------------------------------------------------------------------
    // Slow duration
    //------------------------------------------------------------------------------------------
    //[LabelOverride("Slow Duration")]
    //[Tooltip("A float representing the duration of the slow in seconds.")]
    //public float m_fSlowDuration = 1.0f;

    //------------------------------------------------------------------------------------------
    // Slow count
    //------------------------------------------------------------------------------------------
    //private float m_fSlowCount = 0.0f;

    //------------------------------------------------------------------------------------------
    // Use this for initialization, called when the script is first accessed.
    //------------------------------------------------------------------------------------------
    void Start()
    {

    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame.
    //------------------------------------------------------------------------------------------
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        //    if (col.gameObject.tag == "Character")
        //    {
        //        // create a slow function in player and do p.slow
        //        if (m_fSlowDuration > m_fSlowCount)
        //        {
        //            //p.Slow();
        //            m_fSlowCount += Time.deltaTime;
        //        }
        //        else
        //        {
        //            m_fSlowCount = 0.0f;
        //        }
        //    }

        //    Destroy(gameObject);
    }
}
