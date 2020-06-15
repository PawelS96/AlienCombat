using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchMenuPanels : MonoBehaviour {

	private Button button;

	[SerializeField] private GameObject panelToHide;
	[SerializeField] private GameObject panelToShow;

	void Awake(){
		button = GetComponent<Button> ();
		button.onClick.AddListener (Click);
	}

	void Click(){	

		PlayButtonSound.SharedInstance.PlaySound();
		panelToHide.SetActive (false);
		panelToShow.SetActive (true);
	}
}
