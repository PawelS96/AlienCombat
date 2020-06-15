using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScores : MonoBehaviour {

	private TextMeshProUGUI[] scoreTexts;

	public static DisplayScores SharedInstance;

	void Awake(){		
		SharedInstance = this;
		scoreTexts = gameObject.GetComponentsInChildren<TextMeshProUGUI> ();
	}

	void Start () {		
		InitScores ();
		Display ();
	}

	public void ResetScores(){
		for (int i = 1; i < 11; i++) {		
			PlayerPrefs.SetInt ("Score" + i.ToString (), 0);
		}
		Display ();
	}

	private void Display(){

		int k =1;

		foreach (TextMeshProUGUI scoreText in scoreTexts) {
			int score = PlayerPrefs.GetInt ("Score" + k.ToString());
			scoreText.text = score.ToString ();
			k++;
		}
	}

	private void InitScores(){
		
		if (PlayerPrefs.GetInt ("FirstLaunch") != 0) {			
			ResetScores ();
			PlayerPrefs.SetInt ("FirstLaunch", 0);
		}
	}
}
