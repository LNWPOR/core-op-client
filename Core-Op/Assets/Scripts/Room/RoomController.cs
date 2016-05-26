using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class RoomController : MonoBehaviour {

	// public GameObject[] player;
	// PlayerController[] playerControllerScript;
	// Use this for initialization
	void Start () {
		// playerControllerScript[0] = player[0].GetComponent<PlayerController> ();
		// playerControllerScript[1] = player[1].GetComponent<PlayerController> ();
		// Debug.Log(RoomManager.Instance.roomData.roomNumber);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// if( UserManager.Instance.userData.playerNumber == 0 ){
		// 	playerControllerScript[0].PlayerUpdate();
		// }else if( UserManager.Instance.userData.playerNumber == 1 ){
		// 	playerControllerScript[1].PlayerUpdate();
		// }
	}


}
