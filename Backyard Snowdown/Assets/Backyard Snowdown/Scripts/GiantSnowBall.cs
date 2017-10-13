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
        Knockback();
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

    void Knockback()
    {
        Vector3 hit = new Vector3(10, 0, 10); //ignore these numbers, get position from collision impact

        int playerLayer = 1 << LayerMask.NameToLayer("Player");
        Collider[] players = Physics.OverlapSphere(hit, 20.0f, playerLayer);
        for (int i = 0; i < players.Length; ++i)
        {
            Rigidbody rb = players[i].gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(1000, hit, 20.0f, 1.0f, ForceMode.Impulse);
        }
    }
}
