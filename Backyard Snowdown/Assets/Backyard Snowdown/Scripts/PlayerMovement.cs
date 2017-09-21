using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float m_fSpeed;

	// Use this for initialization
	void Awake ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 v3Pos;
        v3Pos.x = transform.position.x;

        //Mouse raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 target = hit.point;
        target.y = transform.position.y;
        transform.LookAt(target);

        //---------------------------------------------------------------------
        //Movement
        //---------------------------------------------------------------------
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * m_fSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * m_fSpeed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, Time.deltaTime * 200.0f, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, Time.deltaTime * -200.0f, 0);
        }

        //---------------------------------------------------------------------
        // Look at mouse position
        //---------------------------------------------------------------------

        
        //fMouseX = v3MousePos.x;
        //fMouseY = v3MousePos.y;
        //fMouseZ = v3MousePos.z;
        
    }
}
