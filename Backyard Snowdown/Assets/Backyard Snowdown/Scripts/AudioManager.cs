using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager m_SharedInstance;

    public AudioClip sfxDash = null;
    public AudioClip sfxSnowManBoing = null;
    public AudioClip sfxSnowManCrumble = null;
    public AudioClip sfxSnowManSummon = null;
    public AudioMixerGroup m_audmixMixer = null;
    public GameObject goSound = null;
    public AudioSource sfxSource = null;
    public AudioClip[] sfxThrow = null;
    public AudioClip[] sfxHitArray = null;
    public AudioClip[] sfxHurtArray = null;

    private Player scpPlayer;
    private Dash scpDash;

    void Awake()
    {
        m_SharedInstance = this;
    }

    // Use this for initialization
    void Start ()
    {
        sfxSource = gameObject.GetComponent<AudioSource>();
        scpPlayer = gameObject.GetComponent<Player>();
        scpDash = gameObject.GetComponent<Dash>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        sfxSource.volume = Global.SFXVolume;
    }

    public void PlayDashAudio()
    {
        GameObject sound = Instantiate(goSound);
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        sfxSource.clip = sfxDash;
        sfxSource.Play();
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    public void PlayThrowAudio()
    {
        GameObject sound = Instantiate(goSound);
        int Selector = Random.Range(0, sfxThrow.Length);
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        sfxSource.clip = sfxThrow[Selector];
        sfxSource.Play();
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    public void PlaySnowmanBoingAudio()
    {
        GameObject sound = Instantiate(goSound);
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        sfxSource.clip = sfxSnowManBoing;
        sfxSource.Play();
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    public void PlaySnowBallHit()
    {
        GameObject sound = Instantiate(goSound);
        int Selector = Random.Range(0, sfxHitArray.Length);
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        sfxSource.clip = sfxHitArray[Selector];
        sfxSource.Play();
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    public void PlayHurtAudio()
    {
        GameObject sound = Instantiate(goSound);
        int Selector = Random.Range(0, sfxHurtArray.Length);
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        sfxSource.clip = sfxHurtArray[Selector];
        sfxSource.Play();
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    public void PlaySnowmanCrumble()
    {
        GameObject sound = Instantiate(goSound);
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        sfxSource.clip = sfxSnowManCrumble;
        sfxSource.Play();
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    public void PlaySnowmanSummon()
    {
        GameObject sound = Instantiate(goSound);
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        sfxSource.clip = sfxSnowManSummon;
        sfxSource.Play();
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }
}