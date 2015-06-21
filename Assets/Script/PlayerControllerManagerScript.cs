using UnityEngine;
using System.Collections;

public class PlayerControllerManagerScript : Photon.MonoBehaviour {

	public float moveForce = 5, jetPackForce = 5;
	public float jetPackMax = 1;
	public PlayerAttributeScript playerAttribute;
	public int playerID = -1;
	
	private Rigidbody2D rgBody2D;
	// DIRECTION
	private bool left = false, right = false, jetPack = false;
	private bool isFacingRight = false;
	
	private OnlineGameplayManager onlineGameplayManager;
	
	private ExitGames.Client.Photon.Hashtable playerHash;
	
	private float cantMoveSecond = 0;

	void Awake () {
		playerHash = new ExitGames.Client.Photon.Hashtable();
		if (photonView.isMine)
			playerID = PhotonNetwork.player.ID;
		onlineGameplayManager = GameObject.Find("Online Gameplay Manager").GetComponent<OnlineGameplayManager>();
	}

	// Use this for initialization
	void Start () {
		rgBody2D = GetComponent<Rigidbody2D>();
		if (photonView.isMine == false)
			Destroy(rgBody2D);
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine) SyncedMovement();
		else {
			if (cantMoveSecond <= 0) {
				cantMoveSecond = 0;
				GetInput();
			}
			cantMoveSecond -= Time.deltaTime;
		}
	}
	
	// FixedUpdate is called in constant time
	void FixedUpdate() {
		if (photonView.isMine == false) return;
		ApplyPhysics();
	}
	
	/*
		SYNC PART
	*/
	private Vector3 endPosition;
	
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext(transform.position);
		} else {
			syncEndPosition = (Vector3)stream.ReceiveNext();
			syncStartPosition = transform.position;
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
		}
	}
	
	private void SyncedMovement() {
		syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}
	
	/*
		END OF SYNC PART
	*/
	
	void GetInput() {
		left = Input.GetKey(KeyCode.LeftArrow);
		right = Input.GetKey(KeyCode.RightArrow);
		jetPack = Input.GetKey(KeyCode.UpArrow);
		
		if (Input.GetKeyDown(KeyCode.Z))
			playerAttribute.DropBomb(transform.position, playerID);
	}
	
	void ApplyPhysics() {
		if (cantMoveSecond > 0) return;
		if (left) {
			if (isFacingRight) Flip();
			rgBody2D.velocity = new Vector2(-moveForce, rgBody2D.velocity.y);
		} else if (right) {
			if (!isFacingRight) Flip();
			rgBody2D.velocity = new Vector2(moveForce, rgBody2D.velocity.y);
		} else rgBody2D.velocity = new Vector2(0, rgBody2D.velocity.y);
		
		if (jetPack) rgBody2D.AddForce(new Vector2(0, jetPackForce));
		
		rgBody2D.velocity = new Vector2(rgBody2D.velocity.x, Mathf.Min(rgBody2D.velocity.y, jetPackMax));
	}
	
	public void AddBlastForce(Vector3 expPosition, float expRadius, float expForce) {
		if (!photonView.isMine) return;
		var dir = (gameObject.transform.position - expPosition);
		float calc = 1 - (dir.magnitude / expRadius);
		if (calc <= 0) {
			calc = 0;
		}
		if (calc == 0) return;
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		GetComponent<Rigidbody2D>().AddForce (dir.normalized * expForce * calc);
		cantMoveSecond = 0.5f;
	}
	
	void OnTriggerEnter2D(Collider2D other) {	
		if (!photonView.isMine) return;
		if (other.gameObject.tag.Equals("Ice")) {
			if (other.gameObject.GetComponent<IceEffectScript>().playerOwnerID != playerID) {
				moveForce /= 1.5f;
				jetPackForce /= 2f;
				rgBody2D.gravityScale /= 2f;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (!photonView.isMine) return;
		if (other.gameObject.tag.Equals("Ice")) {
			if (other.gameObject.GetComponent<IceEffectScript>().playerOwnerID != playerID) {
				moveForce *= 1.5f;
				jetPackForce *= 2f;
				rgBody2D.gravityScale *= 2f;
			}
		}
	}
	
	[RPC]
	void Flip() {
		isFacingRight ^= true;
	
		Vector3 vec = transform.localScale;
		vec.x *= -1;
		transform.localScale = vec;
		
		if (photonView.isMine)
			PhotonNetwork.RPC(photonView, "Flip", PhotonTargets.Others, true);
	}
	
	[RPC]
	public void Dead(int photonPlayerIdKiller) {
		if (photonView.isMine) {
			if (playerAttribute.isInvulnerable == false) {
				if (playerID != photonPlayerIdKiller) {
					PhotonPlayer player = PhotonPlayer.Find(photonPlayerIdKiller);
					player.AddScore(1);
				}
				transform.position = onlineGameplayManager.onlineMazeGenerator
					.GetSpawnPosition(onlineGameplayManager.playerPosition);
			}
		}
	}
}
