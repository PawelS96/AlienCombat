using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float speed;

	private Rigidbody2D rb;
	private AudioSource[] audio;

	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
		audio = GetComponents<AudioSource> ();
	}

	void FixedUpdate(){
		float moveH = Input.GetAxis("Horizontal");
		float moveV = Input.GetAxis("Vertical");
		Vector2 movement = new Vector2 (moveH, moveV);
		rb.AddForce (movement*speed);
	}

	void OnTriggerEnter2D(Collider2D other){

		switch (other.gameObject.tag) {

			case "Enemy":
				gameObject.GetComponent<PlayerHealth> ().Die ();
				break;

		case "Item":
			
			switch (other.gameObject.name) {

			case "Upgrade(Clone)":
				float weaponDuration = other.gameObject.GetComponent<ItemStats> ().GetDuration ();
				gameObject.GetComponent<PlayerCombat> ().UpgradeWeapon (weaponDuration);
				break;

			case "Repair(Clone)":
				gameObject.GetComponent<PlayerHealth> ().Heal ();
				break;

			case "Coin(Clone)":
				int score = other.gameObject.GetComponent<ItemStats> ().GetPointReward ();
				GameController.SharedInstance.AddScore (score);
				break;

			case "Shield(Clone)":
				float shieldDuration = other.gameObject.GetComponent<ItemStats> ().GetDuration ();
				gameObject.GetComponent<PlayerCombat> ().EnableShield (shieldDuration);
				break;
			}

			other.gameObject.SetActive (false);
			audio[0].Play ();
			break;

		}
	}
}


