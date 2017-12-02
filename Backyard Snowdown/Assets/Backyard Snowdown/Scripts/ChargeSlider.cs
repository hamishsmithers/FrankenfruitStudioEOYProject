//------------------------------------------------------------------------------------------
// Filename:        ChargeSlider.cs
//
// Description:     Charge Slider applies the charge variable to the slider and displays
//                  the charge progression on the player circle underneath the player. 
//                  It changes colour dependent on the charge progress.
//
// Author:          Nathan Nette
// Editors:         Nathan Nette
//------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSlider : MonoBehaviour {

    //------------------------------------------------------------------------------------------
    // Charge Throw Slider
    //------------------------------------------------------------------------------------------
    [LabelOverride("Charge Background")]
    public Image m_imgBackground = null;
    [LabelOverride("Charge Foreground")]
    public Image m_imgForeground = null;

    //------------------------------------------------------------------------------------------
    // Minimum Charge
    //------------------------------------------------------------------------------------------
    [LabelOverride("Min Charge")]
    [Tooltip("The float value that the slider will begin at.")]
    public float m_fMinCharge = 0.0f;

    //------------------------------------------------------------------------------------------
    // Full Ring Sprite
    //------------------------------------------------------------------------------------------
    [LabelOverride("Full Ring Sprite")]
    [Tooltip("The sprite that is the full player circle sprite.")]
    public Sprite m_sprFullRing = null;

    //------------------------------------------------------------------------------------------
    // Empty Ring Sprite
    //------------------------------------------------------------------------------------------
    [LabelOverride("Min Charge")]
    [Tooltip("The sprite that is the empty player circle sprite.")]
    public Sprite m_sprEmptyRing = null;

    //------------------------------------------------------------------------------------------
    // Creating a reference for the player script.
    //------------------------------------------------------------------------------------------
    private Player m_scpPlayer;

    //------------------------------------------------------------------------------------------
    // A float to store the maximum charge.
    //------------------------------------------------------------------------------------------
    private float m_fMaxCharge;

    //------------------------------------------------------------------------------------------
    // When the slider is enabled, it sets the fill amount to min charge.
    //------------------------------------------------------------------------------------------
    private void OnEnable()
    {
        m_imgForeground.fillAmount = m_fMinCharge;
    }

    //------------------------------------------------------------------------------------------
    // Use this for initialization
    //------------------------------------------------------------------------------------------
    void Start ()
    {
        // Allocates the player class into the script to get values and functions in Player 
        // script.
        m_scpPlayer = gameObject.GetComponent<Player>();
        // Sets the max charge to the max charge from the player script.
        m_fMaxCharge = m_scpPlayer.m_fMaxCharge;
    }

    //------------------------------------------------------------------------------------------
    // Update is called once per frame
    //------------------------------------------------------------------------------------------
    void Update ()
    {
        // If max charge is equal to 0, do nothing.
        if (m_fMaxCharge == 0.0f)
            return;

        // Sets the slider fill amount to the charge timer divided by max charge.
        m_imgForeground.fillAmount = m_scpPlayer.m_fChargeTimer / m_fMaxCharge;
        
        // If the player has a ball, set the player circle to full ring sprite.
        if(m_scpPlayer.m_bHasBall)
        {
            m_imgBackground.sprite = m_sprFullRing;
            m_imgForeground.sprite = m_sprFullRing;
        }
        // If they don't have a ball then keep the player circle as the empty sprite.
        else 
        {
            m_imgBackground.sprite = m_sprEmptyRing;
            m_imgForeground.sprite = m_sprEmptyRing;
        }
    }
}
