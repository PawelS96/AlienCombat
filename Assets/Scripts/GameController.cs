using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

	[Header("UI")]
	[SerializeField] private TextMeshProUGUI currentScore;
	[SerializeField] private TextMeshProUGUI finalScore;
	[SerializeField] private TextMeshProUGUI bestScore;

	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject gameOverMenu;

	[Header("Objects")]
	[SerializeField] private List<GameObject> items;
	[SerializeField] private List<GameObject> enemies;

	[Header("Boundaries")]
	public GameObject leftBoundary;                   
	public GameObject rightBoundary;                  
	public GameObject topBoundary;                   
	public GameObject bottomBoundary;

	public static GameController SharedInstance;

	public enum gameState {
		paused, playing, over
	}

	private gameState state;

	private bool paused;
	private int score;

	void Awake(){
		SharedInstance = this;
	}

	void Start(){	
		currentScore.text = score.ToString ();
		state = gameState.playing;
		SpawnItems ();
		SpawnEnemies ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene ("GameScene");		
		if (Input.GetKeyDown (KeyCode.Escape) && state != gameState.over)
			Pause ();				
	}

	public gameState GetState(){
		return (gameState)state;
	}

	public void Pause(){
		if (state == gameState.paused) {
			Time.timeScale = 1;
			pauseMenu.SetActive (false);
			state = gameState.playing;
		} else if (state == gameState.playing) {
			Time.timeScale = 0;
			pauseMenu.SetActive (true);
			state = gameState.paused;
		}
	}

	public void ShowGameOverMenu(){
		finalScore.text = "Your score: " + score.ToString ();
		bestScore.text = "Best score: " + PlayerPrefs.GetInt ("Score1").ToString ();
		gameOverMenu.SetActive (true);
		state = gameState.over;
	}

	public void AddScore(int scoreToAdd){
		score += scoreToAdd;
		currentScore.text = score.ToString ();
	}

	public int GetScore(){
		return score;
	}

	public void SaveScore(){
		
		int scoreToSave = score;

		for (int i = 1; i < 11; i++) {
			int savedScore = PlayerPrefs.GetInt ("Score" + i.ToString ());
			if (scoreToSave > savedScore) {
				for (int j = i; j < 10; j++) {
					int temp = PlayerPrefs.GetInt ("Score" + (j+1).ToString ());
					PlayerPrefs.SetInt ("Score" + (j+1).ToString (), savedScore);
					savedScore = temp;
				}
				PlayerPrefs.SetInt ("Score" + i.ToString (), scoreToSave);
				break;
			} else
				continue;
		}
	}

	private void SpawnItems(){
		foreach (GameObject obj in items) {
			StartCoroutine (SpawnObject (obj, obj.GetComponent<Respawn> ().GetMinSpawnTime (), obj.GetComponent<Respawn> ().GetMaxSpawnTime ()));
		}
	}

	private void SpawnEnemies(){
		foreach (GameObject obj in enemies) {
			StartCoroutine (SpawnObject (obj, obj.GetComponent<Respawn> ().GetMinSpawnTime (), obj.GetComponent<Respawn> ().GetMaxSpawnTime ()));
		}
	}

	private IEnumerator SpawnObject(GameObject objectToSpawn, float minSpawnTime, float maxSpawnTime){

		string name = objectToSpawn.name + "(Clone)";

		while (true) {		
			float timeToSpawn = Random.Range (minSpawnTime, maxSpawnTime);		
			yield return new WaitForSeconds (timeToSpawn);
			GameObject obj = ObjectPooler.SharedInstance.GetPooledObject (name);
			obj.SetActive (true);
		}
	}

	public void Explode(GameObject obj){

		GameObject expl;

		if(obj.name.Contains("BigEnemy"))
		expl = ObjectPooler.SharedInstance.GetPooledObject ("ExplosionBig(Clone)");

		else if(obj.name.Contains("Bullet"))
		expl = ObjectPooler.SharedInstance.GetPooledObject ("ExplosionSmall(Clone)");
		
		else
		expl = ObjectPooler.SharedInstance.GetPooledObject ("Explosion(Clone)");
				
		expl.transform.position = obj.transform.position;
		expl.SetActive (true);
		obj.SetActive (false);
	}


}
