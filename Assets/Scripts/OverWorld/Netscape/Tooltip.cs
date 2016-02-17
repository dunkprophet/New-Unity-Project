using UnityEngine;
using System.Collections;

public class Tooltip : MonoBehaviour {
	private Vector3 mousePosition;
	private bool tempBool = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (OverworldManager.instance.nodeHover == true) {
			if (tempBool == false){
				OverworldManager.instance.textPrint = "";
				OverworldManager.instance.i = 0;
				OverworldManager.instance.text = OverworldManager.instance.nodeTitle;
				tempBool = true;
			}
			mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint (new Vector3 (mousePosition.x + 1, mousePosition.y - 2, mousePosition.z + 2));
			transform.position = mousePosition;
			if (OverworldManager.instance.textFlash <= 1) {
				GetComponent<TextMesh> ().text = OverworldManager.instance.textPrint + "_";
			} else {
				GetComponent<TextMesh> ().text = OverworldManager.instance.textPrint + " ";
			}
		} else if (OverworldManager.instance.netscapeOpen == true) {
			GetComponent<TextMesh>().text = "";
			tempBool = false;
		}
		
	}
}
