using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour {

	[SerializeField] private float minGravity;
	[SerializeField] private float maxGravity;

	private Rigidbody2D rb;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();				
	}

	void OnEnable(){

		float xMin = GameController.SharedInstance.leftBoundary.transform.position.x + 0.5f;
		float xMax = GameController.SharedInstance.rightBoundary.transform.position.x - 0.5f;
		float horizontalPos = Random.Range (xMin, xMax);
		float verticalPos = GameController.SharedInstance.topBoundary.transform.position.y + 2;

		rb.gravityScale = Random.Range (minGravity, maxGravity);

		transform.position = new Vector2 (horizontalPos, verticalPos);	

	}


	void OnTriggerExit2D(Collider2D other){

		if (other.gameObject.tag == "Bound") {
			if (other.gameObject.name != "Top")
				gameObject.SetActive (false);
		}			
	}

}
