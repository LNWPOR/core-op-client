using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {
	private float timer;
	private bool isHit;
	private float finalTime;
	PlayerController playerMovement;
	// Use this for initialization
	void Awake () {
		timer = 0f;
		isHit = false;
		playerMovement = GetComponent<PlayerController> ();

	}
	
	// Update is called once per frame
	void Update () {
		if(isHit){
			timer += Time.deltaTime;
			TimeCounter ();
		}
	}

	public void StartTimeCounter(bool hit,float fin){
		isHit = hit;
		finalTime = fin;
	}

	void TimeCounter(){
		if(timer <= finalTime){
			playerMovement.enabled = false;
		} else {
			timer = 0f;
			playerMovement.enabled = true;
			isHit = false;
		}
	}
		
}
