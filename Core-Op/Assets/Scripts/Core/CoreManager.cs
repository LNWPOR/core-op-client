using UnityEngine;
using System.Collections;

public class CoreManager : MonoBehaviour {
	Transform coreTF;
	// Use this for initialization
	void Start () {
		coreTF = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector2 GetCorePosition(){
		return coreTF.position;
	}
}
