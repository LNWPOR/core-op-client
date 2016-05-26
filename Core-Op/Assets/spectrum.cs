using UnityEngine;
using System.Collections;

public class spectrum : MonoBehaviour {

	public struct Chunk{
		public int minRange;
		public int[] spectrum ;
		public float score;
	}

	public GameObject prefab;
	public GameObject enemyPrefab;
	public int numberOfObjects = 20;
	public float radius = 5f;
	public float ratio = 0.0175f;
	public float[] spt;
	public GameObject[] cubes;
	public int numberOfChunk = 5;
	public float bpm;
	private bool chunkEnable;
	private Vector3[] orgPos;
	private Vector3 orgScale;
	private int sizePerChunk;

	private int[] highScoreIndex;
	private float[] highScore;
	public Chunk[] chunk; 

	void Awake(){
		orgPos = new Vector3[numberOfObjects];
		chunk = new Chunk[numberOfChunk];
		bpm = 1 / (90 / 60);
		chunkEnable = true;
		sizePerChunk = numberOfObjects / numberOfChunk;
	}

	void Start() {
		CreateSpectrum ();
		CreateChunk ();
		InvokeRepeating ("GetChunkScore", 1f, bpm);
	}
	
	// Update is called once per frame
	void Update () {
		TranformSpectrum ();
	}

	void CreateChunk (){
		int spectrumNumber = 0;

		for (int i = 0; i < numberOfChunk; i++) {
			chunk [i].score = 0;
			chunk [i].spectrum = new int[sizePerChunk];
			for (int k = 0; k < sizePerChunk; k++) {
				chunk [i].spectrum [k] = spectrumNumber;
				if (k == 0) {
					chunk [i].minRange = spectrumNumber;
				}
				spectrumNumber++;
			}
		}
	}

	void GetChunkScore(){
		highScoreIndex = new int[3]{0,0,0};
		highScore = new float[3]{0,0,0};
		for (int i = 0; i < numberOfChunk; i++) {
			float score = 0;
			for (int j = 0; j < chunk [i].spectrum.Length; j++) {
				score += spt [chunk [i].spectrum [j]];

			}
			Debug.Log ("Score : "+ i + " " +score);
			chunk [i].score = score;
			int replaceIndex = 0;
			bool needReplace = false;
			for ( int k = 0; k < highScore.Length; k++) {
				if (score > highScore [k]) {
					replaceIndex = k;
					needReplace = true;
				}
			}
			highScore [replaceIndex] = score;
			Debug.Log ("NeedReplace : " + needReplace);

			CheckRatio(needReplace,score,replaceIndex,i);

		}

		for (int k = 0; k < highScore.Length ; k++) {
			Debug.Log("HighScore in List : " + highScore [k]);
		}
			
		InitEnemy (highScore,highScoreIndex);

	}

	void CheckRatio(bool needReplace,float score,int replaceIndex,int i){
		if (needReplace == true) {
			if (score > ratio) {
				highScoreIndex [replaceIndex] = chunk [i].minRange;
				Debug.Log ("Score : " + score + "| > |" + " Ratio : " + ratio);
			} else {
				highScoreIndex [replaceIndex] = 999;
				Debug.Log ("Score : " + score + "| < |" + " Ratio : " + ratio);
			}
		}
	}

	void InitEnemy(float[] highScore,int[] highScoreIndex){
		for (int i = 0; i < highScoreIndex.Length; i++) {
			if (highScoreIndex [i] != 999) {
				int spectrumIndex = Random.Range (highScoreIndex[i], highScoreIndex [i] + sizePerChunk);
				//Debug.Log ("HighScore : " + i + " : " + spectrumIndex);
				//Debug.Log ("Spectrum Index : " + spectrumIndex);
				Vector3 previousScale = cubes [spectrumIndex].transform.localScale;
				//Vector3 offset = new Vector3 (50f, 0, 0);
				Vector3 initPos = cubes [spectrumIndex].transform.right.normalized * 100f;
				Quaternion direction = cubes [spectrumIndex].transform.rotation;
				Vector3 temp = direction.eulerAngles;
				temp.z += 90f;
				direction.eulerAngles = temp;
				Debug.Log ("InitPos : " + i + " : " + cubes [spectrumIndex].transform.right.normalized);
				Instantiate (enemyPrefab, initPos, direction);
			}
		}
	}

	void CreateSpectrum(){
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

	void TranformSpectrum(){
		spt = AudioListener.GetSpectrumData (1024, 0, FFTWindow.Hamming);
		for (int i = 0; i < numberOfObjects; i++) {
			Vector3 previousScale = cubes [i].transform.localScale;
			Vector3 tempPos = orgPos[i];
			//previousScale.x = Mathf.MoveTowards (previousScale.x, spectrum [i] * 200, spectrum [i] * 200);
			previousScale.x = Mathf.Lerp (previousScale.x, spt [i] * 200, Time.deltaTime * 30);
			tempPos = orgPos[i] + cubes [i].transform.right * (previousScale.x / 2.0f + orgScale.x / 2.0f);
			cubes [i].transform.localScale = previousScale;
			cubes [i].transform.position = tempPos;
		}
	}

	void CreateEnemyByChunk(){
		
	}
}
