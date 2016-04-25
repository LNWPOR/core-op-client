using UnityEngine;
using System.Collections;

public class jetController : MonoBehaviour {

	public float maxHp;
	public GameObject player;

	private float currentHp;
	private bool canFix;
	private float radius;
	private Vector2 offset;

	void Start () {
		canFix = false;
		maxHp = 50f;
		radius = 55f;
		currentHp = maxHp;
	}

	void Update () {

	}

	void FixedUpdate () {
		ReadyToRepair ();
		Debug.Log ("Jet" + currentHp);
	}

	//-------------------------------------------//
	//  			  CUSTOM ZONE                //
	//-------------------------------------------//

	public void TakeDamage(float damage){
		if (currentHp - damage > 0) {
			currentHp -= damage;
		} else {
			currentHp = 0;
		}
	}

	public void Repairing(float fixpower){
		if (currentHp < maxHp && ReadyToRepair ()) {
			currentHp += fixpower;
			if (currentHp > maxHp) {
				currentHp = maxHp;
			}
		}
	}

	bool ReadyToRepair(){
		offset = player.transform.position - transform.position;
		float sqrlen = offset.sqrMagnitude;
		//Debug.Log (sqrlen);
		if (sqrlen <= radius) {
			return true;
		} else {
			return false;
		}
	}
}
