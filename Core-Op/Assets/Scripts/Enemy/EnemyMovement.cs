using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	GameObject corePart;
	CoreManager core;
	private Vector2 corePos;
	private Vector2 enemyPos;
	private float dirVecX;
	private float dirVecY;
	private float frac;
	private float timer;
	private float dist;
	public float Cooldown = 2f;
	private float alpha = 0f;

	private static float a = 40f;
	private static float b = 35f;

	private Vector2 EtoC;
	private Vector2 NormEC;
	private Vector2 tangentEtoC;
	private Vector2 tangentNormEC;
	private float Amp;
	private float theta;
	public float speed = 2f;

	private bool phase;
	private int movementType;

	void InitForSine ()
	{
		EtoC = corePos - enemyPos;
		tangentEtoC.x = -EtoC.y;
		tangentEtoC.y = EtoC.x;
		NormEC = EtoC.normalized;
		tangentNormEC = tangentEtoC.normalized;
		Amp = 1.5f;
		theta = 0f;
	}

	void InitForPos ()
	{
		corePart = GameObject.FindGameObjectWithTag ("Core");
		core = corePart.GetComponent<CoreManager> ();
		corePos = core.GetCorePosition ();
		enemyPos = GetComponent<Transform> ().position;
	}

	// Use this for initialization
	void Start () {
		InitForPos ();
		InitForSine ();
		frac = 0.0f;
		timer = 0f;
		phase = false;
		movementType = Random.Range (0, 3);
	}
	// GG noob fuck
	
	// Update is called once per frame
	void FixedUpdate () {
		enemyPos = GetComponent<Transform> ().position;
		movementType = 2;
		switch (movementType) {
		case 0:
			Movement1 ();
			break;
		case 1:
			Movement2 ();
			break;
		case 2:
			Movement3 ();
			break;
		default:
			Movement1 ();
			break;
		}
	}

	void Movement1(){
		dist = Vector2.Distance(enemyPos,corePos);
		if (dist >= 40f) {
			GotoCore ();
		} else {
			if (timer >= Cooldown) {
				GotoCore ();
			}
			CountCD ();

		}
	}

	void Movement2(){
		dist = Vector2.Distance(enemyPos,corePos);
		if (dist >= 40f && !phase) {
			GotoCore ();
		} else {
			phase = true;
			if (timer >= Cooldown) {
				phase = false;
			}
			if (phase) {
//				Debug.Log(Vector2.Angle (corePos, enemyPos));
				GoElispe ();
			} else {
				GotoCore ();
			}
			CountCD ();

		}
	}
	void Movement3(){
//		dist = Vector2.Distance(enemyPos,corePos);
//		if (dist >= 50f) {
//			GotoCore ();
//		} else {
			GoSine ();
//		}
	}
	void GoSine(){
		Vector2 nextPos = enemyPos + NormEC * speed*Time.deltaTime*3f + tangentNormEC*Mathf.Sin(theta)*Amp;
		transform.position = Vector2.Lerp (enemyPos,nextPos,1);
		theta += 7f*Time.deltaTime;

	}

	void GetDegreeFormCoreToEnemy ()
	{
		if (alpha == 0f) {
			Vector2 V2 = enemyPos - corePos;
			float angle = Mathf.Atan2 (V2.y, V2.x);
			alpha = angle * 180f / Mathf.PI;
			if (alpha < 0) {
				alpha = alpha % 360 + 360;
			}
		}
	}

	void GoElispe(){
		float X;
		float Y;

		GetDegreeFormCoreToEnemy ();
		X = (corePos.x + (a * Mathf.Cos(alpha*Time.deltaTime)));
		Y = (corePos.y + (b * Mathf.Sin(alpha*Time.deltaTime)));
		Vector2 temp = new Vector2 (X,Y);
		frac = 1.0f/Vector2.Distance(enemyPos,corePos);
		alpha += 1;
		if (frac <= 1)
			transform.position = Vector2.Lerp(enemyPos, temp, frac);
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
