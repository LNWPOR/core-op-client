using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject[] player;		//Public variable to store a reference to the player game object


	private Vector3 offset;			//Private variable to store the offset distance between the player and camera

	int playerNumber = 0;
	// Use this for initialization
	void Start () 
	{
		// playerNumber = UserManager.Insntance.userData.playerNumber;

		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		
		if(playerNumber == 0){
			offset = transform.position - player[0].transform.position;
		}else if(playerNumber == 1){
			offset = transform.position - player[1].transform.position;
		}
		// offset = transform.position - player.transform.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		// transform.position = player.transform.position + offset;

		if(playerNumber == 0){
			transform.position = player[0].transform.position + offset;
		}else if(playerNumber == 1){
			transform.position = player[1].transform.position + offset;
		}
	}
}
