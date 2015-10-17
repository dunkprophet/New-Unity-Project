using UnityEngine;
using System.Collections;

public class Camera_follow : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 RunnerPOS = GameObject.Find("Runner").transform.transform.position;
		GameObject.Find("Camera").transform.position = new Vector3(RunnerPOS.x - 10, RunnerPOS.y + 8, RunnerPOS.z - 10);
	}
}
