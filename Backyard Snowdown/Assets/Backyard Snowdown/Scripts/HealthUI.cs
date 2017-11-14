using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    public Sprite sprHealth0 = null;
    public Sprite sprHealth1 = null;
    public Sprite sprHealth2 = null;
    public Sprite sprHealth3 = null;
    public Sprite sprHealth4 = null;
    public Sprite sprHealth5 = null;

    public Image imgPlayer = null;
    //public Image imgPlayer2 = null;
    //public Image imgPlayer3 = null;
    //public Image imgPlayer4 = null;

    private Player scpPlayer;
    // Use this for initialization
    void Start ()
    {
        scpPlayer = gameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(scpPlayer.m_nCurrentHealth <= 0)
            imgPlayer.sprite = sprHealth0;
        
        if (scpPlayer.m_nCurrentHealth == 1)
            imgPlayer.sprite = sprHealth1;
        
        if (scpPlayer.m_nCurrentHealth == 2)
            imgPlayer.sprite = sprHealth2;
        
        if (scpPlayer.m_nCurrentHealth == 3)
            imgPlayer.sprite = sprHealth3;
        
        if (scpPlayer.m_nCurrentHealth == 4)
            imgPlayer.sprite = sprHealth4;

        if (scpPlayer.m_nCurrentHealth == 5)
            imgPlayer.sprite = sprHealth5;
    }
}
