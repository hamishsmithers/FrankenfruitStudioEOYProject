using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMusic : MonoBehaviour {

    public Slider sliMusicSlider = null;
    public AudioSource MusicAudioSource = null;

    // Use this for initialization
    void Start ()
    {
        sliMusicSlider.value = Global.MusicVolume;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Global.MusicVolume = sliMusicSlider.value;
        MusicAudioSource.volume = Global.MusicVolume;
    }
}
