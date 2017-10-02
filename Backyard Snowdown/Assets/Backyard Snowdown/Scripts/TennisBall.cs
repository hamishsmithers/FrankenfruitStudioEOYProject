using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    Rigidbody rb;
    private float fVelCount = 0.0f;
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
        //Debug.Log(rb.velocity);

        //if (fVelCount > 1.0f || Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    fVelCount += Time.deltaTime;
        //    Debug.Log("ACTIVE");
        //}
        //else
        //{
        //    if (rb.velocity.x <= 0.5f && fVelCount != 0)
        //    {
        //        rb.velocity = Vector3.zero;
        //        fVelCount = 0.0f;
        //    }

        //    if (rb.velocity.y <= 0.5f && fVelCount != 0)
        //    {
        //        rb.velocity = Vector3.zero;
        //        fVelCount = 0.0f;
        //    }
        //}

        if (rb.velocity.x < 2.0f)
            nScoreValue = 0;

        if (rb.velocity.y < 2.0f)
            nScoreValue = 0;

        if (rb.velocity.x > 2.0f)
            nScoreValue = 2;

        if (rb.velocity.y > 2.0f)
            nScoreValue = 2;

        if (rb.velocity.x > -2.0f)
            nScoreValue = 2;

        if (rb.velocity.y > -2.0f)
            nScoreValue = 2;

        //if (rb.velocity.x < 2.0f && rb.velocity.x > 0.0f || rb.velocity.x > -2.0f && rb.velocity.x < 0.0f || rb.velocity.z < 2.0f && rb.velocity.z > 0.0f || rb.velocity.z > -2.0f && rb.velocity.z < 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //if (rb.velocity.x < 2.0f && rb.velocity.x > 0.0f && rb.velocity.z == 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //if (rb.velocity.z < 2.0f && rb.velocity.z > 0.0f && rb.velocity.x == 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //if (rb.velocity.x > -2.0f && rb.velocity.x < 0.0f && rb.velocity.z == 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //if (rb.velocity.z > -2.0f && rb.velocity.z < 0.0f && rb.velocity.x == 0.0f)
        //    nScoreValue = 2;
        //else if (rb.velocity == Vector3.zero)
        //    nScoreValue = 0;

        //Debug.Log(nScoreValue);
    }
}
