using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public TextMesh timer;
	public float timePassed;
	public int time;
	
	public GameObject bullet;
	public float bulletForce = 10;
	
	public GameObject player;
	public int killerID;
	
	private float second = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CountdownTImer();
	}
	
	void CountdownTImer() {
		second += Time.deltaTime;
		if (second >= timePassed) {
			second = 0;
			time--;
			timer.text = time + "";
		}
		
		if (time <= 0) {
			ShootEm();
			Destroy(gameObject);
		}
	}
	
	public void AddBlastForce(Vector3 expPosition, float expRadius, float expForce) {
		var dir = (gameObject.transform.position - expPosition);
		float calc = 1 - (dir.magnitude / expRadius);
		if (calc <= 0) {
			calc = 0;
		}
		if (calc == 0) return;
		GetComponent<Rigidbody2D>().AddForce (dir.normalized * expForce * calc);
	}
	
	void ShootEm() {
		GameObject left = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		GameObject right = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		GameObject up = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		GameObject down = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		
		left.GetComponent<BulletScript>().bulletForceHorizontal = -bulletForce;
		right.GetComponent<BulletScript>().bulletForceHorizontal = bulletForce;
		up.GetComponent<BulletScript>().bulletForceVertical = bulletForce;
		down.GetComponent<BulletScript>().bulletForceVertical = -bulletForce;
		
		left.GetComponent<BulletScript>().photonPlayerID = killerID;
		right.GetComponent<BulletScript>().photonPlayerID = killerID;
		up.GetComponent<BulletScript>().photonPlayerID = killerID;
		down.GetComponent<BulletScript>().photonPlayerID = killerID;
		
		player.GetComponent<PlayerAttributeScript>().AddBomb(1);
	}
}
