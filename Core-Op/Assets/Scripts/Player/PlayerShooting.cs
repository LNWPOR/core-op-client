using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	public GameObject bullet;
	private float timer;
	private float cooldown;
	private bool isCanShoot;
	// Use this for initialization
	void Start () {
		timer = 0f;
		isCanShoot = true;
		cooldown = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		Shooting ();
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
		if (Input.GetMouseButtonDown (0)) {
			Quaternion direction = RotateToGun ();
			Vector2 gunEndPos = transform.position;
			Instantiate (bullet, gunEndPos, direction);
			isCanShoot = false;
		}
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
