using UnityEngine;
using System.Collections;

public class TutorialArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = GameManager.instance.arrowPos;
	}
}
