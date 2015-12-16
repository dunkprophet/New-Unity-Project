﻿using UnityEngine;
using System.Collections;

public class AIPlayer : Player {

	//int moves = 3;
	//int movesPerMove = 1;
	
	public Vector3 moveDestination;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public override void TurnUpdate ()
	{
		if (Vector3.Distance(moveDestination, transform.position) > 0.1f)
		{
			GameManager.instance.movingPlayer = true;
			transform.position += (moveDestination -transform.position).normalized * moveSpeed * Time.deltaTime;
			
			if (Vector3.Distance(moveDestination, transform.position) <= 0.1f)
			{
				transform.position = moveDestination;
				GameManager.instance.NextTurn();

			}
		} else
		{
			moveDestination = new Vector3(0 - Mathf.Floor(GameManager.instance.mapSize/2),0, -0 + Mathf.Floor(GameManager.instance.mapSize/2));

		}
		
		base.TurnUpdate ();
	}
}
