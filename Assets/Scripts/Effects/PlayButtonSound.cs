using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour {

	private AudioSource audio;
	
	public static PlayButtonSound SharedInstance;
	
	void Awake () {	
		SharedInstance = this;
		audio = GetComponent<AudioSource> ();
	}
	
	public void PlaySound(){
		
		audio.Play();
		
	}
}
