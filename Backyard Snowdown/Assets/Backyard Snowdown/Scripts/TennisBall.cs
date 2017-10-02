using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    Rigidbody rb;

    //-------
    // Score
    //-------
    public static int nScoreValue = 2;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity);

        if (rb.velocity.x < 2.0f || rb.velocity.x < -2.0f)
            nScoreValue = 0;
        else
            nScoreValue = 2;
        
        if (rb.velocity.z < 2.0f || rb.velocity.z < -2.0f)
            nScoreValue = 0;
        else
            nScoreValue = 2;

        Debug.Log(nScoreValue);
    }
}
