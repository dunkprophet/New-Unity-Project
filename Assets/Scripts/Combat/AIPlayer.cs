using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIPlayer : Player {

	public static bool AI = true;
	public int moves = 3;
	public Vector3 target;

	public List<Vector3> markedTiles;
	
	//public Vector3 moveDestination;
	public Vector3 currentAIPlayerPosition;

	public bool attacking;
	public bool targetFound;
	public bool canMove;
	public int health;

	// Use this for initialization
	void Start ()
	{
		//Vector3.zero = GameManager.instance.transform.position;
		moves = 3;
		attacking = false;
		canMove = false;
		GameManager.instance.tilesListAllAIPlayers.Add (transform.position);
		currentAIPlayerPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{

		//Act2 - Going after the target and coloring tiles
		GameManager.instance.AIact = 2;

		//If the tile beneath the AI is not in its list, then it will be added.
		if (markedTiles.Contains (transform.position) == false) {

			markedTiles.Add(transform.position);
			GameManager.instance.tilesListAllAIPlayers.Add (transform.position);

		}

		//Here, when target is found and the AI still has moves, a suitable tile is found and moved towards.
		if (targetFound == true && moves > 0) {
			//Target is to the left
			if (target.x < transform.position.x) {

				//and to the bottom
				if (target.z < transform.position.z){
					//and the square to the left is avaliable 
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x-1, 0, transform.position.z))){
						transform.position = new Vector3(transform.position.x-1, 0, transform.position.z);
						moves--;
					}
					//and the square to the bottom is avaliable 
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x, 0, transform.position.z-1))){
						transform.position = new Vector3(transform.position.x, 0, transform.position.z-1);
						moves--;
					}
				}
				//and to the top
				if (target.z > transform.position.z){
					//and the square to the left is avaliable
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x-1, 0, transform.position.z))){
						transform.position = new Vector3(transform.position.x-1, 0, transform.position.z);
						moves--;
					}
					//and the square to the top is avaliable
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x, 0, transform.position.z+1))){
						transform.position = new Vector3(transform.position.x, 0, transform.position.z+1);
						moves--;
					}
				}
				//and straight to the left
				if (target.z == transform.position.z){
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x-1, 0, transform.position.z))){
						transform.position = new Vector3(transform.position.x-1, 0, transform.position.z);
						moves--;
					}
				}
			}
			//Target is to the right
			if (target.x > transform.position.x) {

				//and to the bottom
				if (target.z < transform.position.z){
					//and the square to the left is avaliable 
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x-1, 0, transform.position.z))){
						transform.position = new Vector3(transform.position.x+1, 0, transform.position.z);
						moves--;
					}
					//and the square to the bottom is avaliable 
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x, 0, transform.position.z-1))){
						transform.position = new Vector3(transform.position.x, 0, transform.position.z-1);
						moves--;
					}
				}
				//and to the top
				if (target.z > transform.position.z){
					//and the square to the left is avaliable
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x-1, 0, transform.position.z))){
						transform.position = new Vector3(transform.position.x+1, 0, transform.position.z);
						moves--;
					}
					//and the square to the top is avaliable
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x, 0, transform.position.z+1))){
						transform.position = new Vector3(transform.position.x, 0, transform.position.z+1);
						moves--;
					}
				}
				//and straight to the right
				if (target.z == transform.position.z){
					if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x-1, 0, transform.position.z))){
						transform.position = new Vector3(transform.position.x+1, 0, transform.position.z);
						moves--;
					}
				}
			}
			//Target is to the top
			if (target.z > transform.position.z){
				if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x, 0, transform.position.z+1))){
					transform.position = new Vector3(transform.position.x, 0, transform.position.z+1);
					moves--;
				}
			}
			//Target is to the bottom
			if (target.z < transform.position.z){
				if (GameManager.instance.tiles.Contains(new Vector3(transform.position.x-1, 0, transform.position.z-1))){
					transform.position = new Vector3(transform.position.x, 0, transform.position.z-1);
					moves--;
				}
			}

		}

		//When moves are empty, the next program is called for,
		//End of act 2
		if (moves <= 0 && targetFound == true) {
			print ("It senses that it is out of moves");
			targetFound = false;
			GameManager.instance.AInextProgram = true;
		}

		health = markedTiles.Count;
		if (health <= 0){
			Destroy(this.gameObject);
		}
	}

	/*public override void TurnUpdate ()
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
	}*/
}
