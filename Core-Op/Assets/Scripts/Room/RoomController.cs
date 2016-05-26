using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class RoomController : MonoBehaviour {

	public GameObject[] player;
	// public List<GameObject> playerPrefabList;

	// Use this for initialization
	void Start () {

		// playerPrefabList = new List<GameObject>();

		// CreatePlayer();
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
		// if( UserManager.Instance.userData.playerNumber == 0 ){
		// 	// racketPlayer1.Move();
		// }else if( UserManager.Instance.userData.playerNumber == 1 ){
		// 	// racketPlayer2.Move();
		// }
	}

	private void CreatePlayer(){ 
		// for(int i = 0 ; i < RoomManager.Instance.roomData.players.Count ; i++){
		// 	// Vector3 playerPos = new Vector3(i*playerSpawnRange, 0, 0);
		// 	// playerPrefabList.Add((GameObject)Instantiate(playerPrefab, playerPos,playerPrefab.transform.rotation));
		// }
	}

}
