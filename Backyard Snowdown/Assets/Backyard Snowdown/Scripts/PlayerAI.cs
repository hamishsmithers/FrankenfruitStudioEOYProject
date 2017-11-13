using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour {

    //------------------------------------------
    // GameObject to store the Right wall point.
    //------------------------------------------
    [LabelOverride("Right Wall Point")]
    [Tooltip("Stores the GameObject for the Right wall point.")]
    public GameObject goRight = null;
    //-----------------------------------------
    // GameObject to store the Left wall point.
    //-----------------------------------------
    [LabelOverride("Left Wall Point")]
    [Tooltip("Stores the GameObject for the Left wall point.")]
    public GameObject goLeft = null;

	// Use this for initialization
	void Awake ()
    {
        //Player player = gameObject.GetComponent<Player>();
        //Destroy(player);

        Dash dash = gameObject.GetComponent<Dash>();
        Destroy(dash);

        AbilitySnowMan snowman = gameObject.GetComponent<AbilitySnowMan>();
        Destroy(snowman);

        //EliminatedAbilityGiantSnowBall giantsnowball = gameObject.GetComponent<EliminatedAbilityGiantSnowBall>();
        //Destroy(giantsnowball);

        BoxCollider boxCol = gameObject.GetComponent<BoxCollider>();
        Destroy(boxCol);

        gameObject.AddComponent<NavMeshAgent>();

        goRight = GameObject.Find("Right Wall Point");
        goLeft = GameObject.Find("Left Wall Point");


    }
    
	
	// Update is called once per frame
	void Update ()
    {
		if(transform.position.x > 0)
        {
            //go to right
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = goRight.transform.position;
        }

        if (transform.position.x < 0)
        {
            //go to left
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = goLeft.transform.position;
        }
    }

}
