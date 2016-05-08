using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public engineController enginePart;
	public coreGunController coreGunPart;
	public jetController jetPart;
	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.


	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn ()
	{
		// If the player has no health left...
		if(isLose())
		{
			// ... exit the function.
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}

	bool isLose(){
		if (enginePart.currentHp <= 0f && coreGunPart.currentHp <= 0f && jetPart.currentHp <= 0f) {
			return true;
		}
		return false;
	}
}
