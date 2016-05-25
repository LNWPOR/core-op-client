using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

	private static RoomManager 		_instance;

	public RoomData roomData;

	public static RoomManager Instance{
		get{
			if(_instance == null ){
				_instance = new GameObject("_RoomManager").AddComponent<RoomManager>();
			}

			return _instance;
		}
	}
}
