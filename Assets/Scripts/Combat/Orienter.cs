using UnityEngine;
using System.Collections;

public class Orienter : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (OverworldManager.instance.isometricBattle == true) {

			transform.rotation = Quaternion.Euler(30,45,0);

			transform.position = new Vector3 (0,0,-9);

		} else {
			transform.rotation = Quaternion.Euler(90,0,0);
			transform.position = new Vector3 (12,5,-9);
		}
	}
}
