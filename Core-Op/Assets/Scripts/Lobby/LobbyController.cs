using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;



public class LobbyController : MonoBehaviour {

	public Button startBtn;
	public List<UserData> _usersData;

	void Awake() {

		startBtn.onClick.AddListener(OnClickStart);

		SocketOn();
		// Debug.Log(NetworkManager.Instance.Socket);
		// NetworkManager.Instance.Socket.On("USER_CONNECTED", OnUserConnected);
		// Debug.Log(LobbyManager.Instance._usersData);
		StartCoroutine("WaitScreenLoad");
	}

	// Use this for initialization
	void Start () {
		_usersData = new List<UserData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SocketOn(){
		NetworkManager.Instance.Socket.On("USER_CONNECTED_LOBBY", OnUserConnectedLobby);
		NetworkManager.Instance.Socket.On("GET_CONNECTED_LOBBY_USER", OnGetConnectedLobbyUser);
	}

	IEnumerator WaitScreenLoad(){
		yield return new WaitForSeconds(1);
		NetworkManager.Instance.Socket.Emit("USER_CONNECTED_LOBBY");
		NetworkManager.Instance.Socket.Emit("GET_CONNECTED_LOBBY_USER");
	}

	void OnClickStart(){
		SceneManager.LoadScene("play");
	}

	private void OnUserConnectedLobby(SocketIOEvent evt){
		// Debug.Log("gg");
		// Debug.Log(evt);
		// _usersData.Add(usrdata);
	}

	private void OnGetConnectedLobbyUser(SocketIOEvent evt){
		// Debug.Log(evt.data.GetField("totalClients"));
		// Debug.Log(evt.data.GetField("clients")[0].GetField("id"));
		// Debug.Log(evt.data.GetField("clients")[0].GetField("name"));
		// Debug.Log(evt.data.GetField("clients")[0].GetField("position"));

		// Debug.Log(Convert.ToInt32(evt.data.GetField("totalClients").ToString()));

		int totalClients = Convert.ToInt32(evt.data.GetField("totalClients").ToString());
		for(int i = 0 ; i < totalClients ; i++){
			// Debug.Log(evt.data.GetField("clients")[i].GetField("name"));
			UserData  usrdata = new UserData();
			usrdata.ID = evt.data.GetField("clients")[i].GetField("id").ToString();
			usrdata.UserName = evt.data.GetField("clients")[i].GetField("name").ToString();
			_usersData.Add(usrdata);	

		}

		// for(int i = 0 ; i < totalClients ; i++){
		// 	Debug.Log(_usersData[i].ID);
		// 	Debug.Log(_usersData[i].UserName);
		// }
	}
}
