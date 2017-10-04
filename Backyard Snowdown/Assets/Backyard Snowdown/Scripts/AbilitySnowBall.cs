using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySnowBall : MonoBehaviour
{
    public float fSlowDuration = 1.0f;
    private float fSlowCount = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Character")
        {
            
            //Make character slow for a public variable
            Player p = col.gameObject.GetComponent<Player>();
            // create a slow function in player and do p.slow
            if (fSlowDuration > fSlowCount)
            {
                //p.Slow();
                fSlowCount += Time.deltaTime;
            }
            else
            {
                fSlowCount = 0.0f;
            }
        }

        Destroy(gameObject);
    }
}
