using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    static List<int> PlayerRank = new List<int>();
    static int nDeathCount = 0;

    // Use this for initialization
    void Start ()
    {
        nDeathCount = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (nDeathCount > 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    public static void Reset()
    {
        PlayerRank.Clear();
    }

    public static void PlayerFinish(int nPlayer)
    {
        ++nDeathCount;
        PlayerRank.Add(nPlayer);
    }

    public static int GetPodiumRank(int nWhichRank)
    {
        return PlayerRank[nWhichRank];
    }

    public static int GetPlayerCount()
    {
        return PlayerRank.Count;
    }
}
