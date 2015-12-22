using UnityEngine;
using System.Collections;

public class SpriteScript : Player {
	public bool playerSelected;
	// Use this for initialization
	void Start () {
		playerSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseEnter() {
		playerSelected = true;
		print("OnmouseEnter works");
	}
	void OnMouseExit(){
		playerSelected = false;
	}
}
