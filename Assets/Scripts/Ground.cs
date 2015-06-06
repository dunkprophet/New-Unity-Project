using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {
	RaycastHit rayHit;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = camera.ScreenPointToRay(new Vector3(200, 200, 0));
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
	}

	void OnMouseDown(){

		if(Physics.Raycast(ray,out rayHit, 100)){
			if (rayHit.rigidbody != null){
			OverworldPlayer.instance.moveDestination[0] = rayHit.point[0];
			OverworldPlayer.instance.moveDestination[1] = rayHit.point[1];
			OverworldPlayer.instance.moveZig();
			}
		}

	}
}
