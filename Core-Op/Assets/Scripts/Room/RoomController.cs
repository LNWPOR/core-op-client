using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class RoomController : MonoBehaviour {

	public Text scoresText;
	public GameObject coreGun;
	public GameObject engine;
	public GameObject jet;
	coreGunController coreGunScript;
	engineController engineScript;
	jetController jetScript;
	private float time = 0f;
	void Start () {
		coreGunScript = coreGun.GetComponent<coreGunController>();
		engineScript = engine.GetComponent<engineController>();
		jetScript = jet.GetComponent<jetController>();

		Debug.Log(coreGunScript.currentHp);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpDateScores();
		GameIsOver();
	}

	void UpDateScores(){
		time = time + Time.deltaTime;
		scoresText.text = "Scores: "+ Mathf.Floor(time).ToString();
	}

	void GameIsOver(){
		if(engineScript.currentHp == 0 && coreGunScript.currentHp == 0 && jetScript.currentHp == 0){
			SceneManager.LoadScene("lobby");
		}
	}


}
