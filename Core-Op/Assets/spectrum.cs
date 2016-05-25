using UnityEngine;
using System.Collections;

public class spectrum : MonoBehaviour {

	public GameObject prefab;
	public int numberOfObjects = 20;
	public float radius = 5f;
	public GameObject[] cubes;
	public int numberOfChunk = 10;
	private Vector3[] orgPos;
	private Vector3 orgScale;

	void Awake(){
		orgPos = new Vector3[numberOfObjects];
	}

	void Start() {
		for (int i = 0; i < numberOfObjects; i++) {
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),1) * radius;
			orgPos [i] = pos;
			float angleId = Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg; 
			Quaternion temp = Quaternion.AngleAxis (angleId, Vector3.forward);
			Instantiate(prefab, pos, temp);
		}
		cubes = GameObject.FindGameObjectsWithTag ("Equrizer");
		orgScale = cubes [0].transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		TranformSpectrum ();
	}

	void ChunkFreq(GameObject[] cubes){
		int sizePerChunk = cubes.Length / numberOfChunk;
	}

	void TranformSpectrum(){
		float[] spectrum = AudioListener.GetSpectrumData (2048, 0, FFTWindow.Hamming);
		for (int i = 0; i < numberOfObjects; i++) {
			Vector3 previousScale = cubes [i].transform.localScale;
			Vector3 tempPos = orgPos[i];
			//previousScale.x = Mathf.MoveTowards (previousScale.x, spectrum [i] * 200, spectrum [i] * 200);
			previousScale.x = Mathf.Lerp (previousScale.x, spectrum [i] * 200, Time.deltaTime * 30);
			tempPos = orgPos[i] + cubes [i].transform.right * (previousScale.x / 2.0f + orgScale.x / 2.0f);
			cubes [i].transform.localScale = previousScale;
			cubes [i].transform.position = tempPos;
		}
	}
}
