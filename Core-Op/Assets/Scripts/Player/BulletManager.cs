using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {
	public float speed = 5f;
	Rigidbody2D bulletRigidbody;
	PlayerController playerCon;
	EnemyHit enemyHitScript;
	GameObject player;
	GameObject enemy;
	Vector3 MouseVec;
	Vector2 playerPos;
	// Use this for initialization
	void Start () {
		bulletRigidbody = GetComponent<Rigidbody2D> ();
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		player = GameObject.FindGameObjectWithTag ("Player");
		playerCon = player.GetComponent<PlayerController> ();
		playerPos = player.GetComponent<Transform> ().position;
		MouseVec = playerCon.getMouseVec ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 temp;
//		playerPos = player.GetComponent<Transform> ().position;
//		MouseVec = playerCon.getMouseVec ();
		fire ();
		Destroy (gameObject, 3f);
	}

	Vector2 getDirection(){
		Vector2 temp;
		temp.x = MouseVec.x;
		temp.y = MouseVec.y;
		Vector2 direction = temp - playerPos;
		direction = direction.normalized;
		return direction;
	}

	void fire(){
		Vector2 temp;
		Vector2 dir;
		dir = getDirection();
		temp.x = dir.x * speed * 10f;
		temp.y = dir.y * speed * 10f;
		bulletRigidbody.velocity = temp;
	}

	void OnCollisionEnter2D(Collision2D coll) {
//		Debug.Log (coll.gameObject.tag);
		switch (coll.gameObject.tag) {
		case "Enemy":
			enemyHitScript = enemy.GetComponent<EnemyHit> ();
			enemyHitScript.DestroyEnemy ();
			break;
		}
		Destroy (gameObject);
	}

}
