﻿using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnMouseEnter()
	{
		transform.GetComponent<Renderer>().material.color = Color.blue;
		Debug.Log(gridPosition);

	}

	void OnMouseExit()
	{
		transform.GetComponent<Renderer>().material.color = Color.white;

	}

	void OnMouseDown()
	{
		if (!GameManager.instance.movingPlayer)
		{
			GameManager.instance.MoveCurrentPlayer(this);
			GameManager.instance.movingPlayer = true;

		}
	}

}
