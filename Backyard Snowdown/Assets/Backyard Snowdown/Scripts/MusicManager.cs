using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip[] Music;
    private AudioSource Source;


	// Use this for initialization
	void Start ()
    {
        Source = gameObject.GetComponent<AudioSource>();

        int Selector = Random.Range(0, Music.Length);
        Source.clip = Music[Selector];
        Source.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
}
