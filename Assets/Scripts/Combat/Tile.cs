using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Tile : MonoBehaviour {


	public Vector2 gridPosition = Vector2.zero;
	private bool movableTile;
	public bool tileMarked;
	public Vector3 tilePosition;
	public bool tileHoldingMark;
	public Vector3 tempVector;
	public int blue;

	public bool canMove;

	// Use this for initialization
	void Start ()
	{
		canMove = true;
		tilePosition = transform.position;
		movableTile = false;
		//GameManager.instance.tiles.Add(tilePosition);
		//Tile tile;

	}

	// Update is called once per frame
	void Update ()
	{
		if (GameManager.instance.tilesListBothPlayers.Contains(transform.position)) {
			GetComponent<Renderer> ().material.mainTexture = GameManager.instance.marked;
		} else {
			GetComponent<Renderer> ().material.mainTexture = GameManager.instance.notMarked;
		}
		/*if (tileMarked == true) {
			transform.GetComponent<Renderer>().material.color = Color.green;
		}
		if (tileMarked == false) {
			transform.GetComponent<Renderer>().material.color = Color.white;
		}*/
		//if (GameManager.instance.players [0].GetComponent<UserPlayer> ().markedTiles.Contains(transform.position)
		/*if (GameManager.instance.markedTiles.Count >= 4) {
			if (GameManager.instance.markedTiles.Peek() == transform.localPosition){
				transform.GetComponent<Renderer>().material.color = Color.white;
			}
			GameManager.instance.markedTiles.Dequeue();
		}*/
		/*if (tileMarked == true) {
			transform.GetComponent<Renderer>().material.color = Color.green;
		}*/

		/*if (GameManager.instance.currentPlayerPosition == tilePosition) {
			tileMarked = true;
		}*/

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

		/*if (GameManager.instance.players [GameManager.instance.lastPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition)) {
			canMove = false;
		}*/

		if (tilePosition != GameManager.instance.currentPlayerPosition && movableTile == true && canMove == true) {
			canMove = GameManager.instance.MoveCurrentPlayer(tilePosition);
			movableTile = false;
		}
	}

	public void OnGUI() {
		
		//tempRect = new Rect (500, 500, 100, 100);

		/*
		
		GUILayout.BeginArea (tempRect);
		GUILayout.BeginVertical ("Health", GUI.skin.GetStyle("box"));
		GUILayout.Label(healthString);
		if (GUILayout.Button ("Attack")) {
			//GameManager.attack();
		}
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		
		tempRect = new Rect(10, 200, 200, 200);
		
		GUILayout.BeginArea (tempRect);
		GUILayout.BeginVertical ("Health", GUI.skin.GetStyle("box"));
		GUILayout.Label(healthString);
		if (GUILayout.Button ("Attack")) {
			//GameManager.attack();
		}
		GUILayout.EndVertical ();
		GUILayout.EndArea ();*/
	}

}
