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
    [LabelOverride("Sound GameObject")]
    [Tooltip("")]
    public GameObject goSound = null;
    //--------------------------------------
    // Sound Effect for SFX Source
    //--------------------------------------
    //[LabelOverride("SFX Audio Source")]
    //[Tooltip("")]
    //public AudioSource sfxSource = null;
    //--------------------------------------
    // Sound Effect for Throw
    //--------------------------------------
    [LabelOverride("")]
    [Tooltip("")]
    public AudioClip[] sfxThrow = null;
    //----------------------------------------------------
    // Array of AudioClips storing the varied hit sounds.
    //----------------------------------------------------
    [LabelOverride("")]
    [Tooltip("")]
    public AudioClip[] sfxHitArray = null;
    //-----------------------------------------------------
    // Array of AudioClips storing the varied hurt sounds.
    //-----------------------------------------------------
    [LabelOverride("")]
    [Tooltip("")]
    public AudioClip[] sfxHurtArray = null;

    //-------------------------------------------------------------------------------
    // Creating a new instance of the Player class to reference parts of the script.
    //-------------------------------------------------------------------------------
    private Player scpPlayer;
    //-------------------------------------------------------------------------------
    // Creating a new instance of the Player class to reference parts of the script.
    //-------------------------------------------------------------------------------
    private Dash scpDash;

    //---------------------------
    // Awake is a Unity Function
    //---------------------------
    void Awake()
    {
        m_SharedInstance = this;
    }

    //-----------------------------
    // Use this for initialization
    //-----------------------------
    void Start ()
    {
        // Allocates the AudioSource class into the audio source called sfxSource
        //sfxSource = gameObject.GetComponent<AudioSource>();

        // Allocates the Player class into the scpPlayer to get the script and be able to use the script in this code.
        scpPlayer = gameObject.GetComponent<Player>();

        // Allocates the Dash class into the scpDash to get the script and be able to use the script in this code.
        scpDash = gameObject.GetComponent<Dash>();
    }
	
    //---------------------------------
	// Update is called once per frame
    //---------------------------------
	void Update ()
    {
        //Convert 0 to 1 volume to -80 to 20 db
        float volumeDB = Global.SFXVolume * 100.0f - 80.0f;
        m_audmixMixer.audioMixer.SetFloat("SFXVolume", volumeDB);
    }

    //------------------------------------------------------------
    // PlayDashAudio is the function called when a player dashes. 
    //------------------------------------------------------------
    public void PlayDashAudio()
    {
        // Instantiates the sound GameObject.
        GameObject sound = Instantiate(goSound);
        AudioSource sfxSource = sound.GetComponent<AudioSource>();

        // The sound effect source's mixer group is now the same as the m_audMixMixer.
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        // Sets the source's clip to the dash sound.
        sfxSource.clip = sfxDash;
        // Play the current clip.
        sfxSource.Play();
        // This varies the pitch randomly to emulate different clips.
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    //------------------------------------------------------------------------
    // PlayThrowAudio is the function called when a player throws a snowball. 
    //------------------------------------------------------------------------
    public void PlayThrowAudio()
    {
        // Instantiates the sound GameObject.
        GameObject sound = Instantiate(goSound);
        AudioSource sfxSource = sound.GetComponent<AudioSource>();

        // Creates an int that is randomised between 0 and the length of the throw array.
        int Selector = Random.Range(0, sfxThrow.Length);
        // The sound effect source's mixer group is now the same as the m_audMixMixer.
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        // Sets the source's clip to a randomized index in the throw array.
        sfxSource.clip = sfxThrow[Selector];
        // Play the current clip.
        sfxSource.Play();
        // This varies the pitch randomly to emulate different clips.
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    //------------------------------------------------------------------------
    // PlaySnowmanBoingAudio is the function called when the snowman jumps.
    //------------------------------------------------------------------------
    public void PlaySnowmanBoingAudio()
    {
        // Instantiates the sound GameObject.
        GameObject sound = Instantiate(goSound);
        AudioSource sfxSource = sound.GetComponent<AudioSource>();

        // The sound effect source's mixer group is now the same as the m_audMixMixer.
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        // Sets the source's clip to the snowman boing sound.
        sfxSource.clip = sfxSnowManBoing;
        // Play the current clip.
        sfxSource.Play();
        // This varies the pitch randomly to emulate different clips.
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    //--------------------------------------------------------------------------
    // PlaySnowBallHit is the function called when the snowball hits something.
    //--------------------------------------------------------------------------
    public void PlaySnowBallHit()
    {
        // Instantiates the sound GameObject.
        GameObject sound = Instantiate(goSound);
        AudioSource sfxSource = sound.GetComponent<AudioSource>();

        // Creates an int that is randomised between 0 and the length of the hit array.
        int Selector = Random.Range(0, sfxHitArray.Length);
        // The sound effect source's mixer group is now the same as the m_audMixMixer.
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        // Sets the source's clip to a randomized index in the hit array.
        sfxSource.clip = sfxHitArray[Selector];
        // Play the current clip.
        sfxSource.Play();
        // This varies the pitch randomly to emulate different clips.
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    //--------------------------------------------------------------------
    // PlayHurtAudio is the function called when the player loses health.
    //--------------------------------------------------------------------
    public void PlayHurtAudio()
    {
        // Instantiates the sound GameObject.
        GameObject sound = Instantiate(goSound);
        AudioSource sfxSource = sound.GetComponent<AudioSource>();

        // Creates an int that is randomised between 0 and the length of the hurt array.
        int Selector = Random.Range(0, sfxHurtArray.Length);
        // The sound effect source's mixer group is now the same as the m_audMixMixer.
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        // Sets the source's clip to a randomized index in the hurt array.
        sfxSource.clip = sfxHurtArray[Selector];
        // Play the current clip.
        sfxSource.Play();
        // This varies the pitch randomly to emulate different clips.
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    //-----------------------------------------------------------------
    // PlaySnowmanCrumble is the function called when the snowman dies
    //-----------------------------------------------------------------
    public void PlaySnowmanCrumble()
    {
        // Instantiates the sound GameObject.
        GameObject sound = Instantiate(goSound);
        AudioSource sfxSource = sound.GetComponent<AudioSource>();

        // The sound effect source's mixer group is now the same as the m_audMixMixer.
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        // Sets the source's clip to the snowman crumble sound effect.
        sfxSource.clip = sfxSnowManCrumble;
        // Play the current clip.
        sfxSource.Play();
        // This varies the pitch randomly to emulate different clips.
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    //-----------------------------------------------------------------------
    // PlaySnowmanSummon is the function called when the snowman is summoned
    //-----------------------------------------------------------------------
    public void PlaySnowmanSummon()
    {
        // Instantiates the sound GameObject.
        GameObject sound = Instantiate(goSound);
        AudioSource sfxSource = sound.GetComponent<AudioSource>();

        // The sound effect source's mixer group is now the same as the m_audMixMixer.
        sfxSource.outputAudioMixerGroup = m_audmixMixer;
        // Sets the source's clip to the snowman summon sound effect.
        sfxSource.clip = sfxSnowManSummon;
        // Play the current clip.
        sfxSource.Play();
        // This varies the pitch randomly to emulate different clips.
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }
}