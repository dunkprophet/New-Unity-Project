using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour {

	private Vector3 ResetCamera;
	private Vector3 Origin;
	private Vector3 Diference;
	private static bool Drag=false;
	void Start () {
		ResetCamera = Camera.main.transform.position;
	}
	void LateUpdate () {
		if (Input.GetMouseButton (0) && IntroScript.sceneStarting == true) {
			Diference=(Camera.main.ScreenToWorldPoint (Input.mousePosition))- Camera.main.transform.position;
			if (Drag==false){
				Drag=true;
				Origin=Camera.main.ScreenToWorldPoint (Input.mousePosition);
			}
		} else {
			Drag=false;
		}
		if (Drag==true){
			Camera.main.transform.position = Origin-Diference;
		}
		//RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
		if (Input.GetMouseButton (1)) {
			Camera.main.transform.position=ResetCamera;
		}
	}
}
