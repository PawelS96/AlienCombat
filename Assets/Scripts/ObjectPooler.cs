using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPoolItem {
	
	public GameObject objectToPool;
	public int amountToPool;
}

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler SharedInstance;
	public List<ObjectPoolItem> itemsToPool;
	public List<GameObject> pooledObjects;

	void Awake() {
		SharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject>();
		foreach (ObjectPoolItem item in itemsToPool) {
			for (int i = 0; i < item.amountToPool; i++) {
				GameObject obj = (GameObject)Instantiate(item.objectToPool);
				obj.SetActive(false);
				pooledObjects.Add(obj);
			}
		}
	}

	public GameObject GetPooledObject(string name) {

		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy && pooledObjects [i].name == name) {
				Debug.Log (pooledObjects [i].name);
				return pooledObjects [i];
			} 
		}
	return null;
	}

	public List<GameObject> GetMultiplePooledObjects(string name, int amount){

		List<GameObject> objects = new List<GameObject> ();

		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy && pooledObjects [i].name == name) {
				objects.Add (pooledObjects [i]); 							
			}
		}
		return objects;
	}

	// Update is called once per frame
	void Update () {

	}
}
