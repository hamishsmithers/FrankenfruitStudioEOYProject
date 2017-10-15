using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSnowBall : MonoBehaviour
{
    public float fKnockbackForce = 10.0f;
    private bool bColliding = false;
    [HideInInspector]
    public bool bExists = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bColliding)
        {
            Knockback();
        }

        Debug.Log(bExists);
    }

    private void OnTriggerEnter(Collider other)
    {
        bColliding = true;
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        bColliding = false;
        Destroy(gameObject);
        bExists = false;
    }

    void Knockback()
    {
        Vector3 hit = transform.position; //ignore these numbers, get position from collision impact

        int playerLayer = 1 << LayerMask.NameToLayer("Player");
        Collider[] players = Physics.OverlapSphere(hit, 20.0f, playerLayer);

        for (int i = 0; i < players.Length; ++i)
        {
            Rigidbody rb = players[i].gameObject.GetComponent<Rigidbody>();
            rb.AddExplosionForce(fKnockbackForce, hit, 20.0f, 1.0f, ForceMode.Impulse);
        }
    }
}
