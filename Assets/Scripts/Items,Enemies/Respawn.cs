using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	[SerializeField] private float minSpawnTime;
	[SerializeField] private float maxSpawnTime;

	public float GetMinSpawnTime(){
		return minSpawnTime;
	}

	public float GetMaxSpawnTime(){
		return maxSpawnTime;
	}
}
