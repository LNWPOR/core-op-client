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
		GotoCore ();
		timer += Time.deltaTime;
	}

	void GotoCore(){
		dist = Vector2.Distance(enemyPos,corePos);
//		Debug.Log (frac);
		frac = 1.0f/dist;
		if (frac <= 1)
			transform.position = Vector2.Lerp(enemyPos, corePos, frac);
	}
		
}
