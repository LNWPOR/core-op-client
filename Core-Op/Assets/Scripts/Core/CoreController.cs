using UnityEngine;
using System.Collections;

public class CoreController : MonoBehaviour {

	public GameObject engine;
	public GameObject coreGun;
	public GameObject jet;
	public float baseRotationSpeed;

	private float rotationSpeed;
	private float rotationRad;
	private float basePower;
	private float visionRadius;
	private float regressionHpRatio;

	void Awake (){
		
	}

	// Use this for initialization
	void Start () {
		baseRotationSpeed = 25f;
		rotationSpeed = baseRotationSpeed;
		basePower = 20f;
		visionRadius = 300f;
		regressionHpRatio = 0.5f;

	}
	
	// Update is called once per frame
	void Update () {
		Rotate ();

	}

	void FixedUpdate() {
		JetHpEffect ();
	}

	//-------------------------------------------//
	//  			  CUSTOM ZONE                //
	//-------------------------------------------//

	void Rotate(){
		transform.Rotate (Vector3.forward,Time.deltaTime * rotationSpeed);
	}

	void EngineHpEffect(){
	
	}

	void JetHpEffect(){
		var jetPart = jet.GetComponent<jetController> ();
		rotationSpeed = (jetPart.maxHp/jetPart.currentHp)*baseRotationSpeed; // Some equation
	}

	void CoreGunHpEffect(){

	}
}
