using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnClick (string name) {
		if (name.Equals("Online Multiplayer"))
			Application.LoadLevel("Online Lobby Scene");
	}
}
