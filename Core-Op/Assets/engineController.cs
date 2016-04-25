using UnityEngine;
using System.Collections;

public class engineController : MonoBehaviour {

	public float maxHp;

	private float currentHp;
	private bool canFix;

	// Use this for initialization
	void Start () {
		canFix = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsSelected(){
		if (canFix == true) {
			return true;
		} else {
			return false;
		}
	}

	public void Repairing(float fixpower){
		Debug.Log ("Repairing a engine");
	}

}
