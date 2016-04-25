using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float breakSpeed;
	public float radius;
	public float fixPower;

	private Vector2 movement;
	private float moveHorizontal;
	private float moveVertical;
	private Rigidbody2D rg2d;
	private float distSqr;
	private bool canFix;
	private Ray ray;
	private RaycastHit2D hit;
	float camRayLength;
	int floorMask;


	void Awake (){
		rg2d = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		speed = 10f;
		breakSpeed = 15f;
		camRayLength = 100f;
		fixPower = 2f;
	}


	void Update () {
		
	}

	void FixedUpdate(){
		MoveUpdate ();
		Rotate ();
		StopCheck ();
		MouseOverUpdate ();
	}


	//-------------------------------------------//
	//  			  CUSTOM ZONE                //
	//-------------------------------------------//
	 

	void StopCheck(){
		float x_axis = rg2d.velocity.x;
		float y_axis = rg2d.velocity.y;
		Vector2 breakX = new Vector2 (-breakSpeed, 0f);
		Vector2 breakY = new Vector2 (0f ,-breakSpeed);
		if (Input.GetKey(KeyCode.Space)){
			if (x_axis > 0) {
				rg2d.AddForce (breakX);
			} else if(x_axis < 0.2f) {
				rg2d.AddForce (-breakX);
			}

			if (y_axis > 0) {
				rg2d.AddForce (breakY);
			} else if (y_axis < 0.2f) {
				rg2d.AddForce (-breakY);
			}

		}
	}

	void MoveUpdate(){
		moveHorizontal = Input.GetAxis ("Horizontal");
		moveVertical = Input.GetAxis ("Vertical");
		movement = new Vector2 (moveHorizontal, moveVertical);
		movement = movement.normalized;
		rg2d.AddForce (movement * speed);
	}
		
	void Rotate(){
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		float angle = Mathf.Atan2(Input.mousePosition.x - objectPos.x, Input.mousePosition.y - objectPos.y) * Mathf.Rad2Deg;
		Vector3 temp = new Vector3 (0, 0, -angle);	
		transform.rotation = Quaternion.Euler (temp);
	}

	void MouseOverUpdate(){
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
		if (hit.collider != null) {
			Debug.Log (hit.collider.name);
		}
	}

}
