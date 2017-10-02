using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    Rigidbody rb;
    public float fDamageSpeed = 3.0f;
    public static bool bTooFast = false;
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

        if (rb.velocity.x + rb.velocity.z > fDamageSpeed)
        {
            nScoreValue = 2;
            bTooFast = true;
        }

        else if (rb.velocity.x + rb.velocity.z < fDamageSpeed)
        {
            nScoreValue = 0;
            bTooFast = false;
        }

        if (rb.velocity.x + rb.velocity.z > fDamageSpeed)
        {
            nScoreValue = 2;
            bTooFast = true;
        }

        else if (rb.velocity.x + rb.velocity.z < fDamageSpeed)
        {
            nScoreValue = 0;
            bTooFast = false;
        }

        // now i need to do the same for the negitive axis but then conflicts happen.

        if (rb.velocity.x + rb.velocity.z < -fDamageSpeed)
        {
            nScoreValue = 2;
            bTooFast = true;
        }

        if (rb.velocity.x + rb.velocity.z < -fDamageSpeed)
        {
            nScoreValue = 2;
            bTooFast = true;
        }


        Debug.Log(nScoreValue);

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

        //stop sliding
        rb.angularVelocity = Vector3.zero;
    }
}
