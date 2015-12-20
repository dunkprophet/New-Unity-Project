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
	}
	void OnMouseExit(){
		playerSelected = false;
	}
}
