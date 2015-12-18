using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UserPlayer : Player {

	public int health;
	public int moves;

	public bool playerSelected;

	public int tempCount;
	public Vector3 tempVector;

	public bool allowedToMove;

	public float tempPositionX;
	public float tempPositionY;

	public int playerNumber;

	public List<Vector3> markedTiles;

	public List<Vector3> specificTiles;

	//public Vector3 moveDestination;


	// Use this for initialization
	void Start ()
	{

		markedTiles = new List<Vector3>();
		
		markedTiles.Add (transform.position);

		specificTiles.Add (transform.position);

		//playerNumber = GameManager.instance.players [UserPlayer];

		moves = 3;
		health = 1;
		allowedToMove = false;
		tempCount = 0;

		if (transform.position == GameManager.instance.startingVector1 || transform.position == GameManager.instance.startingVector2) {
			GameManager.instance.tilesListBothPlayers.Add(transform.position);
			if (GameManager.instance.currentPlayerIndex == 0){
				GameManager.instance.playerTiles1.Add (transform.position);
			}
			if (GameManager.instance.currentPlayerIndex == 1){
				GameManager.instance.playerTiles2.Add (transform.position);
			}
		}

	}

	// Update is called once per frame
	void Update ()
	{
		if (specificTiles.Contains (transform.position) == false) {
			specificTiles.Add (transform.position);
		}
		if (health == 0) {
			//Destroy(this, 1,0f)
		}

		/*if (tempCount > 10) {
			tempCount = 0;
		}   tempCount++;
		tempVector = markedTiles.ToList()[tempCount];
		print (tempVector);
		print (markedTiles);*/

		//if (GameManager.instance.movingPlayer == true){
			//transform.position = Vector3.MoveTowards(transform.position, Tile.instance.transform.position, 0.5f);
		//}

		//tempPositionX = transform.localPosition.x;
		//tempPositionY = transform.localPosition.z;

	}

	public static bool notAI (){
		return true;
	}

	public override void TurnUpdate ()
	{
		health = markedTiles.Count;
		//transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.red;
		//Graphics.DrawTexture(new Rect(200, 200, 200, 200), turnTexture);
			if (moves <= 0) {

				//transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.white;
				//GameManager.instance.tilesListBothPlayers.AddRange(markedTiles.ToList());
				GameManager.instance.NextTurn();
			}


		base.TurnUpdate ();
	}
	void OnMouseEnter() {
		playerSelected = true;
	}
	void OnMouseExit(){
		playerSelected = false;
	}
	void OnMouseDown(){
		if (playerSelected = true && GameManager.instance.currentPlayerPosition != transform.position) {
			GameManager.instance.currentPlayerIndex = playerNumber;
		}
	}


}

