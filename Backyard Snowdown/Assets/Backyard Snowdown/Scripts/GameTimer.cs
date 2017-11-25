using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameTimer : MonoBehaviour
{
    public Text m_txtGameTime;
    private float m_fGameTime = 0.0f;
    
    // Next update in second
    private int nextUpdate = 1;

    // Use this for initialization
    void Start()
    {
        m_txtGameTime = GetComponent<Text>();
        m_fGameTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // If the next update is reached
        if (Time.time >= nextUpdate)
        {
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            string strMinutes = ((int)m_fGameTime / 60).ToString();
            string strSeconds = (m_fGameTime % 60).ToString("f0");
            m_txtGameTime.text = strMinutes + ":" + strSeconds;

            // breaks when ticking over each minute
            if (m_fGameTime % 60 > 9.5f)
                m_txtGameTime.text = strMinutes + ":" + strSeconds;
            else
                m_txtGameTime.text = strMinutes + ":0" + strSeconds;

            m_fGameTime += 1.0f;
        }
    }
}
