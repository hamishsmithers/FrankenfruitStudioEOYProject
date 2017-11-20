using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip[] Music;
    public AudioSource MusicSource;


	// Use this for initialization
	void Start ()
    {
        MusicSource = gameObject.GetComponent<AudioSource>();

        int Selector = Random.Range(0, Music.Length);
        MusicSource.clip = Music[Selector];
        MusicSource.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MusicSource.volume = Global.MusicVolume;
    }
}
