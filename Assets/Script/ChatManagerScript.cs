using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatManagerScript : MonoBehaviour {

	public Text[] listChatText;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void AddChat(string chat) {
		for(int i=listChatText.Length-1;i>=1;i--)
			listChatText[i].text = listChatText[i-1].text;
		listChatText[0].text = chat;
	}
}
