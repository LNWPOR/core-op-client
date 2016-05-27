using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class RoomController : MonoBehaviour {

	private int playerNumber = 0;
	public Text scoresText;
	public GameObject coreGun;
	public GameObject engine;
	public GameObject jet;
	coreGunController coreGunScript;
	engineController engineScript;
	jetController jetScript;
	private float time = 0f;
	string[] playerUsername =  new string[2];
	bool gameIsPlay = true;

	void Start () {
		StartCoroutine("WaitScreenLoad");
		playerNumber = UserManager.Instance.userData.playerNumber;

		coreGunScript = coreGun.GetComponent<coreGunController>();
		engineScript = engine.GetComponent<engineController>();
		jetScript = jet.GetComponent<jetController>();

		SocketOn();

		// Debug.Log(RoomManager.Instance.roomData.players[0].UserName);
		// Debug.Log(RoomManager.Instance.roomData.players[1].UserName);
	}
	IEnumerator WaitScreenLoad(){
		yield return new WaitForSeconds(1);
		JSONObject data = new JSONObject();
		data.AddField("roomNumber",RoomManager.Instance.roomData.roomNumber);
		NetworkManager.Instance.Socket.Emit("GET_ROOM",data);
		
	}
	
	void SocketOn(){
		NetworkManager.Instance.Socket.On("GET_ROOM", OnGetRoom);
		NetworkManager.Instance.Socket.On("GO_BACK_READY", OnGoBackReady);
	}



	// Update is called once per frame
	void FixedUpdate () {
		UpDateScores();
		if(gameIsPlay){
			GameIsOver();
		}
		
	}

	private void OnGoBackReady(SocketIOEvent evt){
		SceneManager.LoadScene("lobby");
	}

	private void OnGetRoom(SocketIOEvent evt){
		playerUsername[0] = Converter.JsonToString(evt.data.GetField("rooms").GetField("players")[0].GetField("name").ToString());
		playerUsername[1] = Converter.JsonToString(evt.data.GetField("rooms").GetField("players")[1].GetField("name").ToString());
		// Debug.Log( Converter.JsonToString(evt.data.GetField("rooms").GetField("players")[0].GetField("name").ToString()));
		// Debug.Log( Converter.JsonToString(evt.data.GetField("rooms").GetField("players")[1].GetField("name").ToString()));

		// RoomManager.Instance.roomData.players[0].UserName = Converter.JsonToString(evt.data.GetField("rooms").GetField("players")[0].GetField("name").ToString());
		// RoomManager.Instance.roomData.players[1].UserName = Converter.JsonToString(evt.data.GetField("rooms").GetField("players")[1].GetField("name").ToString());
	}

	void UpDateScores(){
		time = time + Time.deltaTime;
		scoresText.text = "Scores: "+ Mathf.Floor(time).ToString();
	}

	void GameIsOver(){
		if(engineScript.currentHp == 0 && coreGunScript.currentHp == 0 && jetScript.currentHp == 0){
			if(playerNumber == 0){
				JSONObject data = new JSONObject();
				data.AddField("roomNumber",RoomManager.Instance.roomData.roomNumber);
				data.AddField("player1", playerUsername[0]);
				data.AddField("player2", playerUsername[1]);
				data.AddField("scores",Mathf.Floor(time));
				NetworkManager.Instance.Socket.Emit("GO_BACK_READY",data);
				gameIsPlay = false;
			}
			
		}
	}




}
