using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour {

	[SerializeField] private int pointReward;
	[SerializeField] private float duration;

	public int GetPointReward(){
		return pointReward;
	}

	public float GetDuration(){
		return duration;
	}

}
