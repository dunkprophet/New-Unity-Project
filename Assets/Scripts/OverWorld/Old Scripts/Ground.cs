using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {
	new public Camera camera;
	RaycastHit rayHit;
	Ray ray;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnMouseDown(){
		//Cast a ray from the camera to the ground at the mouse.

		ray = camera.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray,out rayHit)){
			//Tell the lazy bum to go the place
			OverworldPlayer.instance.moveDest(rayHit.point[0],rayHit.point[2]);
			OverworldPlayer.instance.moveZig();
		
		}

	}
}
