using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

	public float speed = 0.1f;
	private Renderer rend;

	void Start () {
		rend = GetComponent<Renderer> ();		
	}
	
	void Update () {
		rend.material.mainTextureOffset = new Vector2 (0f, Time.time * speed);		
	}
}
