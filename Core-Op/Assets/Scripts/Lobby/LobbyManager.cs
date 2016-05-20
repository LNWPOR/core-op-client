using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviour {

	// public enum Player
	// {
	// 	player1,player2,guest
	// }
	private static LobbyManager _instance;

	// public 			UserData 			userData;
	// public 			List<UserData>		clientsList;
	// public 			bool				controlAvariabe = false;
	// public			Player				player;

	public List<UserData> _usersData;

	public static LobbyManager Instance{
		get{
			if(_instance == null ){
				_instance = new GameObject("_LobbyManager").AddComponent<LobbyManager>();
			}

			return _instance;
		}
	}
}
