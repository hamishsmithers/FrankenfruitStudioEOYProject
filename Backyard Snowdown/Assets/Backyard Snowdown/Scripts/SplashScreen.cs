using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]

public class SplashScreen : MonoBehaviour {

	public MovieTexture movie;

    //Time until loading main menu
    public float timeToDelay;

	private AudioSource audSource;

	// Use this for initialization
	void Start () {

		GetComponent<RawImage>().texture = movie as MovieTexture;
		audSource = GetComponent<AudioSource>();
		audSource.clip = movie.audioClip;
		movie.Play();
		audSource.Play();
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > timeToDelay){
            SceneManager.LoadScene("Main Menu");
        }


		
	}
}
