using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class SignUpController : MonoBehaviour {

	public InputField usernameInputField;
	public InputField passwordInputField;
	public Button signUpBtn;

	// Use this for initialization
	void Start () {
		signUpBtn.onClick.AddListener(OnClickSingUp);

		SocketOn();
	}

	void SocketOn(){
		NetworkManager.Instance.Socket.On("SIGNUP_READY", OnSignUpReady);
	}

	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClickSingUp(){
		JSONObject data = new JSONObject();
		data.AddField("username",usernameInputField.text );
		data.AddField("password",passwordInputField.text );
		NetworkManager.Instance.Socket.Emit("SIGNUP",data);
	}

	private void OnSignUpReady(SocketIOEvent evt){
		int status = Convert.ToInt32(evt.data.GetField("status").ToString());
		if(status == 1){
			SceneManager.LoadScene("login");
		}else{
			Debug.Log("signup fail");
		}
	}
}
