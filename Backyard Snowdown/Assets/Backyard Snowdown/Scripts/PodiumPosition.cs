using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumPosition : MonoBehaviour {

    //--------------
    // Players Array
    //--------------
    [Tooltip("This list is filled with the character prefabs in the order of Elements: 0.TeddyBear 1.Supergirl 2.WinterClothes 3.HelicopterHat.")]
    public GameObject[] m_ArrPlayers;
    //-------------
    // Podium Array
    //-------------
    [Tooltip("This list is filled with the SpawnBox GameObjects in the order of Elements: 0.SpawnBox04 1.SpawnBox03 2.SpawnBox02 3.SpawnBox01.")]
    public GameObject[] m_ArrPodium;

    // Use this for initialization
    void Start ()
    {
        // Character 1st in pos 1
        // Character 2nd in pos 2
        // Character 3rd in pos 3
        // Character 4th in pos 4
        //ScoreManager scpScoreManager = gameObject.GetComponent<ScoreManager>();
        for (int i = 0; i < 4; i++)
        {
            m_ArrPlayers[i].transform.parent = m_ArrPodium[3].transform;
            m_ArrPlayers[i].transform.localPosition = Vector3.zero;
        }

        for (int i = 0; i < ScoreManager.GetPlayerCount(); i++)
        {
            int nPlayer = ScoreManager.GetPodiumRank(i);
            m_ArrPlayers[nPlayer].transform.parent = m_ArrPodium[i].transform;
            m_ArrPlayers[nPlayer].transform.localPosition = Vector3.zero;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //------------------------------------------------
    // Implementation
    //------------------------------------------------

    // Create a list that holds which player got which spot. 
    // When this scene loads, it puts the players in the different podium spawnboxes.


}
