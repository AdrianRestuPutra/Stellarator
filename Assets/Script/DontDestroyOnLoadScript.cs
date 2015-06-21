using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadScript : MonoBehaviour {

	public string TAG;

	void Awake () {
		DontDestroyOnLoad(gameObject);
		
		int count = 0;
		GameObject[] objects = GameObject.FindGameObjectsWithTag(TAG);
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
