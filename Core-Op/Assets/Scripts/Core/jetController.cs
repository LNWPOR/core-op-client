using UnityEngine;
using System.Collections;

public class jetController : MonoBehaviour {

	public float maxHp;
	public GameObject player;
	public float currentHp;
	public RectTransform healthBar;

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
		healthBar.sizeDelta = new Vector2(currentHp*2, healthBar.sizeDelta.y);
	}

	public void Repairing(float fixpower){
		if (currentHp < maxHp && ReadyToRepair ()) {
			currentHp += fixpower;
			if (currentHp > maxHp) {
				currentHp = maxHp;
			}
		}
		healthBar.sizeDelta = new Vector2(currentHp*2, healthBar.sizeDelta.y);
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
