using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySnowMan : MonoBehaviour
{

    // Counts how many times it's been hit
    public int nSnowManHitCount = 2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(nSnowManHitCount < 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "TennisBall")
        {
            nSnowManHitCount = nSnowManHitCount - 1;
        }
        
    }
}
