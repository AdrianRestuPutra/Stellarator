using UnityEngine;
using System.Collections;

public class IceEffectScript : MonoBehaviour {

	public int playerOwnerID;
	
	private Vector3 position;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void SetPosition(Vector3 _position) {
		this.position = _position;
	}
	
	void SetPlayerID(int _playerOwnerID) {
		this.playerOwnerID = _playerOwnerID;
	}
	
	public void MoveIceIn() {
		transform.position = position;
	}
	
	public void DestroyIceEffect () {
		Destroy(gameObject);
	}
	
	public void MoveIce () {
		gameObject.transform.position = new Vector3(1000, 1000);
	}
}
