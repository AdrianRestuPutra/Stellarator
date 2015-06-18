﻿using UnityEngine;
using System.Collections;

public class MainCameraGameplayScript : MonoBehaviour {

	private Vector3 velocity = Vector3.zero;
	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		CameraTracker();
	}
	
	void CameraTracker() {
		float xMin = transform.position.x;
		float xMax = transform.position.x;
		float yMin = transform.position.y;
		float yMax = transform.position.y;
		
		GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
		
		for(int i=0;i<player.Length;i++) {
			if (player[i] == null) continue;
			
			xMin = Mathf.Min(xMin, player[i].transform.position.x);
			xMax = Mathf.Max(xMax, player[i].transform.position.x);
			
			yMin = Mathf.Min(yMin, player[i].transform.position.y);
			yMax = Mathf.Max(yMax, player[i].transform.position.y);
		}
		
		float differentY = yMax - yMin;
		float differentX = xMax - xMin;
		
		float cameraY = Mathf.Max(differentY * 0.7f, 10);
		float cameraX = Mathf.Max(differentX * 0.5f, 10);
		
		//GetComponent<Camera>().orthographicSize = Mathf.Max(cameraY, cameraX);
		mainCamera.orthographicSize = Mathf.Lerp(
			mainCamera.orthographicSize,
			Mathf.Max(cameraX, cameraY),
			3 * Time.deltaTime);
		
		float X = (xMin + xMax) / 2f;
		float Y = (yMin + yMax) / 2f;
		float Z = gameObject.transform.position.z;
		
		//gameObject.transform.position = new Vector3((xMin + xMax) / 2f, (yMin + yMax) / 2f, gameObject.transform.position.z);
		transform.position = Vector3.SmoothDamp(transform.position, new Vector3(X, Y, Z), ref velocity, 0.2f);
	}
}