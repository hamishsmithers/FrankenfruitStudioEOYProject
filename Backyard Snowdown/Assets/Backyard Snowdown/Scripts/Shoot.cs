using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
  
    public GameObject m_TennisBall = null;

    private bool bBallPickedUp = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Checks for whether the ball is picked up by player
        if (bBallPickedUp == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject copy = Instantiate(m_TennisBall);
                copy.transform.position = transform.position + transform.forward;

                Rigidbody rb = copy.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 1000, ForceMode.Acceleration);

                bBallPickedUp = false;
            }
        }
    }

    private void OnTriggerEnter(Collider TennisBall)
    {
        bBallPickedUp = true;
    }
}
