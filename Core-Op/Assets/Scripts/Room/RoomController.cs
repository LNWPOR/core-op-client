using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class RoomController : MonoBehaviour {

	public Text scoresText;

	private float time = 0f;
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time = time + Time.deltaTime;
		scoresText.text = "Scores: "+ Mathf.Floor(time).ToString();
	}


}
