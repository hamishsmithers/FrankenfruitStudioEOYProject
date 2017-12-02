//------------------------------------------------------------------------------------------
// Filename:        PlayerAI.cs
//
// Description:     The player AI script controls the NavMesh functionality when
//                  the players die.
//
// Author:          Nathan Nette
// Editors:         Nathan Nette
//------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{

    //------------------------------------------------------------------------------------------
    // GameObject to store the Right wall point.
    //------------------------------------------------------------------------------------------
    [LabelOverride("Right Wall Point")]
    [Tooltip("Stores the GameObject for the Right wall point.")]
    public GameObject m_goRight = null;
    //------------------------------------------------------------------------------------------
    // GameObject to store the Left wall point.
    //------------------------------------------------------------------------------------------
    [LabelOverride("Left Wall Point")]
    [Tooltip("Stores the GameObject for the Left wall point.")]
    public GameObject m_goLeft = null;

    //------------------------------------------------------------------------------------------
    // Use this for initialization, called even if the script is disabled.
    //------------------------------------------------------------------------------------------
    void Awake()
    {
        // Deletes the dash component off the player.
        Dash dash = gameObject.GetComponent<Dash>();
        Destroy(dash);

        // Deletes the AbilitySnowman component off the player.
        AbilitySnowMan snowman = gameObject.GetComponent<AbilitySnowMan>();
        Destroy(snowman);

        // Deletes the GiantSnowball component off the player.
        //EliminatedAbilityGiantSnowBall giantsnowball = gameObject.GetComponent<EliminatedAbilityGiantSnowBall>();
        //Destroy(giantsnowball);

        // Deletes the box collider component off the player.
        BoxCollider boxCol = gameObject.GetComponent<BoxCollider>();
        Destroy(boxCol);

        // Adds a navmesh agent to the player.
        gameObject.AddComponent<NavMeshAgent>();

        // Seeks the closest wall point to walk to when out of health.
        m_goRight = GameObject.Find("Right Wall Point");
        m_goLeft = GameObject.Find("Left Wall Point");

        // Sets the rigidbody to kinematic to prevent any physics.
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame.
    //------------------------------------------------------------------------------------------
    void Update()
    {
        // Seeks Right.
        if (transform.position.x > 0)
        {
            // Go to right.
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = m_goRight.transform.position;
        }

        // Seeks Left.
        if (transform.position.x < 0)
        {
            // Go to left.
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = m_goLeft.transform.position;
        }
    }

}
