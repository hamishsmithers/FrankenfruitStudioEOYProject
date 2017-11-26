using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSFX : MonoBehaviour {

    public Slider sliSFXSlider = null;
    public AudioSource SFXAudioSource = null;
    public AudioClip buttonSelected = null;
    public AudioClip buttonClicked = null;
    public AudioClip buttonBack = null;

    // Use this for initialization
    void Start()
    {
        sliSFXSlider.value = Global.SFXVolume;
    }

    // Update is called once per frame
    void Update()
    {
        Global.SFXVolume = sliSFXSlider.value;
        SFXAudioSource.volume = Global.SFXVolume;
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
