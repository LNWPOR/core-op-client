using UnityEngine;
using System.Collections;

public class engineController : MonoBehaviour {

	public float maxHp;
	public GameObject player;

	private float currentHp;
	private bool canFix;

	void Start () {
		canFix = false;
		maxHp = 50f;
		currentHp = maxHp;
	}

	void Update () {
		
	}

	void FixedUpdate () {
		CurrentHpUpdate ();
	}

	//-------------------------------------------//
	//  			  CUSTOM ZONE                //
	//-------------------------------------------//

	public bool IsSelected(){
		if (canFix == true) {
			return true;
		} else {
			return false;
		}
	}

	public void TakeDamage(){
		
	}

	public void Repairing(float fixpower){
		Debug.Log ("Repairing a engine");
	}

}
