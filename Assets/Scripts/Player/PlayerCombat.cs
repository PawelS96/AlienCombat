using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

	[SerializeField] private float shotDelay;

	private enum Weapon {
		Standard, Upgraded
	};

	private Weapon currentWeapon;
	private float lastShot = 0.0f;

	private AudioSource[] audio;

	void Start () {
		audio = GetComponents<AudioSource> ();
		currentWeapon = Weapon.Standard;		
	}

	void Update () {

		if (GameController.SharedInstance.GetState() == GameController.gameState.playing) {

			if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) {

				switch (currentWeapon) {

				case Weapon.Standard:
					ShootStandard ();
					break;

				case Weapon.Upgraded:
					ShootUpgraded ();
					break;
				}
			}
		}
	}

	private void ShootStandard(){

		if (Time.time > shotDelay + lastShot) {
			GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("PlayerBullet(Clone)");
			if (bullet != null) {
				bullet.transform.position = transform.position + new Vector3(0, 0.7f, 0);
				bullet.transform.rotation = transform.rotation;
				bullet.SetActive (true);
				audio[1].Play ();
				lastShot = Time.time;
			} 
		}
	}

	private void ShootUpgraded(){

		if (Time.time > shotDelay + lastShot) {
			List<GameObject> bullets = ObjectPooler.SharedInstance.GetMultiplePooledObjects ("PlayerBullet(Clone)", 3);

			if(bullets[0] != null && bullets[1] != null && bullets[2] != null){

				bullets [0].transform.position = transform.position + new Vector3 (-0.5f, 0, 0);
				bullets [1].transform.position = transform.position + new Vector3 (0, 0.7f, 0);
				bullets [2].transform.position = transform.position + new Vector3 (0.5f, 0, 0);

				for (int i = 0; i < 3; i++) {
					bullets [i].transform.rotation = transform.rotation;
					bullets [i].SetActive (true);					
				}

				audio[1].Play ();
				lastShot = Time.time;
			}
		}
	}

	private IEnumerator WaitForWeaponChange(float duration){		
		yield return new WaitForSeconds (duration);
		currentWeapon = Weapon.Standard;
	}

	private IEnumerator WaitForShieldDisable(float duration){		
		yield return new WaitForSeconds (duration);
		transform.GetChild (0).gameObject.SetActive (false);
	}

	public void UpgradeWeapon(float duration){
		currentWeapon = Weapon.Upgraded;
		StartCoroutine (WaitForWeaponChange(duration));
	}

	public void EnableShield(float duration){
		transform.GetChild (0).gameObject.SetActive (true);
		StartCoroutine (WaitForShieldDisable (duration));
	}
}
