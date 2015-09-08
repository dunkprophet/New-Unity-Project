﻿using UnityEngine;
using System.Collections;

public class UserPlayer : Player {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void TurnUpdate ()
	{
		transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.red;

		if (Vector3.Distance(moveDestination, transform.position) > 0.1f) {
			transform.position += (moveDestination -transform.position).normalized * moveSpeed * Time.deltaTime;

			if (Vector3.Distance(moveDestination, transform.position) <= 0.1f) {

				transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.white;

				GameManager.instance.nextTurn();
				transform.position = moveDestination;
			}
		}

		base.TurnUpdate ();
	}
}