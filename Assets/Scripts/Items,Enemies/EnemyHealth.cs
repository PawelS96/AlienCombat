using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	[SerializeField] private int health;
	[SerializeField] private int killReward;
	//[SerializeField] private bool shouldExplode;

	[SerializeField] private bool showFlamesAtLowHP;

	private GameObject flames;

	private int currentHealth;

	void Awake(){
		flames = transform.GetChild (0).gameObject;
	}

	void OnEnable(){
		currentHealth = health;
	}

	public void TakeDamage(int damage){
		currentHealth -= damage;

		if(currentHealth == 1 && showFlamesAtLowHP)
		flames.SetActive (true);

		if (currentHealth <= 0) {
			GameController.SharedInstance.Explode (gameObject);

			if (showFlamesAtLowHP)
				flames.SetActive (false);

			GameController.SharedInstance.AddScore (killReward);
		}
	}
}
