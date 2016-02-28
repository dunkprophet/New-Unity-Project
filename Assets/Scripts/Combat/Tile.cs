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

	public float markHeight = 0.65f;

	public bool enteredTile = false;

	public bool bumpTileCreated = false;
	public bool bumpAttackTileCreated = false;
	public bool bumpMarkedTileCreated = false;
	public bool bumpMarkedAITileCreated = false;

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

		if (bumpTileCreated == true && GameManager.instance.currentPlayerPosition == transform.position){
			Destroy (transform.FindChild ("movableTilePrefab(Clone)").gameObject);
			bumpTileCreated = false;
		} 
		if (bumpTileCreated == true && GameManager.instance.tilesListBothPlayers.Contains(transform.position) == true && GameManager.instance.players[GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains(transform.position) == false){
			Destroy (transform.FindChild ("movableTilePrefab(Clone)").gameObject);
			bumpTileCreated = false;
		} 
		if (bumpTileCreated == true && GameManager.instance.tilesListAllAIPlayers.Contains(transform.position) == true){
			Destroy (transform.FindChild ("movableTilePrefab(Clone)").gameObject);
			bumpTileCreated = false;
		}

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
			tempVector = GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().transform.position;
			tempInt = GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().moves;

			/*if (GameManager.instance.tilesListBothPlayers.Contains (transform.position)) {
				GetComponent<Renderer> ().material.mainTexture = GameManager.instance.marked;
			} 
			else {
				GetComponent<Renderer> ().material.mainTexture = GameManager.instance.notMarked;
			} */
			if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().hasAttacked == true) {
				attackableTile = false;
			}


//CHANGE THIS TO PREFABS OR SOMETHING, NOT TEXTURE CHANGERS ---------------------------------------------------------------------------------------------

			//MARKEDTILES
			if (GameManager.instance.tilesListBothPlayers.Contains (transform.position)) {
				if (bumpMarkedTileCreated == false){
					createMarkedMark ();
				}
			} else if (bumpMarkedTileCreated == true){
				Destroy (transform.FindChild ("markedTilePrefab(Clone)").gameObject);
				bumpMarkedTileCreated = false;
			}
			//AI MARKEDTILES
			if (GameManager.instance.tilesListAllAIPlayers.Contains (transform.position)) {
				if (bumpMarkedAITileCreated == false){
					createMarkedAIMark ();
				}
			} else if (bumpMarkedAITileCreated == true){
				Destroy (transform.FindChild ("markedAITilePrefab(Clone)").gameObject);
				bumpMarkedAITileCreated = false;
			}
				

			//MOVETILES
			if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().attacking == false) {
				if (tilePosition == new Vector3 (tempVector.x + tempInt, 0, tempVector.z) || tilePosition == new Vector3 (tempVector.x, 0, tempVector.z + tempInt) || Vector3.Distance (tempVector, tilePosition) <= tempInt - 0.5 || tilePosition == new Vector3 (tempVector.x, 0, tempVector.z - tempInt) || tilePosition == new Vector3 (tempVector.x - tempInt, 0, tempVector.z)) {

					if (bumpTileCreated == false) {
						print ("Spawning Mark");
						if (GameManager.instance.currentPlayerPosition != transform.position){
							createMoveMark ();
						}
						
					} else if (bumpTileCreated == true && GameManager.instance.currentPlayerPosition == transform.position){
						Destroy (transform.FindChild ("movableTilePrefab(Clone)").gameObject);
						bumpTileCreated = false;
					} if (bumpTileCreated == true && GameManager.instance.tilesListBothPlayers.Contains(transform.position) == true && GameManager.instance.players[GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains(transform.position) == false){
						Destroy (transform.FindChild ("movableTilePrefab(Clone)").gameObject);
						bumpTileCreated = false;
					} if (bumpTileCreated == true && GameManager.instance.tilesListAllAIPlayers.Contains(transform.position) == true){
						Destroy (transform.FindChild ("movableTilePrefab(Clone)").gameObject);
						bumpTileCreated = false;
					}


				} else if (bumpTileCreated == true) {
					Destroy (transform.FindChild ("movableTilePrefab(Clone)").gameObject);
					bumpTileCreated = false;

					//DELETE TILE
				} else if (bumpTileCreated == true && GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().moves == 0) {
					Destroy (transform.FindChild ("movableTilePrefab(Clone)").gameObject);
					bumpTileCreated = false;
				}
			}

			//ATTACKTILES
			if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().attacking == true && attackableTile == true) {
				if (bumpAttackTileCreated == false) {
					print ("Spawning Mark");
					if (GameManager.instance.currentPlayerPosition != transform.position){
						createAttackMark ();
					}
				}
			} 
			if (bumpAttackTileCreated == true && GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().attacking == false) {
				Destroy (transform.FindChild ("attackableTilePrefab(Clone)").gameObject);
				bumpAttackTileCreated = false;
				
				//DELETE TILE
			}
		}
	}

	void OnMouseExit() 
	{
		movableTile = false;
	}

	void OnMouseEnter()
	{

		if (GameManager.instance.matchStarted == true && GameManager.instance.gamePaused == false) {
			if (Vector3.Distance (GameManager.instance.currentPlayerPosition, transform.position) < 1.1f) {

				if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == false && GameManager.instance.tilesListBothPlayers.Contains (tilePosition) == false) {
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
				if (GameManager.instance.tilesListAllAIPlayers.Contains (tilePosition) == true) {
					movableTile = false;
				}
			}
		}

	}


	void createMarkedMark(){
		GameObject mark; 
		mark = (GameObject)Instantiate (
			GameManager.instance.markedTilePrefab,
			new Vector3(transform.position.x, 0.5f,transform.position.z),
			Quaternion.Euler (new Vector3 ()));
		mark.transform.parent = transform;
		bumpMarkedTileCreated = true;
	}
	void createMarkedAIMark(){
		GameObject mark; 
		mark = (GameObject)Instantiate (
			GameManager.instance.markedAITilePrefab,
			new Vector3(transform.position.x, 0.5f,transform.position.z),
			Quaternion.Euler (new Vector3 ()));
		mark.transform.parent = transform;
		bumpMarkedAITileCreated = true;
	}
	void createMoveMark(){
		GameObject mark; 
		mark = (GameObject)Instantiate (
			GameManager.instance.movableTilePrefab,
			new Vector3(transform.position.x, 0.65f,transform.position.z),
			Quaternion.Euler (new Vector3 ()));
		mark.transform.parent = transform;
		bumpTileCreated = true;
		OnMouseEnter ();
	}
	void createAttackMark(){
		GameObject mark; 
		mark = (GameObject)Instantiate (
			GameManager.instance.attackableTilePrefab,
			new Vector3(transform.position.x, 0.65f,transform.position.z),
			Quaternion.Euler (new Vector3 ()));
		mark.transform.parent = transform;
		bumpAttackTileCreated = true;
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
				GameManager.instance.attackDone(tilePosition);
				
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
