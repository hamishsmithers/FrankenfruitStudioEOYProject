//-------------------------------------------------------------------------------
// Filename:        GameTimer.cs
//
// Description:     Prints out the game timer text in a polished way.
//
// Author:          Mitchell Cattini-Schultz
// Editors:         Hamish Smithers
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameTimer : MonoBehaviour
{
    public Text m_txtGameTime;
    private float m_fGameTime = 0.0f;

	private int m_nSeconds = 0;
	private int m_nMinutes = 0;
	private float initialTime;
    
    // Next update in second
    //private int nextUpdate = 1;


    //--------------------------------------------------------------------------------------
    // Use this for initialization
    //--------------------------------------------------------------------------------------
    void Start()
    {
        m_txtGameTime = GetComponent<Text>();
        m_fGameTime = 0.0f;
    }

    //--------------------------------------------------------------------------------------
    // Update is called once per frame
    //--------------------------------------------------------------------------------------
    void Update()
    {
		m_fGameTime = Time.timeSinceLevelLoad;
		m_nSeconds = (int)m_fGameTime;
		m_nSeconds -= m_nMinutes * 60;

		if (m_nSeconds == 60) {
			m_nMinutes++;
			m_nSeconds = 0;
		}

        // if the seconds are above 9 then chane the formatting so that it displays nicely
		if (m_nSeconds < 10) {
			m_txtGameTime.text = m_nMinutes + ":0" + m_nSeconds;
		} else {
			m_txtGameTime.text = m_nMinutes + ":" + m_nSeconds;
		}


        // Mitchell's attempt does not work

        //string strMinutes = ((int)m_fGameTime / 60).ToString();
        //string strSeconds = (m_fGameTime % 60).ToString("f0");
        //m_txtGameTime.text = strMinutes + ":" + strSeconds;
        
        // breaks when ticking over each minute
        //  if (m_fGameTime % 60 > 9.5f)
        //      m_txtGameTime.text = strMinutes + ":" + strSeconds;
        //  else
        //      m_txtGameTime.text = strMinutes + ":0" + strSeconds;
        //m_txtGameTime.text=Time.time.ToString();
        //
        //m_fGameTime = Time.time;
        
        //// If the next update is reached
        //if (Time.time >= nextUpdate)
        //{
        //    // Change the next update (current second+1)
        //    nextUpdate = Mathf.FloorToInt(Time.time) + 1;
        //    string strMinutes = ((int)m_fGameTime / 60).ToString();
        //    string strSeconds = (m_fGameTime % 60).ToString("f0");
        //    m_txtGameTime.text = strMinutes + ":" + strSeconds;

        //    // breaks when ticking over each minute
        //    if (m_fGameTime % 60 > 9.5f)
        //        m_txtGameTime.text = strMinutes + ":" + strSeconds;
        //    else
        //        m_txtGameTime.text = strMinutes + ":0" + strSeconds;

        //    m_fGameTime += 1.0f;
        //}
    }
}
