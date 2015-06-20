using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float bulletForceHorizontal = 0;
	public float bulletForceVertical = 0;
	
	public int photonPlayerID;
	
	private MainCameraGameplayScript mainCameraGameplay;

	void Awake () {
		mainCameraGameplay = GameObject.Find("Main Camera").GetComponent<MainCameraGameplayScript>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(bulletForceHorizontal, bulletForceVertical);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Wall") {
			mainCameraGameplay.Shake();
			Destroy(gameObject);
		} else if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerControllerManagerScript>().Dead(photonPlayerID);
		}
	}
}
