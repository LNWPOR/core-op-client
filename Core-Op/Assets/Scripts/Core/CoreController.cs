using UnityEngine;
using System.Collections;

public class CoreController : MonoBehaviour {
	private float minimumFrac = 10f;
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
		enginePart.TakeDamage( baseRegressionHpRatio * Time.deltaTime );
		coreGunPart.TakeDamage (regressionHpRatio * Time.deltaTime);
		jetPart.TakeDamage (regressionHpRatio * Time.deltaTime);
	}

	void Rotate(){
		transform.Rotate (Vector3.forward,Time.deltaTime * rotationSpeed);
	}

	void EngineHpEffect(){
		float frac = enginePart.currentHp;
		if (frac < minimumFrac)
			frac = minimumFrac;
		regressionHpRatio = (enginePart.maxHp / frac) * baseRegressionHpRatio;
	}

	void JetHpEffect(){
		float frac = jetPart.currentHp;
		if (frac < minimumFrac)
			frac = minimumFrac;
		rotationSpeed = (jetPart.maxHp / frac) * baseRotationSpeed; // Some equation
	}

	void CoreGunHpEffect(){

	}


}
