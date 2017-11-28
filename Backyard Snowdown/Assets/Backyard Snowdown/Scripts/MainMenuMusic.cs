using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuMusic : MonoBehaviour {

    public Slider sliMusicSlider = null;
    public AudioSource MusicAudioSource = null;

    //--------------------------------------
    // Audio Mixer for Sound Effects
    //--------------------------------------
    [LabelOverride("MusicMixer")]
    [Tooltip("The audio mixer titled Music.")]
    public AudioMixerGroup m_audmixMixer = null;

    // Use this for initialization
    void Start ()
    {
        sliMusicSlider.value = Global.MusicVolume;
	}

    private float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    // Update is called once per frame
    void Update ()
    {
        Global.MusicVolume = sliMusicSlider.value;
        float fMusicVolumeDB = LinearToDecibel(Global.MusicVolume);
        m_audmixMixer.audioMixer.SetFloat("MusicVolume", fMusicVolumeDB);
        //MusicAudioSource.volume = Global.MusicVolume;
    }
}
