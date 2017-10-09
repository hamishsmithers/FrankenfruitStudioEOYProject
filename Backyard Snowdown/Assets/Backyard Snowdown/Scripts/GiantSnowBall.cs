using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSnowBall : MonoBehaviour
{
    public float fKnockbackForce = 10.0f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Rigidbody>().AddForce(Vector3.up * fKnockbackForce, ForceMode.Acceleration);
    }

    private void OnTriggerStay(Collider other)
    {
        //Vector3 v3Target = transform.position;
        //other.GetComponent<Rigidbody>().transform.position += transform.LookAt(v3Target) * fKnockbackForce * Time.deltaTime;
        other.GetComponent<Rigidbody>().AddForce(transform.forward * fKnockbackForce * 1, ForceMode.Acceleration);
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
