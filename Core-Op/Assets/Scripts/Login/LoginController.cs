using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour {

	public Button loginBtn;
	public InputField usernameInputField;

	void Awake(){
		loginBtn.onClick.AddListener(OnClickLogin);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClickLogin(){
		Debug.Log(usernameInputField.text + " Login");
		SceneManager.LoadScene("lobby");
	}
}
