﻿using UnityEngine;
using System.Collections;

public class CameraMap : MonoBehaviour {

	
	public static CameraMap instance;
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
		if (OverworldManager.instance.mapOpen == true) {
			cameraPosX = 4;
		} else {
			cameraPosX = 1;
		}
		GetComponent<Camera>().pixelRect = new Rect (Screen.width / cameraPosX, Screen.height/5, Screen.width/1.48f, Screen.height * 0.72f);


	}
}