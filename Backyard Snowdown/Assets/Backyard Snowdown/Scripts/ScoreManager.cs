//------------------------------------------------------------------------------------------
// Filename:        ScoreManager.cs
//
// Description:     Score Manager is used for the player rank list. It stores a player when
//                  they die and then applies that to the podium position.
//
// Author:          Nathan Nette
// Editors:         Nathan Nette
//------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;       // Be sure to include this if you want an object to have Xbox input

public class ScoreManager : MonoBehaviour
{
    // The list that players get added to to get the order of them dying.
    static List<int> m_LstPlayerRank = new List<int>();

    // The variable to be checked whether there is only one player left alive.
    static int m_nDeathCount = 0;

    // The rounds won counter was for a function that didn't get implemented due to scope.
    // It was meant to be counting how many rounds a player had won and the first to 3 would win.
    static int m_nRoundsWonCount = 0;

    // XInput
    public XboxController m_Controller;

    //------------------------------------------------------------------------------------------
    // Use this for initialization, called when the script is first accessed.
    //------------------------------------------------------------------------------------------
    void Start()
    {
        m_nDeathCount = 0;
        m_nRoundsWonCount = 0;
    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame.
    //------------------------------------------------------------------------------------------
    void Update()
    {
        // If there is only one player left, go to end round scene.
        if (m_nDeathCount > 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndRound");
        }
        
    }

    //------------------------------------------------------------------------------------------
    // Getter of rounds won count.
    //------------------------------------------------------------------------------------------
    public static int GetnRoundsWonCount()
    {
        return m_nRoundsWonCount;
    }

    //------------------------------------------------------------------------------------------
    // Setter for rounds won count.
    //------------------------------------------------------------------------------------------
    public static void SetnRoundsWonCount(int a)
    {
        m_nRoundsWonCount = a;
    }

    //------------------------------------------------------------------------------------------
    // Add one to the rounds won count.
    //------------------------------------------------------------------------------------------
    public static void IteratenRoundsWonCount()
    {
        ++m_nRoundsWonCount;
    }

    //------------------------------------------------------------------------------------------
    // Clear the player rank list.
    //------------------------------------------------------------------------------------------
    public static void Reset()
    {
        m_LstPlayerRank.Clear();
    }

    //------------------------------------------------------------------------------------------
    // When a player dies, this gets called to add them to the list and iterate the death count.
    //------------------------------------------------------------------------------------------
    public static void PlayerFinish(int nPlayer)
    {
        ++m_nDeathCount;
        m_LstPlayerRank.Add(nPlayer);
    }

    //------------------------------------------------------------------------------------------
    // Getter for podium rank.
    //------------------------------------------------------------------------------------------
    public static int GetPodiumRank(int nWhichRank)
    {
        return m_LstPlayerRank[nWhichRank];
    }

    //------------------------------------------------------------------------------------------
    // Getter for player rank count.
    //------------------------------------------------------------------------------------------
    public static int GetPlayerCount()
    {
        return m_LstPlayerRank.Count;
    }
}
