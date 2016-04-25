using UnityEngine;
using System.Collections;

public class FixZone : MonoBehaviour {

	public float radius;
	public float fixPower;

	private float distSqr;
	private bool canFix;
	private Ray ray;
	private RaycastHit2D hit;
	private string mouseOn;

	void Awake(){
		
	}

	void Start () {
		radius = 5f;
		canFix = false;
		fixPower = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);

		if (hit.collider != null) {
			Debug.Log (hit.collider.name);
		}

	}

	void OnTriggerStay (Collider other){
		if(other.gameObject.name == "engine" && Input.GetKey(KeyCode.E) && mouseOn == "engine"){
			var health = other.gameObject.GetComponent<engineController>();
			if (health != null) {
				health.Repairing (fixPower);
			}
		}
	}
		
	void Fixing(){
	
	}

}
