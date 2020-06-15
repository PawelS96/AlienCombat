using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {

	[SerializeField] private float shotDelay;
	[SerializeField] private int bulletsFired;

	private Transform target;
	private float lastShot = 0.0f;
	private AudioSource audio;

	void Start(){
		audio = GetComponent<AudioSource> ();

		var temp = GameObject.FindGameObjectWithTag ("Player");

		if (temp != null)
			target = temp.transform;
	}

	void Update(){

		if (target != null && GameController.SharedInstance.GetState() == GameController.gameState.playing) {		

			if (Mathf.Clamp (transform.position.x, target.position.x - 1.0f, target.position.x + 1.0f) == transform.position.x) {
				if (bulletsFired == 1)
					ShootSingle ();
				else if (bulletsFired >= 1)
					ShootMultiple (bulletsFired);
			}
		}
	}

	void ShootSingle(){

			if (Time.time > shotDelay + lastShot) {
				
				GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject ("EnemyBullet(Clone)");
				if (bullet != null) {

					Vector3 rot = transform.rotation.eulerAngles;
					rot = new Vector3 (rot.x, rot.y, rot.z + 180);

					bullet.transform.position = transform.position;
					bullet.transform.rotation = Quaternion.Euler (rot);
					bullet.SetActive (true);
					audio.Play ();
					lastShot = Time.time;
				}
			} 
	}

	private void ShootMultiple(int amountOfBullets){

		int angle = 180 - (amountOfBullets * 10 - 10);
		Vector3 rot;

			if (Time.time > shotDelay + lastShot) {
				List<GameObject> bullets = ObjectPooler.SharedInstance.GetMultiplePooledObjects ("EnemyBullet(Clone)", amountOfBullets);

				if (bullets != null) {

					for (int i = 0; i < amountOfBullets; i++) {

						rot = transform.rotation.eulerAngles;
						rot = new Vector3 (rot.x, rot.y, rot.z + angle);

						bullets [i].transform.rotation = Quaternion.Euler (rot);				
						bullets [i].transform.position = transform.position + new Vector3 (0, -1.5f, 0);
						bullets [i].SetActive (true);	

						angle += 20;
					}
					audio.Play ();
					lastShot = Time.time;
				}
			}
	}

}
