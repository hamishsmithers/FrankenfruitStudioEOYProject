using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmullRomSpline : MonoBehaviour 
{

    public Transform[] m_tPointList;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for(int i = 0; i < m_tPointList.Length; ++i)
        {
            if ((i == 0 || i == m_tPointList.Length - 2 || i == m_tPointList.Length - 1))
            {
                continue;
            }

            DisplayCatmullRomSpline(i);
        }
    }

    private void DisplayCatmullRomSpline(int m_nPos)
    {
        Vector3 m_vP0 = m_tPointList[ClampListPosition(m_nPos - 1)].position;
        Vector3 m_vP1 = m_tPointList[m_nPos].position;
        Vector3 m_vP2 = m_tPointList[ClampListPosition(m_nPos + 1)].position;
        Vector3 m_vP3 = m_tPointList[ClampListPosition(m_nPos + 2)].position;

        Vector3 m_vLastPosition = m_vP1;

        float fResolution = 0.1f;

        int loop = Mathf.FloorToInt(1f / fResolution);

        for(int i = 0; i <= loop; ++i)
        {
            float t = i * fResolution;

            Vector3 m_vNewPosition = GetCatmullRomSplinePosition(t, m_vP0, m_vP1, m_vP2, m_vP3);

            Gizmos.DrawLine(m_vLastPosition, m_vNewPosition);

            m_vLastPosition = m_vNewPosition;
        }

    }

    int ClampListPosition(int m_nPos)
    {
        if (m_nPos < 0)
            m_nPos = m_tPointList.Length - 1;

        if (m_nPos > m_tPointList.Length)
            m_nPos = 1;

        else if (m_nPos > m_tPointList.Length - 1)
            m_nPos = 0;

        return m_nPos;
    }

    Vector3 GetCatmullRomSplinePosition(float t, Vector3 m_vP0, Vector3 m_vP1, Vector3 m_vP2, Vector3 m_vP3)
    {
        Vector3 a = 2.0f * m_vP1;
        Vector3 b = m_vP2 - m_vP0;
        Vector3 c = 2.0f * m_vP0 - 5.0f * m_vP1 + 4.0f * m_vP2 - m_vP3;
        Vector3 d = -m_vP0 + 3.0f * m_vP1 - 3.0f * m_vP2 + m_vP3;

        Vector3 m_vPosition = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return m_vPosition;
    }
    
}
