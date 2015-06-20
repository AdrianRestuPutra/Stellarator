﻿using UnityEngine;
using System.Collections;

public class PlayerAttributeScript : Photon.MonoBehaviour {
	
	public PlayerControllerManagerScript playerControllerManager;

	public GameObject bomb;
	
	// EFFECT PREFAB
	public GameObject blast;
	public GameObject portalIn;
	public GameObject portalOut;
	
	private bool gameOver = false;
	private ExitGames.Client.Photon.Hashtable roomHash;
	private int bombMax = 1;
	private OnlineMazeGenerator onlineMazeGenerator;
	
	private int playerCharacterID = -1;

	// Use this for initialization
	void Start () {
		onlineMazeGenerator = GameObject.Find("Online Gameplay Manager").GetComponent<OnlineMazeGenerator>();
		playerCharacterID = (int)PhotonNetwork.player.customProperties["Character Id"];
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine) return;
		
		if (Input.GetKeyDown(KeyCode.X)) {
			if (playerCharacterID == 2) Blast(transform.position, playerControllerManager.playerID);
			if (playerCharacterID == 3) Portal(transform.position, onlineMazeGenerator.GetRandomEmptyPlace());
			
		}
	}
	
	[RPC]
	public void Blast(Vector3 position, int playerID) {
		GameObject _blast = Instantiate(blast, position, Quaternion.identity) as GameObject;
		_blast.SendMessage("BlastOther", playerID);
		if (photonView.isMine)
			PhotonNetwork.RPC(photonView, "Blast", PhotonTargets.Others, true, position, playerID);
	}
	
	[RPC]
	public void Portal(Vector3 from, Vector3 to) {
		transform.position = to;
		Instantiate(portalIn, from, Quaternion.identity);
		Instantiate(portalOut, to, Quaternion.identity);
		
		if (photonView.isMine)
			PhotonNetwork.RPC(photonView, "Portal", PhotonTargets.Others, true, from, to);
	}
	
	[RPC]
	public void DropBomb(Vector3 position, int killerID) {
		if (bombMax > 0) {
			GameObject _bomb = Instantiate(bomb, position, Quaternion.identity) as GameObject;
			_bomb.GetComponent<BombScript>().player = gameObject;
			_bomb.GetComponent<BombScript>().killerID = killerID;
			bombMax--;
		
			if (photonView.isMine)
				PhotonNetwork.RPC(photonView, "DropBomb", PhotonTargets.Others, true, position, killerID);
		}
	}
	
	public void AddBomb(int sum) {
		bombMax++;
	}
}
