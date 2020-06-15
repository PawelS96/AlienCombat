using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	[SerializeField] private float speed;
	[SerializeField] private int damage;

	private Rigidbody2D rb;

	void Awake(){
		rb = transform.GetComponent<Rigidbody2D>();
	}

	void OnEnable () {
		rb.velocity = transform.up * speed;
	}



	void OnTriggerEnter2D(Collider2D other){
		
		 if (gameObject.tag == "Enemy Bullet") {
			if (other.gameObject.tag == "Player") {
				other.gameObject.GetComponent<PlayerHealth> ().TakeDamage (damage);
				GameController.SharedInstance.Explode (gameObject);
			} else if (other.gameObject.tag == "ShieldEffect") {
				GameController.SharedInstance.Explode (gameObject);
			}
		} else if (gameObject.tag == "Player Bullet") {
			if (other.gameObject.tag == "Enemy") {
				other.gameObject.GetComponent<EnemyHealth> ().TakeDamage (damage);
				GameController.SharedInstance.Explode (gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Bound")
			gameObject.SetActive (false);
	}


}
