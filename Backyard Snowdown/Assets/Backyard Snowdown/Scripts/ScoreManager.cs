using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;       // Be sure to include this if you want an object to have Xbox input

public class ScoreManager : MonoBehaviour {

    static List<int> m_LstPlayerRank = new List<int>();
    static int m_nDeathCount = 0;
    static int m_nRoundsWonCount = 0;
    public XboxController m_Controller;

    // Use this for initialization
    void Start ()
    {
        m_nDeathCount = 0;
        m_nRoundsWonCount = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_nDeathCount > 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndRound");
        }
    }
    
    public static int GetnRoundsWonCount()
    {
        return m_nRoundsWonCount;
    }

    public static void SetnRoundsWonCount(int a)
    {
        m_nRoundsWonCount = a;
    }

    public static void IteratenRoundsWonCount()
    {
        ++m_nRoundsWonCount;
    }

    public static void Reset()
    {
        m_LstPlayerRank.Clear();
    }

    public static void PlayerFinish(int nPlayer)
    {
        ++m_nDeathCount;
        m_LstPlayerRank.Add(nPlayer);
    }

    public static int GetPodiumRank(int nWhichRank)
    {
        return m_LstPlayerRank[nWhichRank];
    }

    public static int GetPlayerCount()
    {
        return m_LstPlayerRank.Count;
    }
}
