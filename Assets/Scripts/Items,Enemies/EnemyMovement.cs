using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] private float speed;

	private GameObject leftBoundary;                   
	private GameObject rightBoundary;                 
	private GameObject topBoundary;                    
	private GameObject bottomBoundary;   

	private float randX, randY;
	private Vector2 movement;

	void Awake () {

		leftBoundary = GameController.SharedInstance.leftBoundary;
		rightBoundary = GameController.SharedInstance.rightBoundary;
		topBoundary = GameController.SharedInstance.topBoundary;
		bottomBoundary = GameController.SharedInstance.bottomBoundary;
	}
	
	void FixedUpdate () {

		transform.position = Vector2.MoveTowards (transform.position, movement, speed * Time.deltaTime);

		if ((Vector2)transform.position == movement){
			randomizeVector ();
			transform.position = Vector2.MoveTowards (transform.position, movement, speed * Time.deltaTime);
		}
	}

	void OnEnable(){

		float xMin = GameController.SharedInstance.leftBoundary.transform.position.x + 0.5f;
		float xMax = GameController.SharedInstance.rightBoundary.transform.position.x - 0.5f;
		float horizontalPos = Random.Range (xMin, xMax);
		float verticalPos = GameController.SharedInstance.topBoundary.transform.position.y + 2;

		transform.position = new Vector2 (horizontalPos, verticalPos);	
		randomizeVector ();
	}

	private void randomizeVector(){
		randX = Random.Range (leftBoundary.transform.position.x, rightBoundary.transform.position.x);
		randY = Random.Range ((bottomBoundary.transform.position.y + topBoundary.transform.position.y ) / 2, topBoundary.transform.position.y);
		movement = new Vector2 (randX, randY);
	}
}
