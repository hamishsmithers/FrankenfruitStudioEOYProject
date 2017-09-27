using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    bool bEscapeToggle = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (bEscapeToggle)
            {
                Time.timeScale = 0;
                bEscapeToggle = false;
            }
            else if (!bEscapeToggle)
            {
                Time.timeScale = 1;
                bEscapeToggle = true;
            }
        }
    }
}
