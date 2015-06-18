using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadScript : MonoBehaviour {

	void Awake () {
		DontDestroyOnLoad(gameObject);
		
		int count = 0;
		GameObject[] objects = GameObject.FindGameObjectsWithTag("Dont Destroy");
		foreach(GameObject _object in objects) {
			if (_object.name.Equals(gameObject.name))
				count++;
		}
		if (count > 1) {
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
