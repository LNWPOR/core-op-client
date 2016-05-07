using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour {

	public engineController enginePart;
	public coreGunController gunPart;
	public jetController jetPart;
	public EnemyMovement enemymovement;
	public float damage = 20f;
	private bool isHit;

	// Use this for initialization
	void Awake () {
		isHit = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(!isHit){
			if (coll.gameObject.tag == "Core") {
				if (enginePart.currentHp > 0) {
					enginePart.TakeDamage (damage);
				}
				else if(gunPart.currentHp > 0){
					gunPart.TakeDamage (damage);
				}
				else if(jetPart.currentHp > 0){
					jetPart.TakeDamage (damage);
				}
			}
			else if(coll.gameObject.tag == "Engine Part"){
				if (enginePart.currentHp > 0) {
					enginePart.TakeDamage (damage);
				}
			}
			else if(coll.gameObject.tag == "Gun Part"){
				if (gunPart.currentHp > 0) {
					gunPart.TakeDamage (damage);
				}
			}
			else if(coll.gameObject.tag == "Jet Part"){
				if (jetPart.currentHp > 0) {
					jetPart.TakeDamage (damage);
				}
			}
			Destroy (gameObject, 0.1f);
			isHit = true;
			enemymovement.ResetCD ();
			enemymovement.enabled = false;

		}
	}
}
