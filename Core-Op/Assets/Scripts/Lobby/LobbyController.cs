using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;



public class LobbyController : MonoBehaviour {

	public Button startBtn;

	void Awake() {
		startBtn.onClick.AddListener(OnClickStart);
		SocketOn();
		StartCoroutine("WaitScreenLoad");
	}

	// Use this for initialization
	void Start () {
		LobbyManager.Instance._usersData = new List<UserData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SocketOn(){
		NetworkManager.Instance.Socket.On("GET_CONNECTED_LOBBY_USER", OnGetConnectedLobbyUser);
		NetworkManager.Instance.Socket.On("USER_CONNECTED_LOBBY", OnUserConnectedLobby);
	}

	IEnumerator WaitScreenLoad(){
		yield return new WaitForSeconds(1);
		NetworkManager.Instance.Socket.Emit("GET_CONNECTED_LOBBY_USER");
		NetworkManager.Instance.Socket.Emit("USER_CONNECTED_LOBBY");
	}

	void OnClickStart(){
		SceneManager.LoadScene("play");
	}

	private void OnUserConnectedLobby(SocketIOEvent evt){
		// Debug.Log("gg");
		// Debug.Log(evt);
		// _usersData.Add(usrdata);
		UserData  usrdata = new UserData();
		usrdata.ID = Converter.JsonToString(evt.data.GetField("id").ToString());
		usrdata.UserName = Converter.JsonToString(evt.data.GetField("name").ToString());
		LobbyManager.Instance._usersData.Add(usrdata);
		Debug.Log(evt.data.GetField("id").ToString());
		Debug.Log(evt.data.GetField("name").ToString());
	}

	private void OnGetConnectedLobbyUser(SocketIOEvent evt){
		// Debug.Log(evt.data.GetField("totalClients"));
		// Debug.Log(evt.data.GetField("clients")[0].GetField("id"));
		// Debug.Log(evt.data.GetField("clients")[0].GetField("name"));
		// Debug.Log(evt.data.GetField("clients")[0].GetField("position"));

		// Debug.Log(Convert.ToInt32(evt.data.GetField("totalClients").ToString()));

		int totalClients = Convert.ToInt32(evt.data.GetField("totalClients").ToString());
		Debug.Log(totalClients);
		for(int i = 0 ; i < totalClients ; i++){
			// Debug.Log(evt.data.GetField("clients")[i].GetField("name"));
			UserData  usrdata = new UserData();
			usrdata.ID = evt.data.GetField("clients")[i].GetField("id").ToString();
			usrdata.UserName = evt.data.GetField("clients")[i].GetField("name").ToString();
			LobbyManager.Instance._usersData.Add(usrdata);	

		}

		for(int i = 0 ; i < totalClients ; i++){
			Debug.Log(LobbyManager.Instance._usersData[i].ID);
			Debug.Log(LobbyManager.Instance._usersData[i].UserName);
		}
	}
}
