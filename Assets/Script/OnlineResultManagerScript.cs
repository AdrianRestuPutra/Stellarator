using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnlineResultManagerScript : MonoBehaviour {

	public Text[] resultString;

	// Use this for initialization
	void Start () {
		PhotonPlayer[] players = PhotonNetwork.playerList;
		for(int i=0;i<players.Length;i++) {
			resultString[i].text = players[i].name + " " + players[i].GetScore();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			if (Input.GetKeyDown(KeyCode.Return))
				PhotonNetwork.LoadLevel("Online Room Scene");
		}
	}
}
