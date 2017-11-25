using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChargeSlider : MonoBehaviour {

    //----------------------------------------------
    // Charge Throw Slider
    //----------------------------------------------
    [LabelOverride("Charge Background")]
    public Image m_imgBackground = null;
    [LabelOverride("Charge Foreground")]
    public Image m_imgForeground = null;

    //----------------------------------------------
    // Minimum Charge
    //----------------------------------------------
    [LabelOverride("Min Charge")]
    [Tooltip("The float value that the slider will begin at.")]
    public float m_fMinCharge = 0.0f;
    //public float m_fMaxCharge = 100.0f;

    public Sprite m_sprFullRing = null;
    public Sprite m_sprEmptyRing = null;

    private Player scpPlayer;
    private float m_fMaxCharge;

    private void OnEnable()
    {
        m_imgForeground.fillAmount = m_fMinCharge;
    }
    

	// Use this for initialization
	void Start ()
    {
        scpPlayer = gameObject.GetComponent<Player>();
        m_fMaxCharge = scpPlayer.m_fMaxCharge;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_fMaxCharge == 0.0f)
            return;

        m_imgForeground.fillAmount = scpPlayer.m_fChargeTimer / m_fMaxCharge;
        
        //Debug.Log(m_sliChargeSlider.value);

        if(scpPlayer.m_bHasBall)
        {
            m_imgBackground.sprite = m_sprFullRing;
            m_imgForeground.sprite = m_sprFullRing;
        }
        else 
        {
            m_imgBackground.sprite = m_sprEmptyRing;
            m_imgForeground.sprite = m_sprEmptyRing;
        }
    }


}
