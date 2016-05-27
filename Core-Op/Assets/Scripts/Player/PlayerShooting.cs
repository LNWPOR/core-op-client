using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class PlayerShooting : MonoBehaviour {
	private int playerNumber;
	public GameObject bullet;
	private float timer;
	private float cooldown;
	private bool isCanShoot;
	// Use this for initialization
	void Start () {
		timer = 0f;
		isCanShoot = true;
		cooldown = 0.5f;

		if(gameObject.name == "playerGunEnd1"){
			playerNumber = 0;
		}else if(gameObject.name == "playerGunEnd2"){
			playerNumber = 1;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerNumber == UserManager.Instance.userData.playerNumber){
			// Shooting ();
			if (Input.GetMouseButtonDown (0)) {
				// Debug.Log("ggggggggggggg");
				JSONObject data = new JSONObject();
				data.AddField("position", transform.up.x.ToString()+","+transform.up.y.ToString()+","+transform.up.z.ToString());
				NetworkManager.Instance.Socket.Emit("SHOOT",data);


				// Quaternion direction = RotateToGun ();
				// Vector2 gunEndPos = transform.right * 2f;
				// Instantiate (bullet, gunEndPos, direction);
				// isCanShoot = false;
			}
		}
	}

	Quaternion RotateToGun ()
	{
		Quaternion direction;
		direction = transform.rotation;
		Vector3 temp;
		temp = direction.eulerAngles;
		temp.z += 90f;
		direction.eulerAngles = temp;
		return direction;
	}


	void StartFire ()
	{

		// if (Input.GetMouseButtonDown (0)) {
		// 	// Debug.Log("ggggggggggggg");
		// 	JSONObject data = new JSONObject();
		// 	data.AddField("position", transform.right.x.ToString()+","+transform.right.y.ToString()+","+transform.right.z.ToString());
		// 	NetworkManager.Instance.Socket.Emit("SHOOT",data);


		// 	// Quaternion direction = RotateToGun ();
		// 	// Vector2 gunEndPos = transform.right * 2f;
		// 	// Instantiate (bullet, gunEndPos, direction);
		// 	// isCanShoot = false;
		// }
	}

	


	void Shooting(){
		if(timer > cooldown && !isCanShoot)
			ReSetCooldown ();
		CountTime ();
		if (isCanShoot) {
			StartFire ();
		}
	}

	void ReSetCooldown(){
		timer = 0f;
		isCanShoot = true;
	}

	void CountTime(){
		timer += Time.deltaTime;
	}
}
