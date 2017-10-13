using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;       // Be sure to include this if you want an object to have Xbox input

public class ScoreManager : MonoBehaviour {

    static List<int> PlayerRank = new List<int>();
    static int nDeathCount = 0;
    public XboxController controller;

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

        if (XCI.GetButton(XboxButton.Start, controller))
        {
            SceneManager.LoadScene(0);
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
