using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

	private Button button;

	[SerializeField] private buttonAction action;

	private enum buttonAction
	{
		resumeGame, startNewGame, exitToMainMenu, exitGame, resetScores
	}


	void Awake(){
		button = GetComponent<Button> ();
		button.onClick.AddListener (Click);
	}

	void Click(){	
	
		PlayButtonSound.SharedInstance.PlaySound();

		switch (action) {

		case buttonAction.resumeGame:
			GameController.SharedInstance.Pause ();
			break;
		case buttonAction.startNewGame:
			SceneManager.LoadScene ("GameScene");	
			break;
		case buttonAction.exitToMainMenu:
			Time.timeScale = 1;	
			SceneManager.LoadScene ("MenuScene");
			break;
		case buttonAction.exitGame:
			Application.Quit ();
			break;
		case buttonAction.resetScores:
			DisplayScores.SharedInstance.ResetScores ();
			break;
		
		}


	}


}
