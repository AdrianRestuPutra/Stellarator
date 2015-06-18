using UnityEngine;
using System.Collections;

public class MouseBackgroundFollowerScript : MonoBehaviour {

	public Camera kamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		Vector3 pos = kamera.ScreenToWorldPoint(Input.mousePosition);
		position.x = pos.x / 50;
		position.y = pos.y / 50;
		
		transform.position = position;
	}
}
