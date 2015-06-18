using UnityEngine;
using System.Collections;

public class PreviewCharacterScript : MonoBehaviour {

	public Sprite emptyImage;
	public Sprite[] imagesPreview;
	public SpriteRenderer[] listPreview;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		PhotonPlayer[] players = PhotonNetwork.playerList;
		for(int i=0;i<players.Length;i++) {
			int index = 0;
			if (players[i].customProperties.ContainsKey("Character Id"))
				index = (int)players[i].customProperties["Character Id"];
			listPreview[i].sprite = imagesPreview[index];
		}
		for(int i=players.Length;i<listPreview.Length;i++) {
			listPreview[i].sprite = emptyImage;
		}
	}
}
