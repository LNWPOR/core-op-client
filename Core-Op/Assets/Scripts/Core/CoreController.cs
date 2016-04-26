using UnityEngine;
using System.Collections;

public class CoreController : MonoBehaviour {

	public engineController enginePart;
	public coreGunController coreGunPart;
	public jetController jetPart;
	public float baseRotationSpeed;
	public float baseRegressionHpRatio;

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
		baseRegressionHpRatio = 1f;
		basePower = 20f;
		visionRadius = 300f;
		regressionHpRatio = baseRegressionHpRatio;
		rotationSpeed = baseRotationSpeed;

	}
	
	// Update is called once per frame
	void Update () {
		Rotate ();

	}

	void FixedUpdate() {
		RegressionEffect (enginePart,coreGunPart,jetPart);
		JetHpEffect ();
	}

	//-------------------------------------------//
	//  			  CUSTOM ZONE                //
	//-------------------------------------------//

	void RegressionEffect(engineController enginePart, coreGunController coreGunPart, jetController jetPart){
		EngineHpEffect ();
		if (enginePart.currentHp > 0) {
			enginePart.currentHp -= baseRegressionHpRatio * Time.deltaTime;
		} else {
			enginePart.currentHp = 0;
		}

		if (coreGunPart.currentHp > 0) {
			coreGunPart.currentHp -= regressionHpRatio * Time.deltaTime;
		} else {
			coreGunPart.currentHp = 0;
		}

		if (jetPart.currentHp > 0) {
			jetPart.currentHp -= regressionHpRatio * Time.deltaTime;
		} else {
			jetPart.currentHp = 0;
		}

	}

	void Rotate(){
		transform.Rotate (Vector3.forward,Time.deltaTime * rotationSpeed);
	}

	void EngineHpEffect(){
		regressionHpRatio = (enginePart.maxHp / enginePart.currentHp) * baseRegressionHpRatio;
	}

	void JetHpEffect(){
		rotationSpeed = (jetPart.maxHp / jetPart.currentHp) * baseRotationSpeed; // Some equation
	}

	void CoreGunHpEffect(){

	}


}
