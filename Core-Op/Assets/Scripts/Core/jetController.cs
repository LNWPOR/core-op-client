using UnityEngine;
using System.Collections;

public class jetController : MonoBehaviour {

	public float maxHp;
	public GameObject[] player;
	public float currentHp;
	public RectTransform healthBar;

	private bool canFix;
	private float radius;

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
		float[] sqrlen = new float[2];
		Vector2[] offset = new Vector2[2];
		for(int i = 0 ; i < sqrlen.Length ; i++){
			offset[i] = player[i].transform.position - transform.position;
			sqrlen[i] = offset[i].sqrMagnitude;
		}
		if (sqrlen[0] <= radius || sqrlen[1] <= radius) {
			return true;
		} else {
			return false;
		}
	}
}
