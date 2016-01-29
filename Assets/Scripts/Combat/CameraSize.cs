using UnityEngine;
using System.Collections;

public class CameraSize : MonoBehaviour {

	
	public static CameraSize instance;

	void Awake(){
		instance = this;

	}
	// Use this for initialization
	void Start () {
		//Camera.Render();
		if (OverworldManager.instance.menuStarted == true){
		Camera.main.pixelRect = new Rect (Screen.width / 4 + 30, Screen.height*0.25f, Screen.width - 50 - Screen.width / 4, Screen.height * 0.72f);
		}
	}
	
	// Update is called once per frame
	void Update () {


	}
}
