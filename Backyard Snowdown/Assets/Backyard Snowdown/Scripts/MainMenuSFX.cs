using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuSFX : MonoBehaviour {

    public Slider sliSFXSlider = null;
    public AudioSource SFXAudioSource = null;
    public AudioClip buttonSelected = null;
    public AudioClip buttonClicked = null;
    public AudioClip buttonBack = null;


    //--------------------------------------
    // Audio Mixer for Sound Effects
    //--------------------------------------
    [LabelOverride("SFX Mixer")]
    [Tooltip("The audio mixer titled Music.")]
    public AudioMixerGroup m_audmixMixer = null;

    
    private float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    // Use this for initialization
    void Start()
    {
        sliSFXSlider.value = Global.SFXVolume;
    }

    // Update is called once per frame
    void Update()
    {
        Global.SFXVolume = sliSFXSlider.value;
        //Global.MusicVolume = sliSFXSlider.value;
        float fSFXVolumeDB = LinearToDecibel(Global.SFXVolume);
        m_audmixMixer.audioMixer.SetFloat("SFXVolume", fSFXVolumeDB);
        //SFXAudioSource.volume = Global.SFXVolume;
    }

    public void OnButtonSelect()
    {
        //change audiosource clip to select and play.
        SFXAudioSource.clip = buttonSelected;
        SFXAudioSource.Play();
    }

    public void OnButtonClick()
    {
        //change audiosource clip to select and play.
        SFXAudioSource.clip = buttonClicked;
        SFXAudioSource.Play();
    }

    public void OnButtonBack()
    {
        //change audiosource clip to select and play.
        SFXAudioSource.clip = buttonBack;
        SFXAudioSource.Play();
    }
}
