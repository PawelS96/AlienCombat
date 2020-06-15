using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

	private Animator anim;
	private float length;

	void Start () {
		anim = GetComponent<Animator> ();
		length =  anim.GetCurrentAnimatorStateInfo(0).length;
		StartCoroutine (WaitForAnimationEnd ());
	}	

	IEnumerator WaitForAnimationEnd(){
		yield return new WaitForSeconds (length);
		if (anim.enabled)
			anim.enabled = false;
	}
}
