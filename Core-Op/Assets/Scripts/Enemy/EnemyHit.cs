using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour {

	GameObject enginePart;
	GameObject gunPart;
	GameObject jetPart;
	GameObject player;
	engineController engineS;
	coreGunController gunS;
	jetController jetS;
	PlayerManagement playerManager;
	EnemyMovement enemymovement;
	public float damage = 20f;
	private bool isHit;

	// Use this for initialization
	void Start () {
		isHit = false;
		enginePart = GameObject.FindGameObjectWithTag ("Engine Part");
		gunPart = GameObject.FindGameObjectWithTag ("Gun Part");
		jetPart = GameObject.FindGameObjectWithTag ("Jet Part");
		player = GameObject.FindGameObjectWithTag ("Player");
		engineS = enginePart.GetComponent<engineController> ();
		gunS = gunPart.GetComponent<coreGunController> ();
		jetS = jetPart.GetComponent<jetController> ();
		enemymovement = GetComponent<EnemyMovement> ();
		playerManager = player.GetComponent<PlayerManagement> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
//		Debug.Log (isHit);
//		Debug.Log("Engine HP: " + engineS.currentHp);
		if(!isHit){
			Debug.Log (coll.gameObject.tag);
			if (coll.gameObject.tag == "Core") {
				if (engineS.currentHp > 0) {
					engineS.TakeDamage (damage);
				}
				if(gunS.currentHp > 0){
					gunS.TakeDamage (damage);
				}
				if(jetS.currentHp > 0){
					jetS.TakeDamage (damage);
				}
			}
			else if(coll.gameObject.tag == "Engine Part"){
				if (engineS.currentHp > 0) {
					engineS.TakeDamage (damage);
//					Debug.Log ("OKKK");
				}
			}
			else if(coll.gameObject.tag == "Gun Part"){
				if (gunS.currentHp > 0) {
					gunS.TakeDamage (damage);
				}
			}
			else if(coll.gameObject.tag == "Jet Part"){
				if (jetS.currentHp > 0) {
					jetS.TakeDamage (damage);
				}
			} else if(coll.gameObject.tag == "Player"){
				playerManager.StartTimeCounter (true, 3f);
			}
			Destroy (gameObject, 0.1f);
			isHit = true;
			enemymovement.ResetCD ();
			enemymovement.enabled = false;

		}
	}
}
