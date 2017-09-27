using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
  
    public GameObject m_TennisBall = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject copy = Instantiate(m_TennisBall);
            copy.transform.position = transform.position + transform.forward;

            Rigidbody rb = copy.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 1000, ForceMode.Acceleration);
            
        }
    }
}
