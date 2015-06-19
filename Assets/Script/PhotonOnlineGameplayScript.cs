using UnityEngine;
using System.Collections;

public class PhotonOnlineGameplayScript : Photon.MonoBehaviour {
	
	private Hashtable hashScore;
	
	void Awake () {
		hashScore = new Hashtable();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
