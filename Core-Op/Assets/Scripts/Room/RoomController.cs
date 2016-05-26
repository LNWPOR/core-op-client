using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class RoomController : MonoBehaviour {

	public GameObject playerPrefab;
	public List<GameObject> playerPrefabList;
	// Use this for initialization
	void Start () {

		playerPrefabList = new List<GameObject>();

		CreatePlayer();
		// Debug.Log(RoomManager.Instance.roomData.roomNumber);
		// if(UserManager.Instance.userData.playerNumber == 0){
		// 	Vector3 rndPos = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20));
		// 	Instantiate(playerPrefab, playerPrefab.position, playerPrefab.rotation)
		// }
		// else(UserManager.Instance.userData.playerNumber == 1){

		// }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void CreatePlayer(){
		Debug.Log(RoomManager.Instance.roomData.players.Count);
		for(int i = 0 ; i < RoomManager.Instance.roomData.players.Count ; i++){

		}
	}

}
