using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SocketIO;

public class LoginController : MonoBehaviour {

	public Button loginBtn;
	public InputField usernameInputField;
	private SocketIOComponent _socket;

	void Awake(){
		loginBtn.onClick.AddListener(OnClickLogin);
		InitManager();
		SocketOn();
	}

	void Start () {
		
	}
	
	void Update () {
	
	}

	void InitManager(){
		NetworkManager.Instance.Init();
		_socket = NetworkManager.Instance.Socket;
		DontDestroyOnLoad(_socket.gameObject);
		DontDestroyOnLoad(UserManager.Instance.gameObject);
		DontDestroyOnLoad(LobbyManager.Instance.gameObject);
	}

	void SocketOn(){
		_socket.On("NET_AVARIABLE",  (SocketIOEvent evt) =>{
			Debug.Log("Net Avariable");
		});
		_socket.On("CONNECTED", OnUserLogin);
	}

	private void OnUserLogin(SocketIOEvent evt ){
		// Debug.Log("ID = "+evt.data.GetField("id").ToString());
		// Debug.Log("NAME = "+evt.data.GetField("name").ToString());
		UserData usrData = new UserData();
		usrData.ID = Converter.JsonToString(evt.data.GetField("id").ToString());
		usrData.UserName = Converter.JsonToString(evt.data.GetField("name").ToString());
		usrData.isSignUp = true;
		UserManager.Instance.userData = usrData;
		SceneManager.LoadScene("lobby");
	}

	void OnClickLogin(){
		// Debug.Log(usernameInputField.text + " Login");
		string username = usernameInputField.text;
		Dictionary<string, string> data = new Dictionary<string, string>();
		data["name"] = username;
		NetworkManager.Instance.Socket.Emit("SIGNUP",new JSONObject(data));
		
	}


}
