//-------------------------------------------------------------------------------
// Filename:        HealthUI.cs
//
// Description:     Health UI controls the health bars displayed on the UI.
//                  It is dependent on the player's current health.
//
// Author:          Nathan Nette
// Editors:         Nathan Nette
//-------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {


    //-------------------------------------------------------------------------------
    // Sprites for each level of health lost.
    //-------------------------------------------------------------------------------
    [LabelOverride("Health 0")]
    [Tooltip("The sprite for when the player has 0 health.")]
    public Sprite m_sprHealth0 = null;
    [LabelOverride("Health 1")]
    [Tooltip("The sprite for when the player has 1 health.")]
    public Sprite m_sprHealth1 = null;
    [LabelOverride("Health 2")]
    [Tooltip("The sprite for when the player has 2 health.")]
    public Sprite m_sprHealth2 = null;
    [LabelOverride("Health 3")]
    [Tooltip("The sprite for when the player has 3 health.")]
    public Sprite m_sprHealth3 = null;
    [LabelOverride("Health 4")]
    [Tooltip("The sprite for when the player has 4 health.")]
    public Sprite m_sprHealth4 = null;
    [LabelOverride("Health 5")]
    [Tooltip("The sprite for when the player has 5 health.")]
    public Sprite m_sprHealth5 = null;

    //-------------------------------------------------------------------------------
    // Image of the character.
    //-------------------------------------------------------------------------------
    [LabelOverride("Character Image")]
    [Tooltip("The image of the character.")]
    public Image m_imgPlayer = null;

    // Getting access to the player script.
    private Player m_scpPlayer;

    //-------------------------------------------------------------------------------
    // Use this for initialization
    //-------------------------------------------------------------------------------
    void Start ()
    {
        m_scpPlayer = gameObject.GetComponent<Player>();
	}

    //-------------------------------------------------------------------------------
    // Update is called once per frame
    //-------------------------------------------------------------------------------
    void Update ()
    {
        // If the player's health is 0, show the sprite with 0 bars of health.
		if(m_scpPlayer.m_nCurrentHealth <= 0)
            m_imgPlayer.sprite = m_sprHealth0;

        // If the player's health is 1, show the sprite with 1 bars of health.
        if (m_scpPlayer.m_nCurrentHealth == 1)
            m_imgPlayer.sprite = m_sprHealth1;

        // If the player's health is 2, show the sprite with 2 bars of health.
        if (m_scpPlayer.m_nCurrentHealth == 2)
            m_imgPlayer.sprite = m_sprHealth2;

        // If the player's health is 3, show the sprite with 3 bars of health.
        if (m_scpPlayer.m_nCurrentHealth == 3)
            m_imgPlayer.sprite = m_sprHealth3;

        // If the player's health is 4, show the sprite with 4 bars of health.
        if (m_scpPlayer.m_nCurrentHealth == 4)
            m_imgPlayer.sprite = m_sprHealth4;

        // If the player's health is 5, show the sprite with 5 bars of health.
        if (m_scpPlayer.m_nCurrentHealth == 5)
            m_imgPlayer.sprite = m_sprHealth5;
    }
}
