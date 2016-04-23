using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody2D rg2d;

	// Use this for initialization
	void Start () {
		rg2d = GetComponent<Rigidbody2D> ();
		speed = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		float moveHorrizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorrizontal, moveVertical);
		rg2d.AddForce (movement * speed);
	}

}
