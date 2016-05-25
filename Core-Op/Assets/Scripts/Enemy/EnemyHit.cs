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

	void InitGameObjects ()
	{
		enginePart = GameObject.FindGameObjectWithTag ("Engine Part");
		gunPart = GameObject.FindGameObjectWithTag ("Gun Part");
		jetPart = GameObject.FindGameObjectWithTag ("Jet Part");
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void InitScripts ()
	{
		engineS = enginePart.GetComponent<engineController> ();
		gunS = gunPart.GetComponent<coreGunController> ();
		jetS = jetPart.GetComponent<jetController> ();
		enemymovement = GetComponent<EnemyMovement> ();
		playerManager = player.GetComponent<PlayerManagement> ();
	}

	// Use this for initialization
	void Start () {
		isHit = false;
		InitGameObjects ();
		InitScripts ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DestroyEnemy ()
	{
		Destroy (gameObject, 0.1f);
	}


	void OnCollisionEnter2D(Collision2D coll) {
		if(!isHit){
			Debug.Log (coll.gameObject.tag);
			switch (coll.gameObject.tag) {
			case "Core":
				if (engineS.currentHp > 0)
					engineS.TakeDamage (damage);
				if (gunS.currentHp > 0)
					gunS.TakeDamage (damage);
				if (jetS.currentHp > 0)
					jetS.TakeDamage (damage);
				break;
			case "Engine Part":
				if (engineS.currentHp > 0)
					engineS.TakeDamage (damage);
				break;
			case "Gun Part":
				if (gunS.currentHp > 0)
					gunS.TakeDamage (damage);
				break;
			case "Jet Part":
				if (jetS.currentHp > 0)
					jetS.TakeDamage (damage);
				break;
			case "Player":
				playerManager.StartCounter (true, 3f);
				break;
			}
			DestroyEnemy ();
			isHit = true;
			enemymovement.ResetCD ();
			enemymovement.enabled = false;

		}
	}
}
