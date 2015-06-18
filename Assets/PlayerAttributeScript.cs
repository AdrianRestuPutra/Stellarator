using UnityEngine;
using System.Collections;

public class PlayerAttributeScript : Photon.MonoBehaviour {

	public GameObject bomb;
	
	// EFFECT PREFAB
	public GameObject blast;
	public GameObject portalIn;
	public GameObject portalOut;
	
	private bool gameOver = false;
	private ExitGames.Client.Photon.Hashtable roomHash;
	private int bombMax = 1;
	private OnlineMazeGenerator onlineMazeGenerator;

	// Use this for initialization
	void Start () {
		onlineMazeGenerator = GameObject.Find("Online Gameplay Manager").GetComponent<OnlineMazeGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine) return;
		
		if (Input.GetKeyDown(KeyCode.X))
			Portal(transform.position, onlineMazeGenerator.GetRandomEmptyPlace());
	}
	
	[RPC]
	public void Blast(Vector3 position) {
		Instantiate(blast, position, Quaternion.identity);
		
		if (photonView.isMine)
			PhotonNetwork.RPC(photonView, "Blast", PhotonTargets.Others, true, position);
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
	public void DropBomb(Vector3 position) {
		if (bombMax > 0) {
			GameObject _bomb = Instantiate(bomb, position, Quaternion.identity) as GameObject;
			_bomb.GetComponent<BombScript>().player = gameObject;
			bombMax--;
		
			if (photonView.isMine)
				PhotonNetwork.RPC(photonView, "DropBomb", PhotonTargets.Others, true, position);
		}
	}
	
	public void AddBomb(int sum) {
		bombMax++;
	}
}
