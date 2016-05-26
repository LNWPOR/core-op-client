using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour {

	private int playerNumber;
	public float speed;
	public float breakSpeed;
	public float radius;
	public float fixPower;
	public float turnSpeed;
	private Vector2 movement;
	private float moveHorizontal;
	private float moveVertical;
	private Rigidbody2D rg2d;
	private float distSqr;
	private bool canFix;
	private Ray ray;
	private RaycastHit2D hit;
	private Vector3 VecMouse;
	float camRayLength;
	int floorMask;

	private bool canAddDamage;


	void Awake (){
		rg2d = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		speed = 10f;
		turnSpeed = 5f;
		breakSpeed = 15f;
		camRayLength = 100f;
		fixPower = 2f;
		canAddDamage = true;

		if(gameObject.name == "Player1"){
			playerNumber = 0;
		}else if(gameObject.name == "Player2"){
			playerNumber = 1;
		}
		
		SocketOn();
	}

	void SocketOn(){
		NetworkManager.Instance.Socket.On("UPDATE_OTHER_PLAYER", OnUpdateOtherPlayer);
	}

	void Update () {
//		Debug.Log (canAddDamage);
	}

	void FixedUpdate(){
		if(playerNumber == UserManager.Instance.userData.playerNumber){
			MoveUpdate ();
			Rotate ();
			BreakCheck ();
			MouseOverUpdate ();
			AddDamageTest ();
			RepairDamage ();
			testTurnForce ();

			JSONObject data = new JSONObject();
			data.AddField("position", transform.position.x.ToString()+","+transform.position.y.ToString());
			NetworkManager.Instance.Socket.Emit("UPDATE_OTHER_PLAYER",data);
		}
		

//		getNormMouse ();
	}

	private void OnUpdateOtherPlayer(SocketIOEvent evt){

		int playerNumberUpdate = Convert.ToInt32(evt.data.GetField("playerNumber").ToString());
		int roomNumberUpdate = Convert.ToInt32(evt.data.GetField("roomNumber").ToString());

		if(playerNumber == playerNumberUpdate && RoomManager.Instance.roomData.roomNumber == roomNumberUpdate){
			// Debug.Log(playerNumber + " : " + UserManager.Instance.userData.UserName);
			FromBoradcastVelocity(Converter.JsonToVecter2(Converter.JsonToString(evt.data.GetField("position").ToString())));
		}
		
	}

	public void FromBoradcastVelocity( Vector2 velocity ){
		StartCoroutine(MoveLerp(velocity.y));
	}

	IEnumerator MoveLerp( float pos){
		float posY = transform.position.y;
		while( posY !=  pos){
			posY = Mathf.Lerp(posY, pos, 5*Time.deltaTime);
			transform.position = new Vector2( transform.position.x, posY);
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
	}

	//-------------------------------------------//
	//  			  CUSTOM ZONE                //
	//-------------------------------------------//
	 

	void BreakCheck (){
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
		testTurnForce ();
	}
		
	void Rotate(){
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		float angle = Mathf.Atan2(Input.mousePosition.x - objectPos.x, Input.mousePosition.y - objectPos.y) * Mathf.Rad2Deg;
		Vector3 temp = new Vector3 (0, 0, -angle);	
		transform.rotation = Quaternion.Euler (temp);
	}

	void MouseOverUpdate(){
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		VecMouse = ray.origin;
		hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
		if (hit.collider != null) {
//			Debug.Log (hit.collider.name);
		}
	}

	void AddDamageTest(){
		DamageToEngine ();
		DamageToJet ();
		DamageToCoreGun ();
	}

	void DamageToEngine(){
		if (Input.GetKeyDown(KeyCode.X) && hit.collider.name == "engine"){
			var engine = hit.collider.gameObject.GetComponent<engineController> ();
			if (engine != null) {
				engine.TakeDamage (10f);
				canAddDamage = false;
			}
		}
	}

	void DamageToJet(){
		if (Input.GetKeyDown(KeyCode.X) && hit.collider.name == "jet"){
			var engine = hit.collider.gameObject.GetComponent<jetController> ();
			if (engine != null) {
				engine.TakeDamage (10f);
				canAddDamage = false;
			}
		}
	}

	void DamageToCoreGun(){
		if (Input.GetKeyDown(KeyCode.X) && hit.collider.name == "coreGun"){
			var engine = hit.collider.gameObject.GetComponent<coreGunController> ();
			if (engine != null) {
				engine.TakeDamage (10f);
				canAddDamage = false;
			}
		}
	}

	void RepairDamage(){
		RepairEngine ();
		RepairJet ();
		RepairCoreGun ();
	}

	void RepairEngine(){
		if (Input.GetKey (KeyCode.E) && hit.collider.name == "engine") {
			var engine = hit.collider.gameObject.GetComponent<engineController> ();
			if (engine != null) {
				engine.Repairing (fixPower);
			}
		}
	}

	void RepairJet(){
		if (Input.GetKey (KeyCode.E) && hit.collider.name == "jet") {
			var engine = hit.collider.gameObject.GetComponent<jetController> ();
			if (engine != null) {
				engine.Repairing (fixPower);
			}
		}
	}

	void RepairCoreGun(){
		if (Input.GetKey (KeyCode.E) && hit.collider.name == "coreGun") {
			var engine = hit.collider.gameObject.GetComponent<coreGunController> ();
			if (engine != null) {
				engine.Repairing (fixPower);
			}
		}
	}

	void testTurnForce() {
		rg2d.AddTorque (turnSpeed*Input.GetAxis("Horizontal"));
	}

	public Vector3 getMouseVec(){
		return VecMouse;
	}
		
}
