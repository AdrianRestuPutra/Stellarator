using UnityEngine;
using System.Collections;

public class BlastScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void BlastOther(int playerID) {
		GameObject[] bomb = GameObject.FindGameObjectsWithTag("Bomb");
		for(int i=0;i<bomb.Length;i++)
			bomb[i].GetComponent<BombScript>().AddBlastForce(transform.position, 10, 1000);
		
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject player in players) {
			if (player.GetComponent<PlayerControllerManagerScript>().playerID != playerID) {
				player.GetComponent<PlayerControllerManagerScript>().AddBlastForce(transform.position, 10, 5000);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
