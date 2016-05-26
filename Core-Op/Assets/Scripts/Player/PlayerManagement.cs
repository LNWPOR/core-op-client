using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {
	private float timer;
	private bool isHit;
	private float finalTime;
	private float maxGauge = 100f;
	private float damageOverTime = 20f;
	private float healPerHit = 10f;
	private float currentHeal;
	PlayerController playerMovement;
	// Use this for initialization
	void Awake () {
		timer = 0f;
		currentHeal = 0f;
		isHit = false;
		playerMovement = GetComponent<PlayerController> ();

	}
	
	// Update is called once per frame
	void Update () {
		if(isHit){
			Debug.Log (currentHeal);
			timer += Time.deltaTime;
//			TimeCounter ();
			// StartReviving();
		}
	}

	public void StartCounter(bool hit,float fin){
		isHit = hit;
		currentHeal = 0f;
		finalTime = fin;
	}

	void ResistHealing(){
		if (currentHeal > 0f && currentHeal <= maxGauge) {
			currentHeal -= damageOverTime * Time.deltaTime;
		}
		else if (currentHeal < 0f) {
			currentHeal = 0f;
		}

	}

	void Healing(){
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (currentHeal <= maxGauge) {
				currentHeal += healPerHit;
			}
			else if (currentHeal > maxGauge) {
				currentHeal = maxGauge;
				playerMovement.enabled = true;
				isHit = false;
			}
		}

	}

	void StartReviving(){
		playerMovement.enabled = false;
		Healing ();
		ResistHealing ();
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
