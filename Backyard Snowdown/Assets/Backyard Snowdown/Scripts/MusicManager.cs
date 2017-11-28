using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

    public AudioClip[] Music;
    public AudioSource MusicSource;

    //--------------------------------------
    // Audio Mixer for Sound Effects
    //--------------------------------------
    [LabelOverride("MusicMixer")]
    [Tooltip("The audio mixer titled Music.")]
    public AudioMixerGroup m_audmixMixer = null;


    // Use this for initialization
    void Start ()
    {
        MusicSource = gameObject.GetComponent<AudioSource>();

        int Selector = Random.Range(0, Music.Length);
        MusicSource.clip = Music[Selector];
        MusicSource.Play();
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
        //MusicSource.volume = Global.MusicVolume;
        float fMusicVolumeDB = LinearToDecibel(Global.MusicVolume);
        m_audmixMixer.audioMixer.SetFloat("MusicVolume", fMusicVolumeDB);
    }
}
