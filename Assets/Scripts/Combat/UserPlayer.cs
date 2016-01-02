using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UserPlayer : Player {

	public int health;
	public int moves;
	public int maxHealth;

	public bool playerSelected;

	public bool doingItOnce;

	public int tempCount;
	public Vector3 tempVector;
	public Player tempPlayer;

	public GameObject player;

	public bool allowedToMove;
	public bool hasAttacked;
	public bool noMovesSelected;
	public bool attacking;

	public float tempPositionX;
	public float tempPositionY;

	public int tempInt;

	public float attackRange;
	public int attackDamage;

	public int playerNumber;

	public List<Vector3> markedTiles;

	public List<Vector3> markedTilesLastTurn;
	public Vector3 positionLastTurn;

	public List<Vector3> specificTiles;

	//public Vector3 moveDestination;


	// Use this for initialization
	void Start ()
	{
		//Vector3.zero = GameManager.instance.transform.position;

		markedTiles = new List<Vector3>();
		
		markedTilesLastTurn = new List<Vector3>();
		
		markedTiles.Add (transform.position);

		GameManager.instance.tilesListBothPlayers.Add (transform.position);

		specificTiles.Add (transform.position);

		//playerNumber = GameManager.instance.players [UserPlayer];

		doingItOnce = false;
		hasAttacked = false;
		noMovesSelected = false;
		attacking = false;
		
		health = 1;
		allowedToMove = false;
		tempCount = 0;

		positionLastTurn = transform.position;
		markedTilesLastTurn = markedTiles;

	}

	// Update is called once per frame
	void Update ()
	{

		if (GameManager.instance.matchStarted == true) {
			
			if (health == 0) {
				Destroy(this.gameObject);
			}
			if (doingItOnce == false) {
				Player player;
				player = this;
				playerNumber = GameManager.instance.players.IndexOf(player);
				doingItOnce = true;
			}

		health = markedTiles.Count;

			if (attacking == true){
				GameManager.instance.attackDamage = attackDamage;
			}
			if (markedTiles.Contains(GameManager.instance.attackPosition)){
				GameManager.instance.attackHit = false;
				GameManager.instance.attackDamage = tempInt;
				while (tempInt > 0){
					tempVector = markedTiles[0];
					markedTiles.RemoveAt(0);
					GameManager.instance.tilesListAllAIPlayers.Remove(tempVector);
					tempInt--;
				}
				if (health <= 0){
					Destroy(this.gameObject);
				}
				GameManager.instance.attackPosition = new Vector3(-1,-1,-1);
			}



			/*if (markedTiles.Contains (transform.position) == false) {
				
				markedTiles.Add(transform.position);
				
			}*/



		if (playerNumber == GameManager.instance.currentPlayerIndex) {
			transform.FindChild("Sprite").GetComponent<SpriteRenderer>().sprite = GameManager.instance.selected;
			//transform.GetComponent<Renderer>().material.mainTexture = GameManager.instance.selected;
		}
		if (playerNumber != GameManager.instance.currentPlayerIndex) {
			transform.FindChild("Sprite").GetComponent<SpriteRenderer>().sprite = GameManager.instance.notSelected;
		}

		if (GameManager.instance.turnChange == true) {
			//Fix this
			tempPlayer = GameManager.instance.players[playerNumber];
			GameManager.instance.playersThatCanMove.Add(tempPlayer);
			positionLastTurn = transform.position;
			markedTilesLastTurn = markedTiles;
			hasAttacked = false;
			noMovesSelected = false;
			//print ("TurnChange Works");
			//print (playerNumber);
			if (GameManager.instance.players.Count == GameManager.instance.playersThatCanMove.Count){
				GameManager.instance.turnChange = false;
			}
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
	}

	public static bool notAI (){
		return true;
	}

	public override void TurnUpdate ()
	{
		//transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.red;
		//Graphics.DrawTexture(new Rect(200, 200, 200, 200), turnTexture);
		
		if (moves <= 0 && hasAttacked == true/*&& noMovesSelected == false*/) {
			//transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.white;
			//GameManager.instance.tilesListBothPlayers.AddRange(markedTiles.ToList());
			GameManager.instance.NextProgram();
		}
		if (moves <= 0 && hasAttacked == false) {
			GameManager.instance.attack ();
		}

		if (attacking == true) {
			
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

		if (GameManager.instance.matchStarted == true) {
			if (playerSelected == true) {
				/*if (moves <= 0){
					noMovesSelected = true;
					print ("nomoves works");
				}
				if (moves > 0) {
					noMovesSelected = false;
				}*/
				GameManager.instance.currentPlayerIndex = playerNumber;
			}
		}
	}


}

