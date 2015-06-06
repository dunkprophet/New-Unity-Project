﻿using UnityEngine;
using System.Collections;

public class OverworldPlayer : MonoBehaviour {
	
	//float speed = 3.0f;

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3 (Input.mousePosition, 0, 0);
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
		Debug.Log(transform.position);

	}
}
