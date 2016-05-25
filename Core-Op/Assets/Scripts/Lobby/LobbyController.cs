using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class LobbyController : MonoBehaviour {

	// public Button startBtn;
	public Button[] joinRoomBtn;
	public Text[] joinRoomBtnText;
	public Text[] roomText;
	public Text[] playerNameText;
	private int roomNumberSelected;

	void Awake() {
		// startBtn.onClick.AddListener(OnClickStart);
		joinRoomBtn[0].onClick.AddListener(() => OnClickJoinRoom(0));
		joinRoomBtn[1].onClick.AddListener(() => OnClickJoinRoom(1));
		joinRoomBtn[2].onClick.AddListener(() => OnClickJoinRoom(2));

		SocketOn();
		StartCoroutine("WaitScreenLoad");
	}

	// Use this for initialization
	void Start () {
		LobbyManager.Instance._usersData = new List<UserData>();
		LobbyManager.Instance._roomsData = new List<RoomData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SocketOn(){
		NetworkManager.Instance.Socket.On("GET_CONNECTED_LOBBY_USER", OnGetConnectedLobbyUser);
		NetworkManager.Instance.Socket.On("GET_CONNECTED_ROOM_USER", OnGetConnectedRoomUser);
		NetworkManager.Instance.Socket.On("USER_CONNECTED_LOBBY", OnUserConnectedLobby);
		NetworkManager.Instance.Socket.On("USER_CONNECTED_ROOM", OnUserConnectedRoom);
		NetworkManager.Instance.Socket.On("OTHER_USER_CONNECTED_ROOM", OnOtherUserConnectedRoom);
	}

	IEnumerator WaitScreenLoad(){
		yield return new WaitForSeconds(1);
		NetworkManager.Instance.Socket.Emit("USER_CONNECTED_LOBBY");
		NetworkManager.Instance.Socket.Emit("GET_CONNECTED_LOBBY_USER");
		NetworkManager.Instance.Socket.Emit("GET_CONNECTED_ROOM_USER");
		
	}

	// void OnClickStart(){
	// 	SceneManager.LoadScene("play");
	// }

	void OnClickJoinRoom(int roomNumber){
		
		roomNumberSelected = roomNumber;
		Dictionary<string, string> data = new Dictionary<string, string>();
		data["roomNumber"] = roomNumber.ToString();
		NetworkManager.Instance.Socket.Emit("USER_CONNECTED_ROOM",new JSONObject(data));
		// SceneManager.LoadScene("play");
	}

	private void OnUserConnectedRoom(SocketIOEvent evt){
		// RoomManager.Instance.roomNumber = roomNumber;

		if(evt.data.GetField("canEnterRoom").ToString() == "true"){
			Debug.Log("welcome to roomNumber : " + (roomNumberSelected+1) + " as a playerNumber : " + evt.data.GetField("roomSelected").GetField("players").Count);
			
			LobbyManager.Instance._roomsData[roomNumberSelected].players[evt.data.GetField("roomSelected").GetField("players").Count-1] = UserManager.Instance.userData;
			joinRoomBtn[roomNumberSelected].interactable = false;

			
		}else if(evt.data.GetField("canEnterRoom").ToString() == "false"){
			Debug.Log("This room was full.Please try again later.");
		}
		
	}

	private void OnOtherUserConnectedRoom(SocketIOEvent evt){
		// RoomManager.Instance.roomNumber = roomNumber;
		Debug.Log("gg");
	}

	private void OnUserConnectedLobby(SocketIOEvent evt){
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
		// Debug.Log(totalClients);
		for(int i = 0 ; i < totalClients ; i++){
			// Debug.Log(evt.data.GetField("clients")[i].GetField("name"));
			UserData  usrdata = new UserData();
			usrdata.ID = evt.data.GetField("clients")[i].GetField("id").ToString();
			usrdata.UserName = evt.data.GetField("clients")[i].GetField("name").ToString();
			LobbyManager.Instance._usersData.Add(usrdata);	

		}

		// for(int i = 0 ; i < totalClients ; i++){
		// 	Debug.Log(LobbyManager.Instance._usersData[i].ID);
		// 	Debug.Log(LobbyManager.Instance._usersData[i].UserName);
		// }
	}

	private void OnGetConnectedRoomUser(SocketIOEvent evt){
		int totalRooms = Convert.ToInt32(evt.data.GetField("totalRooms").ToString());
		int maxRoomPlayer = Convert.ToInt32(evt.data.GetField("maxRoomPlayer").ToString());
		// Debug.Log(totalRooms);

		for(int i = 0 ; i < totalRooms ; i++){
			RoomData  roomData = new RoomData();
			roomData.roomNumber = i;
			roomData.players = new List<UserData>();

			// Debug.Log(evt.data.GetField("rooms")[i].GetField("players").Count);
			if(evt.data.GetField("rooms")[i].GetField("players").Count == maxRoomPlayer){
				joinRoomBtn[i].interactable = false;
				joinRoomBtnText[i].text = "FULL";
			}else{
				joinRoomBtn[i].interactable = true;
				joinRoomBtnText[i].text = "JOIN";
			}

			for(int j = 0 ; j < evt.data.GetField("rooms")[i].GetField("players").Count ; j++){
				// Debug.Log(evt.data.GetField("rooms")[i].GetField("players")[j].GetField("id"));
				UserData  usrdata = new UserData();
				usrdata.ID = evt.data.GetField("rooms")[i].GetField("players")[j].GetField("id").ToString();
				usrdata.UserName = evt.data.GetField("rooms")[i].GetField("players")[j].GetField("name").ToString();
				roomData.players.Add(usrdata);	
				// Debug.Log(roomData.players[j]);

				playerNameText[i*maxRoomPlayer+j].text = usrdata.UserName;
			}
			LobbyManager.Instance._roomsData.Add(roomData);
			roomText[i].text = "Room#" + (i+1);

		}

		// for(int i = 0 ; i < totalRooms ; i++){
		// 	Debug.Log(i);
		// 	for(int j = 0 ; j < maxRoomPlayer ; j++){
		// 		Debug.Log(LobbyManager.Instance._roomsData[i].players[j].UserName);
		// 	}
		// }

	}
}
