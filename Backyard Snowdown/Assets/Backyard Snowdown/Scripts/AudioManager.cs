using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager m_SharedInstance;


    //--------------------------------------
    // Sound Effect for Dash
    //--------------------------------------
    [LabelOverride("Dash SFX")]
    [Tooltip("The sound effect for when the player dashes.")]
    public AudioClip sfxDash = null;
    //--------------------------------------
    // Sound Effect for Snowman Jumping
    //--------------------------------------
    [LabelOverride("Boing Snowman SFX")]
    [Tooltip("The boing sound effect for when the snowman jumps.")]
    public AudioClip sfxSnowManBoing = null;
    //--------------------------------------
    // Sound Effect for Snowman Crumbling
    //--------------------------------------
    [LabelOverride("Crumble Snowman SFX")]
    [Tooltip("The crumbling sound effect for when the snowman dies.")]
    public AudioClip sfxSnowManCrumble = null;
    //-----------------------------------------
    // Sound Effect for Snowman being Summoned
    //-----------------------------------------
    [LabelOverride("Summon Snowman SFX")]
    [Tooltip("The sound effect for when the snowman is summoned.")]
    public AudioClip sfxSnowManSummon = null;
    //--------------------------------------
    // Audio Mixer for Sound Effects
    //--------------------------------------
    [LabelOverride("SFX Mixer")]
    [Tooltip("The audio mixer titled SFX.")]
    public AudioMixerGroup m_audmixMixer = null;
    //--------------------------------------
    // Sound Effect for Sounds
    //--------------------------------------
    [LabelOverride("GameObject Sound")]
    [Tooltip("")]
    public GameObject goSound = null;
    //--------------------------------------
    // Sound Effect for SFX Source
    //--------------------------------------
    [LabelOverride("")]
    [Tooltip("")]
    public AudioSource sfxSource = null;
    //--------------------------------------
    // Sound Effect for Dash
    //--------------------------------------
    [LabelOverride("")]
    [Tooltip("")]
    public AudioClip[] sfxThrow = null;
    //--------------------------------------
    // Sound Effect for Dash
    //--------------------------------------
    [LabelOverride("")]
    [Tooltip("")]
    public AudioClip[] sfxHitArray = null;
    //--------------------------------------
    // Sound Effect for Dash
    //--------------------------------------
    [LabelOverride("")]
    [Tooltip("")]
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