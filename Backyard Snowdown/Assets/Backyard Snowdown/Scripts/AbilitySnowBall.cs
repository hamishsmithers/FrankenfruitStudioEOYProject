using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySnowBall : MonoBehaviour
{
    public float m_fSlowDuration = 1.0f;
    private float m_fSlowCount = 0.0f;

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
            // create a slow function in player and do p.slow
            if (m_fSlowDuration > m_fSlowCount)
            {
                //p.Slow();
                m_fSlowCount += Time.deltaTime;
            }
            else
            {
                m_fSlowCount = 0.0f;
            }
        }

        Destroy(gameObject);
    }
}
