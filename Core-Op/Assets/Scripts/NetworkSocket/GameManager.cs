using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public enum Player
	{
		player1,player2,guest
	}
	private static GameManager 		_instance;

	public 			UserData 			userData;
	public 			List<UserData>		clientsList;
	public 			bool				controlAvariabe = false;
	public			Player				player;

	public static GameManager Instance{
		get{
			if(_instance == null ){
				_instance = new GameObject("_GameManager").AddComponent<GameManager>();
			}

			return _instance;
		}
	}

}
