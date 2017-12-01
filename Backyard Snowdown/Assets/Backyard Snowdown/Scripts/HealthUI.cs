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
    public Sprite sprHealth0 = null;
    [LabelOverride("Health 1")]
    [Tooltip("The sprite for when the player has 1 health.")]
    public Sprite sprHealth1 = null;
    [LabelOverride("Health 2")]
    [Tooltip("The sprite for when the player has 2 health.")]
    public Sprite sprHealth2 = null;
    [LabelOverride("Health 3")]
    [Tooltip("The sprite for when the player has 3 health.")]
    public Sprite sprHealth3 = null;
    [LabelOverride("Health 4")]
    [Tooltip("The sprite for when the player has 4 health.")]
    public Sprite sprHealth4 = null;
    [LabelOverride("Health 5")]
    [Tooltip("The sprite for when the player has 5 health.")]
    public Sprite sprHealth5 = null;

    //-------------------------------------------------------------------------------
    // Image of the character.
    //-------------------------------------------------------------------------------
    [LabelOverride("Character Image")]
    [Tooltip("The image of the character.")]
    public Image imgPlayer = null;

    // Getting access to the player script.
    private Player scpPlayer;

    //-------------------------------------------------------------------------------
    // Use this for initialization
    //-------------------------------------------------------------------------------
    void Start ()
    {
        scpPlayer = gameObject.GetComponent<Player>();
	}

    //-------------------------------------------------------------------------------
    // Update is called once per frame
    //-------------------------------------------------------------------------------
    void Update ()
    {
        // If the player's health is 0, show the sprite with 0 bars of health.
		if(scpPlayer.m_nCurrentHealth <= 0)
            imgPlayer.sprite = sprHealth0;

        // If the player's health is 1, show the sprite with 1 bars of health.
        if (scpPlayer.m_nCurrentHealth == 1)
            imgPlayer.sprite = sprHealth1;

        // If the player's health is 2, show the sprite with 2 bars of health.
        if (scpPlayer.m_nCurrentHealth == 2)
            imgPlayer.sprite = sprHealth2;

        // If the player's health is 3, show the sprite with 3 bars of health.
        if (scpPlayer.m_nCurrentHealth == 3)
            imgPlayer.sprite = sprHealth3;

        // If the player's health is 4, show the sprite with 4 bars of health.
        if (scpPlayer.m_nCurrentHealth == 4)
            imgPlayer.sprite = sprHealth4;

        // If the player's health is 5, show the sprite with 5 bars of health.
        if (scpPlayer.m_nCurrentHealth == 5)
            imgPlayer.sprite = sprHealth5;
    }
}
