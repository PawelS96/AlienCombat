using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerHealth : MonoBehaviour {
	
	[SerializeField] private int health;
	[SerializeField] private TextMeshProUGUI displayHealth;

	private int currentHealth;

	void Start () {
		currentHealth = health;
		UpdateHealthDisplay ();		
	}

	public void TakeDamage(int damage){
		currentHealth -= damage;

		if (currentHealth <= 0) {
			currentHealth = 0;
			GameController.SharedInstance.Explode (gameObject);
			GameController.SharedInstance.SaveScore ();
			GameController.SharedInstance.ShowGameOverMenu ();
		}
		UpdateHealthDisplay ();
	}

	public void Die(){
		currentHealth = 0;
		UpdateHealthDisplay ();
		GameController.SharedInstance.Explode (gameObject);
		GameController.SharedInstance.SaveScore ();
		GameController.SharedInstance.ShowGameOverMenu ();
	}

	public void Heal(){
		currentHealth = health;
		UpdateHealthDisplay ();
	}

	private void UpdateHealthDisplay(){
		displayHealth.text = currentHealth.ToString ();
	}
}
