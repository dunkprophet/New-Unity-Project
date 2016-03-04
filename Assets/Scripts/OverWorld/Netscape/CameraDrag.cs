using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour {

	private Vector3 ResetCamera;
	private Vector3 Origin;
	private Vector3 Difference;
	private static bool Drag=false;
	public Vector3 Position;
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
				
				/*if (Origin.x <= -40) {
					Drag = false;
					transform.position = new Vector3 (-40, transform.position.y, transform.position.z);
				}
				if (Origin.z >= 30) {
					Drag = false;
					transform.position = new Vector3 (transform.position.x, transform.position.y, 30);
				}
				if (Origin.z <= -40) {
					Drag = false;
					transform.position = new Vector3 (transform.position.x, transform.position.y, -40);
				}
				if (Origin.x >= 30) {
					Drag = false;
					transform.position = new Vector3 (transform.position.x, transform.position.y, 30);
				}
				if (Origin.y >= 55) {
					Drag = false;
					transform.position = new Vector3 (transform.position.x, 55, transform.position.z);
				}*/
				Position = Origin - Difference;
				GetComponent<Camera> ().transform.position = Position;
				//if (Position )
				/*if (GetComponent<Camera> ().ScreenToWorldPoint(Position).x > -45 && GetComponent<Camera> ().ScreenToWorldPoint(Position).x < 30){
					GetComponent<Camera> ().transform.position = new Vector3 (Position.x, GetComponent<Camera> ().transform.position.y, GetComponent<Camera> ().transform.position.z);
				}
				if (GetComponent<Camera> ().ScreenToWorldPoint(Position).y < 90 && GetComponent<Camera> ().ScreenToWorldPoint(Position).y > 70){
					GetComponent<Camera> ().transform.position = new Vector3 (GetComponent<Camera> ().transform.position.x,  Position.y, GetComponent<Camera> ().transform.position.z);
				}
				if (GetComponent<Camera> ().ScreenToWorldPoint(Position).z > -50 && GetComponent<Camera> ().ScreenToWorldPoint(Position).z < 25){
					GetComponent<Camera> ().transform.position = new Vector3 (GetComponent<Camera> ().transform.position.x,  GetComponent<Camera> ().transform.position.y, Position.z);
				}*/
			}
			//RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
			/*if (Input.GetMouseButton (1)) {
				GetComponent<Camera> ().transform.position = ResetCamera;
			}*/

			if (Input.GetKey (KeyCode.W)) {

			}

		} else if (OverworldManager.instance.netscapeOpen == false) {
			
		}
	}
}
