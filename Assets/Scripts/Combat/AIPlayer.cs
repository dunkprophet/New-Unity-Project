using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIPlayer : Player {

	public static bool AI = true;
	public int moves = 3;
	public Vector3 target;
	public Vector3 tempVector;

	public List<Vector3> markedTiles;
	
	//public Vector3 moveDestination;
	public Vector3 currentAIPlayerPosition;

	public bool attacking;
	public bool hasAttacked;
	public bool targetFound;
	public bool canMove;
	public bool pathFound;

	public List<Vector3> AImovement = new List<Vector3>();
	public List<Vector3> AImovementSecondary = new List<Vector3>();

	public int tempInt;
	public int tempInt1;
	public int tempInt2;
	public float tempFloat1;
	public float tempFloat2;
	public int moveTimer;

	public int maxHealth;

	public int attackDamage;
	public float attackRange;

	public int health;

	public int tileX;
	public int tileY;

	public GameManager map;

	public List<Node> currentPath = null;

	int moveSpeed = 2;
	float remainingMovement=2;

	/// <summary>
	/// ----------------------------------------------------------------PATHFINDING STUFF
	/// </summary>





	///
	///------------------------------------------------------------------END OF PATHFINDING STUFF


	// Use this for initialization
	void Start ()
	{
		//Vector3.zero = GameManager.instance.transform.position;
		moves = 3;
		attacking = false;
		canMove = false;
		pathFound = false;

		//GameManager.instance.tilesListAllAIPlayers.Add (transform.position);
		currentAIPlayerPosition = transform.position;

		tempInt = 0;
		moveTimer = 0;

	}
	
	// Update is called once per frame
	void Update ()
	{
		tempInt = (int)transform.position.x;
		tileX = tempInt;
		tempInt = (int)transform.position.z;
		tileY = tempInt;
		if (markedTiles.Count > maxHealth) {
			tempVector = markedTiles[0];
			GameManager.instance.tilesListAllAIPlayers.Remove(tempVector);
			markedTiles.RemoveAt(0);
		}

		currentAIPlayerPosition = transform.position;

		//Act2 - Going after the target and coloring tiles
		GameManager.instance.AIact = 2;

		//If the tile beneath the AI is not in its list, then it will be added.
		if (markedTiles.Contains (transform.position) == false) {

			markedTiles.Add(transform.position);
			GameManager.instance.tilesListAllAIPlayers.Add (transform.position);

		}
		//moveTimer++;
		//Here, when target is found, a path is found.
		if (targetFound == true) {

			tempFloat1 = target.x;
			tempFloat2 = target.z;
			tempInt1 = (int)tempFloat1;
			tempInt2 = (int)tempFloat2;

			//The great search -----------------------------------------------------------
			GameManager.instance.GeneratePathTo (tempInt1, tempInt2, transform.position);
			//----------------------------------------------------------------------------

			if (currentPath != null) {
				pathFound = true;
				print ("Path Found!");
			}
			if (Vector3.Distance (target, transform.position) < attackRange){
				print ("Attackmode for AI activated");
				attacking = true;
				moves = 0;
			}
			if (currentPath != null && attacking == false) {
				int currNode = 0;

				while (currNode < currentPath.Count-1 && moves > 0) {
					print (currentPath [currNode]);
					Vector3 moveLocation = new Vector3 (currentPath [currNode].x, 0, currentPath [currNode].y);
					transform.position = moveLocation;
					if (markedTiles.Contains(transform.position) == false){
						markedTiles.Add (transform.position);
						GameManager.instance.tilesListAllAIPlayers.Add (transform.position);
					}
					currNode++;
					moves--;
					if (Vector3.Distance (target, transform.position) < attackRange) {
						attacking = true;
						moves = 0;
					}
				}
			}
			if (attacking == true){
				print ("AI looking for tile to attack");
				if (GameManager.instance.tilesListBothPlayers.Contains(new Vector3(transform.position.x+1,0,transform.position.z))){
					GameManager.instance.attackPosition = (new Vector3(transform.position.x+1,0,transform.position.z));
					attacking = false;
				}
				if (GameManager.instance.tilesListBothPlayers.Contains(new Vector3(transform.position.x-1,0,transform.position.z))){
					GameManager.instance.attackPosition = (new Vector3(transform.position.x-1,0,transform.position.z));
					attacking = false;
				}
				if (GameManager.instance.tilesListBothPlayers.Contains(new Vector3(transform.position.x,0,transform.position.z+1))){
					GameManager.instance.attackPosition = (new Vector3(transform.position.x,0,transform.position.z+1));
					attacking = false;
				}
				if (GameManager.instance.tilesListBothPlayers.Contains(new Vector3(transform.position.x,0,transform.position.z-1))){
					GameManager.instance.attackPosition = (new Vector3(transform.position.x,0,transform.position.z-1));
					attacking = false;
					hasAttacked = true;
				} else {
					attacking = false;
					hasAttacked = true;
				}
			}

			//GameManager.instance.findGoodTile = true;

		}
		/*if (pathFound == true && moves > 0) {
			print ("PathFound!");
			tempVector = new Vector3 (currentPath[0].x, 0, currentPath[0].y);
			currentPath.RemoveAt(0);
			transform.position = tempVector;
			moves--;
		}*/

		if (moves <= 0) {
			pathFound = false;
		}

		//When moves are empty, the next program is called for,
		//End of act 2
		if (moves <= 0) {
			print ("It senses that it is out of moves");
			targetFound = false;
			GameManager.instance.AInextProgram = true;
		}

		health = markedTiles.Count;

		if (attacking == true){
			GameManager.instance.attackDamage = attackDamage;
			//AI ATTACK HERE?
		}

		if (markedTiles.Contains(GameManager.instance.attackPosition)){
			takingDamage();
		}

		if (health <= 0){
			Destroy(this.gameObject);
		}
	}
	public void takingDamage(){
		tempInt = GameManager.instance.attackDamage;
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
