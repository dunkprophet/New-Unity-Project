using UnityEngine;
using System.Collections;

	public class Camera_follow : MonoBehaviour {

	public float speed;

	public bool gameStart;

	public float cameraPositionY;

	// Use this for initialization
	void Start () {
		bool gameStart = false;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 RunnerPOS = GameObject.Find("Runner").transform.transform.position;

		//if (gameStart == true) {
			GameObject.Find("Camera").transform.position = new Vector3(RunnerPOS.x - 10, RunnerPOS.y + 8, RunnerPOS.z - 10);
		//}

		/*if (gameStart == false) {
			cameraPositionY = transform.position.y;
			cameraPositionY = transform.position.y+ 0.01f;
			if (transform.position.y > 0) {gameStart = true;}
		}*/
	}
}
