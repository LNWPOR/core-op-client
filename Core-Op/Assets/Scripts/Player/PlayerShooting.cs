using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	public GameObject bullet;
	// Use this for initialization
	void Start () {
	
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

	void Shooting(){
		if (Input.GetMouseButtonDown(0)) {
			Quaternion direction = RotateToGun ();
			Vector2 gunEndPos = transform.position;
			Instantiate (bullet, gunEndPos, direction);
		}
	}
}
