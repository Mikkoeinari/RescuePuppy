using UnityEngine;
using System.Collections;

public class StartSceneButtons : MonoBehaviour {

	public void launchScene (string sceneToLaunch) {
        Application.LoadLevel(sceneToLaunch);
	}
}
