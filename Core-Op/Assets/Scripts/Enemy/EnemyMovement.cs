using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public Transform core;
	private Vector2 corePos;
	private Vector2 enemyPos;
	private float frac;
	private float timer;
	private float dist;

	// Use this for initialization
	void Start () {
		corePos = core.position;
		frac = 0.0f;
		timer = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		enemyPos = GetComponent<Transform> ().position;
		Movement ();

	}

	void Movement(){
		dist = Vector2.Distance(enemyPos,corePos);
		if (dist >= 40f) {
			GotoCore ();
		} else {
			if (timer >= 2f) {
				GotoCore ();
			}
			CountCD ();

		}
	}

	void GotoCore(){
//		Debug.Log (frac);
		frac = 1.0f/dist;
		if (frac <= 1)
			transform.position = Vector2.Lerp(enemyPos, corePos, frac);
	}

	void CountCD(){
		timer += Time.deltaTime;
	}
	public void ResetCD(){
		timer = 0f;
	}
		
}
