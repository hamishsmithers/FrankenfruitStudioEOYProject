using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumPosition : MonoBehaviour {

    public GameObject[] PlayersArray;
    public GameObject[] PodiumArray;

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
            PlayersArray[i].transform.parent = PodiumArray[3].transform;
            PlayersArray[i].transform.localPosition = Vector3.zero;
        }

        for (int i = 0; i < ScoreManager.GetPlayerCount(); i++)
        {
            int nPlayer = ScoreManager.GetPodiumRank(i);
            PlayersArray[nPlayer].transform.parent = PodiumArray[i].transform;
            PlayersArray[nPlayer].transform.localPosition = Vector3.zero;
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
