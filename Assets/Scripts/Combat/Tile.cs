using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Tile : MonoBehaviour {

	public static int targetDistance;

	public Vector2 gridPosition = Vector2.zero;
	private bool movableTile;
	public bool tileMarked;
	public bool attackableTile;
	public Vector3 tilePosition;
	public bool tileHoldingMark;
	public Vector3 tempVector;
	public int tempInt;
	public int blue;

	public List<Vector3> nearbyTiles = new List<Vector3>();

	public bool canMove;

	// Use this for initialization
	void Start ()
	{

		//GameManager.instance.tilesObj.Add (this.gameObject);
		GameManager.instance.tilesPos.Add (transform.position);
		//Vector3.zero = GameManager.instance.transform.position;
		canMove = true;
		tilePosition = transform.position;
		movableTile = false;
		attackableTile = false;
		//GameManager.instance.tiles.Add(tilePosition);
		//Tile tile;

	}

	// Update is called once per frame
	void Update ()
	{
		if (movableTile == true) {
			if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer>().moves <= 0){
				movableTile = false;
			}
		}

		if (GameManager.instance.tileBool == true) {
			GameManager.instance.tiles [(int)transform.position.x, (int)transform.position.z] = 0;
		}

		if (GameManager.instance.matchStarted == true) {
			if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().attacking == true) {
				if (Vector3.Distance (GameManager.instance.currentPlayerPosition, transform.position) < GameManager.instance.currentPlayerAttackRange) {
					attackableTile = true;
				}
			}
			tempVector = GameManager.instance.players[GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer>().transform.position;
			tempInt = GameManager.instance.players[GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer>().moves;

			if (GameManager.instance.tilesListBothPlayers.Contains (transform.position)) {
				GetComponent<Renderer> ().material.mainTexture = GameManager.instance.marked;
			} 
			else if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().attacking == true && attackableTile == true) {
				GetComponent<Renderer> ().material.mainTexture = GameManager.instance.attackTexture;
			} 
			else {
				GetComponent<Renderer> ().material.mainTexture = GameManager.instance.notMarked;
			} 
			if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().hasAttacked == true){
				attackableTile = false;
			}
			if (GameManager.instance.tilesListAllAIPlayers.Contains (transform.position)){
				GetComponent<Renderer> ().material.mainTexture = GameManager.instance.AImarked;
			}

//CHANGE THIS TO PREFABS OR SOMETHING, NOT TEXTURE CHANGERS ---------------------------------------------------------------------------------------------

			if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer>().attacking == false){
				if (tilePosition == new Vector3(tempVector.x+tempInt, 0, tempVector.z) || tilePosition == new Vector3(tempVector.x, 0, tempVector.z+tempInt) || Vector3.Distance(tempVector, tilePosition) <= tempInt-0.5 || tilePosition == new Vector3(tempVector.x, 0, tempVector.z-tempInt) || tilePosition == new Vector3(tempVector.x-tempInt, 0, tempVector.z) ){
					//Create TILE
					/*Object movableTilePrefab;
					movableTilePrefab =
					Instantiate (
							GameManager.instance.movableTilePrefab,
							new Vector3 (tilePosition.x, tilePosition.y, tilePosition.z),
							Quaternion.Euler (new Vector3 ()));*/
				} else {
					//DELETE TILE
				}
			}

			//If AI trying to find good tile
			if (GameManager.instance.findGoodTile == true){

			}

			/*if (GameManager.instance.AImoving == true && GameManager.instance.tilesListBothPlayers.Contains(transform.position) == false){
				if (Vector3.Distance (GameManager.instance.AIplayers [GameManager.instance.currentAIPlayerIndex].GetComponent<AIPlayer> ().currentAIPlayerPosition, transform.position) < 1.1f){
					print ("AI finds several tiles!");
					for (float y = 0; y < 2f; y=y+0.1f){
					if (GameManager.instance.tempFloat > y + Vector3.Distance (tilePosition, GameManager.instance.AIplayers[GameManager.instance.currentAIPlayerIndex].GetComponent<AIPlayer> ().target) && GameManager.instance.AImoving == true){
						print("AI finds a tile");
						
						GameManager.instance.MoveCurrentAIPlayer(tilePosition);
						}
					}
				}
				else {
					GameManager.instance.MoveCurrentAIPlayer(GameManager.instance.AIplayers [GameManager.instance.currentAIPlayerIndex].GetComponent<AIPlayer> ().transform.position);
				}
			}*/



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
		if (GameManager.instance.matchStarted == true && GameManager.instance.gamePaused == false) {
			if (Vector3.Distance (GameManager.instance.currentPlayerPosition, transform.position) < 1.1f) {

				if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == false && GameManager.instance.tilesListBothPlayers.Contains (tilePosition) == false ) {
					movableTile = true;
				}
				if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == false && GameManager.instance.tilesListBothPlayers.Contains (tilePosition) == true) {
					movableTile = false;
				} 
				if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == true && GameManager.instance.tilesListBothPlayers.Contains (tilePosition) == false) {
					movableTile = true;
				}
				if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == true) {
					movableTile = true;
				}
				if (GameManager.instance.tilesListAllAIPlayers.Contains(tilePosition) == true){
					movableTile = false;
				}
			}
		}
	}

	void OnMouseExit()
	{
		movableTile = false;
	}

	void OnMouseDown (){
		if (GameManager.instance.matchStarted == true && GameManager.instance.gamePaused == false ) {
			GameManager.instance.movingPlayer = false;

			/*if (GameManager.instance.players [GameManager.instance.lastPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition)) {
			canMove = false;
		}*/

			if (tilePosition != GameManager.instance.currentPlayerPosition && movableTile == true && canMove == true && attackableTile == false) {
				if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer>().moves > 0){
					canMove = GameManager.instance.MoveCurrentPlayer (tilePosition);
					movableTile = false;
				}
			}
			if (attackableTile == true){
				GameManager.instance.players[GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer>().attacking = false;
				if (GameManager.instance.tilesListAllAIPlayers.Contains (tilePosition) == true){
				GameManager.instance.attackPosition = tilePosition;
				if (GameManager.instance.attackPosition != tilePosition){
					GameManager.instance.attackDone(tilePosition);
				}
				} else {
					GameManager.instance.attackDone(tilePosition);
				}
			}
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
