using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnlineRoomManagerScript : Photon.MonoBehaviour {

	public Text roomText;
	public Text chatText;
	public ChatManagerScript chatManager;
	
	private ExitGames.Client.Photon.Hashtable hashtable;
	private ExitGames.Client.Photon.Hashtable chatHash;
	private ExitGames.Client.Photon.Hashtable roomHash;

	void Awake () {
		PhotonNetwork.automaticallySyncScene = true;
	
		chatHash = new ExitGames.Client.Photon.Hashtable();
	
		hashtable = new ExitGames.Client.Photon.Hashtable();
		hashtable.Add("Character Id", 0);
		hashtable.Add("Gameplay Score", 0);
		PhotonNetwork.player.SetCustomProperties(hashtable);
		
		PhotonNetwork.OnEventCall += this.OnEventReceived;
		
		if (PhotonNetwork.isMasterClient) {
			roomHash = new ExitGames.Client.Photon.Hashtable();
			roomHash.Add("Maze Seed", (long)(Time.time * 1000));
			PhotonNetwork.room.SetCustomProperties(roomHash);
		}
	}

	// Use this for initialization
	void Start () {
		roomText.text = PhotonNetwork.room.name;
	}
	
	// Update is called once per frame
	void Update () {
		TakeInput();
	}
	
	void TakeInput () {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			ChangeCharacter(-1);
		else if (Input.GetKeyDown(KeyCode.RightArrow))
			ChangeCharacter(1);
		
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) {
			PhotonNetwork.LeaveRoom();
		}
		
		if (PhotonNetwork.isMasterClient && Input.GetKeyDown(KeyCode.Return)) {
			PhotonPlayer[] players = PhotonNetwork.playerList;
			foreach(PhotonPlayer player in players) 
				player.SetScore(0);
			PhotonNetwork.LoadLevel("Online Gameplay Scene");
		}
	}
	
	void ChangeCharacter (int number) {
		int characterId = 0;
		if (PhotonNetwork.player.customProperties.ContainsKey("Character Id"))
			characterId = (int)PhotonNetwork.player.customProperties["Character Id"];
		
		characterId = ((characterId + number) + 4) % 4;
		hashtable.Clear();
		hashtable.Add("Character Id", characterId);
		PhotonNetwork.SetPlayerCustomProperties(hashtable);
	}
	
	public void OnClickChat () {
		chatHash.Clear();
		chatHash.Add("Name", PhotonNetwork.player.name);
		chatHash.Add("Chat", chatText.text);
		PhotonNetwork.RaiseEvent((byte)0, chatHash, true, RaiseEventOptions.Default);
		
		chatManager.AddChat(chatHash["Name"] + " : " + chatHash["Chat"]);
	}
	
	public void OnEventReceived (byte eventCode, object content, int senderID) {
		chatHash.Clear();
		chatHash = (ExitGames.Client.Photon.Hashtable)content;
		chatManager.AddChat(chatHash["Name"] + " : " + chatHash["Chat"]);
	}
	
	void OnLeftRoom () {
		Application.LoadLevel("Online Lobby Scene");
	}
}
