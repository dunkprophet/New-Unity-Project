using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Spawner spawner;

	public Vector3 spawnerPosition;
	public bool spawnerSelected;
	public Vector3 tempVector;

	// Use this for initialization
	void Start () {

		tempVector = transform.position;
		tempVector [1] = tempVector [1] - 0.5f;
		spawnerPosition = tempVector;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.matchStarted == true) {
			Destroy(this.gameObject);
		}
	}
	void OnMouseEnter () {
		spawnerSelected = true;
	}
	void OnMouseExit () {
		spawnerSelected = false;
	}
	void OnMouseDown () {
		if (spawnerSelected == true && GameManager.instance.matchStarted == false) {
			GameManager.instance.spawnPlayer(spawnerPosition);
		}
	}
}
