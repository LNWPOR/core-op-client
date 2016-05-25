using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserManager : MonoBehaviour {

	// public enum Player
	// {
	// 	player1,player2,guest
	// }
	private static UserManager 		_instance;

	public 			UserData 			userData;
	// public 			List<UserData>		clientsList;
	// public 			bool				controlAvariabe = false;
	// public			Player				player;

	public static UserManager Instance{
		get{
			if(_instance == null ){
				_instance = new GameObject("_UserManager").AddComponent<UserManager>();
			}

			return _instance;
		}
	}

}
