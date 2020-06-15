using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayExplosionSound : MonoBehaviour {

	private AudioSource audio;

	void Awake(){	
		audio = gameObject.GetComponent<AudioSource> ();
	}

	void OnEnable(){
		audio.Play ();		
	}
}
