﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed =10.0f;

	public Vector3 moveDestination;

	private int moves;
	private int movesPerMove;

	void Awake ()
	{
		moveDestination = transform.position;

	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void TurnUpdate()
	{


	}
}
