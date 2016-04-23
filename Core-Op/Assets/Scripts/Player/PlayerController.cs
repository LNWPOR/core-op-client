using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Vector2 movement;
	private Rigidbody2D rg2d;
	float camRayLength;
	int floorMask;

	// Use this for initialization
	void Awake (){
		floorMask = LayerMask.GetMask("Floor");
		rg2d = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		speed = 10;
		camRayLength = 100f;
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		Move ();
		Rotate ();

	}

	void Move(){
		float moveHorrizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		movement = new Vector2 (moveHorrizontal, moveVertical);
		movement = movement.normalized;
		rg2d.AddForce (movement * speed);
	}

	void Rotate(){
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		float angle = Mathf.Atan2(Input.mousePosition.x - objectPos.x, Input.mousePosition.y - objectPos.y) * Mathf.Rad2Deg;
		Vector3 temp = new Vector3 (0, 0, -angle);	
		transform.rotation = Quaternion.Euler (temp);
	}

}
