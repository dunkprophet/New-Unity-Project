using UnityEngine;
using System.Collections;

public class CameraNetscape : MonoBehaviour {

	
	public static CameraNetscape instance;
	public int cameraPosX = 1;

	void Awake(){
		instance = this;

	}
	// Use this for initialization
	void Start () {
		//Camera.Render();
		if (OverworldManager.instance.menuStarted == true){
		
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (OverworldManager.instance.netscapeOpen == true) {
			cameraPosX = 0;
		} else {
			cameraPosX = Screen.width;
		}
		//GetComponent<Camera>().pixelRect = new Rect (Screen.width / cameraPosX, Screen.height/5, Screen.width/1.48f, Screen.height * 0.72f);
		GetComponent<Camera>().pixelRect = new Rect (cameraPosX, 0, Screen.width, Screen.height);
	}
}
