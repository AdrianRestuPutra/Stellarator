using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnlineLobbyManagerScript : MonoBehaviour {

	public GameObject loading;
	public Text roomText;
	public Text totalRoom, totalPlayer;
	
	private ExitGames.Client.Photon.Hashtable hashtable;

	// Use this for initialization
	void Start () {
		if (PhotonNetwork.connected == false) {
			PhotonNetwork.autoJoinLobby = true;
			PhotonNetwork.player.name = "Madya121";
			if (!PhotonNetwork.connected)
				PhotonNetwork.ConnectToMaster("192.168.1.163", 5055, "Stellarator", "0.1");
			//PhotonNetwork.ConnectUsingSettings("0.1");
		}
		if (PhotonNetwork.insideLobby == true)
			Destroy(loading);
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
	}
	
	void FixedUpdate () {
		if (PhotonNetwork.insideLobby == true) {
			totalRoom.text = "Total Room : " + PhotonNetwork.countOfRooms;
			totalPlayer.text = "Total Player : " + PhotonNetwork.countOfPlayers;
		}
	}
	
	void GetInput () {
		if (PhotonNetwork.insideLobby == false || PhotonNetwork.connected == false) return;
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) {
			PhotonNetwork.Disconnect();
		}
	}
	
	public void CreateRoom () {
		if (roomText.text.Trim().Equals(string.Empty) == false) {
			RoomOptions option = new RoomOptions();
			option.maxPlayers = 4;
			hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable.Add("Character Id", 0);
			PhotonNetwork.player.SetCustomProperties(hashtable);
			PhotonNetwork.CreateRoom(roomText.text);
		}
	}
	
	public void AutoJoin () {
		hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable.Add("Character Id", 0);
		PhotonNetwork.player.SetCustomProperties(hashtable);
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnJoinedLobby() {
		Destroy(loading);
	}
	
	public void OnJoinedRoom() {
		Application.LoadLevel("Online Room Scene");
	}
	
	public void OnDisconnectedFromPhoton() {
		Application.LoadLevel("Main Menu Scene");
	}
}
