using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnlineGameplayManager : MonoBehaviour {

	public int playerPosition = 1;
	public OnlineMazeGenerator onlineMazeGenerator;
	public Text timerText;
	
	public int timer = 180;

	PhotonPlayer[] players;
	int playerID;
	ExitGames.Client.Photon.Hashtable hashtable;
	float second = 0;

	void Awake () {
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.sendRate = 20;
		PhotonNetwork.sendRateOnSerialize = 20;
	
		players = PhotonNetwork.playerList;
		playerID = PhotonNetwork.player.ID;
		
		foreach(PhotonPlayer player in players) {
			// INITIALIZE
			if (player.ID < playerID)
				playerPosition++;
		}
		
		timerText.text = timer + "";
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		second += Time.deltaTime;
		if (second >= 1) {
			timer--;
			if (timer < 0) timer = 0;
			timerText.text = timer + "";
			second = 0;
		}
		
		if (timer <= 0) {
			if (PhotonNetwork.isMasterClient) {
				PhotonNetwork.DestroyAll();
				PhotonNetwork.LoadLevel("Online Room Scene");
			}
		}
		
		if (PhotonNetwork.playerList.Length == 1) {
			PhotonNetwork.LeaveRoom();
		}
	}
	
	void OnLeftRoom() {
		Application.LoadLevel("Online Lobby Scene");
	}
	
	void FixedUpdate () {
		
	}
}
