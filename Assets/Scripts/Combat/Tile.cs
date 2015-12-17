using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {


	public Vector2 gridPosition = Vector2.zero;
	private bool movableTile;
	public bool tileMarked;
	public Vector3 tilePosition;
	public bool tileHoldingMark;
	public Vector3 tempVector;
	public int blue;

	// Use this for initialization
	void Start ()
	{
		tilePosition = transform.position;
		movableTile = false;
		//GameManager.instance.tiles.Add(tilePosition);
		//Tile tile;
		GameManager.instance.tiles.Add(transform.position);
	}
	
	// Update is called once per frame
	void Update ()
	{

		//transform.GetComponent<Renderer>().material.color = Color.white;

		/*if (GameManager.instance.markedTiles.Count >= 4) {
			if (GameManager.instance.markedTiles.Peek() == transform.localPosition){
				transform.GetComponent<Renderer>().material.color = Color.white;
			}
			GameManager.instance.markedTiles.Dequeue();
		}*/
		/*if (tileMarked == true) {
			transform.GetComponent<Renderer>().material.color = Color.green;
		}*/

		if (GameManager.instance.currentPlayerPosition == tilePosition) {
			tileMarked = true;
		}/*

		/*if (Vector3.Distance (transform.position, GameManager.instance.currentPlayerPosition) < 1) {
			transform.GetComponent<Renderer>().material.color = Color.green;
		}*/


	}

	public void AddToQueue(){

	}

	public void MakeMeWhite(){
		transform.GetComponent<Renderer>().material.color = Color.white;
	}
	public void MakeMeGreen(){
		transform.GetComponent<Renderer> ().material.color = Color.green;
	}
	public void MakeMeRed(){
		transform.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseEnter()
	{
		if (Vector3.Distance (GameManager.instance.currentPlayerPosition, transform.position) < 1.1f) {
			movableTile = true;
		}
	}

	void OnMouseExit()
	{
		movableTile = false;
	}
	void OnMouseDown (){
		GameManager.instance.movingPlayer = false;

		if (tilePosition != GameManager.instance.currentPlayerPosition && movableTile == true) {
			GameManager.instance.MoveCurrentPlayer(tilePosition);
		}
	}
}
