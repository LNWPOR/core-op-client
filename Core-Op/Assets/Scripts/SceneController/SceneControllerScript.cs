using UnityEngine;
using System.Collections;

public class SceneControllerScript : MonoBehaviour {

	public void ChangeScene(string sceneName){
		Application.LoadLevel(sceneName);
	}
}
