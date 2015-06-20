using UnityEngine;
using System.Collections;

public class DestroyWhenOnAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void DestroyNow() {	
		Destroy(gameObject);
	}
}
