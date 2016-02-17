using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour {

	private Vector3 ResetCamera;
	private Vector3 Origin;
	private Vector3 Difference;
	private static bool Drag=false;
	void Awake () {
		if (OverworldManager.instance.netscapeOpen == true) {
			ResetCamera = GetComponent<Camera> ().transform.position;
		}
	}
	void LateUpdate () {
		if (OverworldManager.instance.netscapeOpen == true && OverworldManager.instance.showNode == false) {
			if (Input.GetMouseButton (0) /*&& IntroScript.sceneStarting == true*/) {
				Difference = (GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition)) - GetComponent<Camera> ().transform.position;
				if (Drag == false) {
					Drag = true;
					Origin = GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition);
				}
			} else {
				Drag = false;
			}
			if (Drag == true) {
				GetComponent<Camera> ().transform.position = Origin - Difference;
			}
			//RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
			/*if (Input.GetMouseButton (1)) {
				GetComponent<Camera> ().transform.position = ResetCamera;
			}*/
		}
	}
}
