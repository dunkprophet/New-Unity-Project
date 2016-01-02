using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	
	private GUISkin MetalGUISkin;
	public Rect tempRect;
	
	public Texture turnTexture;
	public Texture marked;
	public Texture notMarked;
	public Texture attackTexture;	
	public Texture AImarked;
	public Texture movableTile;
	public Sprite selected;
	public Sprite notSelected;
	public string healthString;
	public GameObject movableTilePrefab;

	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;

	//public int moves = 3;
	public Vector3 currentPlayerPosition;
	public Vector3 lastPosition;
	public float currentPlayerAttackRange;
	public bool movingPlayer;

	public bool turnChange;

	public bool switchMade;

	public bool spawningPlayer;

	public bool AImoving;
	public bool findGoodTile;
	public bool tileBool;

	public bool AInextProgram;
	public int AIact = 0;

	public bool matchStarted;
	public bool gamePaused;
	public List<Player> players = new List<Player>();
	public List<Player> playersThatCanMove = new List<Player>();
	public List<AIPlayer> AIplayers = new List<AIPlayer> ();

	public List<Vector3> tilesPos = new List<Vector3> ();
	public List<Tile> tilesObj = new List<Tile> ();

	//public List<Vector3> tilesVectors = new List<Vector3>();

	public List<Vector3> tilesListBothPlayers;
	public List<Vector3> tilesListAllAIPlayers;

	public List<Vector3> playerTiles1 = new List<Vector3>();
	public List<Vector3> playerTiles2 = new List<Vector3>();

	public int tempTile;
	public int tempIndex1;
	public int tempIndex2;
	public int tempIndex;
	public bool tempBool;
	public List<Vector3> tempList;
	public Vector3 tempVectorInList1;
	public Vector3 tempVectorInList2;
	public Vector3 searcherPosition = new Vector3 (0, 0, 0);
	public Player tempPlayer;
	public float tempFloat;

	public Vector3 attackPosition;
	public int attackDamage;
	public bool attackHit;

	public int round;

	public int currentPlayerIndex = 0;
	public int currentAIPlayerIndex = 0;
	public int lastPlayerIndex;

	private int tempCount;
	private Vector3 tempVector;
	public int distanceToTarget;

	public Vector3 startingVector;
	
	public TileType[] tileTypes;

	public int[,] tiles;
	Node[,] graph;

	public int mapSizeX = 50;
	public int mapSizeY = 50;

	void Awake ()
	{
		instance = this;

	}

	/// <summary>
	/// -------------------------------------------------------------------------------------------PATHFINDING STUFF
	/// </summary>
	void GenerateMapData() {
		// Allocate our map tiles
		tiles = new int[mapSizeX,mapSizeY];
		
		int x,y;
		
		// Initialize our map tiles to be grass
		for(x=0; x < mapSizeX; x++) {
			for(y=0; y < mapSizeX; y++) {
				tiles[x,y] = 2;
			}
		}
		tileBool = true;
		
	}
	public void GeneratePathfindingGraph() {
		// Initialize the array
		graph = new Node[mapSizeX,mapSizeY];
		
		// Initialize a Node for each spot in the array
		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {
				graph[x,y] = new Node();
				graph[x,y].x = x;
				graph[x,y].y = y;
			}
		}
		
		// Now that all the nodes exist, calculate their neighbours
		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {
				
				// This is the 4-way connection version:
				if(x > 0)
					graph[x,y].neighbours.Add( graph[x-1, y] );
				if(x < mapSizeX-1)
					graph[x,y].neighbours.Add( graph[x+1, y] );
				if(y > 0)
					graph[x,y].neighbours.Add( graph[x, y-1] );
				if(y < mapSizeY-1)
					graph[x,y].neighbours.Add( graph[x, y+1] );
				
				// This also works with 6-way hexes and n-way variable areas (like EU4)
			}
		}
	}
	public bool UnitCanEnterTile(int x, int y) {
		
		// We could test the unit's walk/hover/fly type against various
		// terrain flags here to see if they are allowed to enter the tile.
		tempVector = new Vector3 (x, 0, y);
		if (tilesPos.Contains (tempVector) == true) {
			return tileTypes [tiles [x, y]].isWalkable;
		} else {
			return false;
		}
	}
	public void GeneratePathTo(int x, int y, Vector3 AIPos) {
		// Clear out our unit's old path.
		AIplayers[currentAIPlayerIndex].GetComponent<AIPlayer>().currentPath = null;
		
		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
		
		// Setup the "Q" -- the list of nodes we haven't checked yet.
		List<Node> unvisited = new List<Node>();

		print (AIPos.x);
		print (AIPos.z);

		Node source = graph[
		                    (int)AIPos.x, 
		                    (int)AIPos.z
		                    ];
		
		Node target = graph[
		                    x, 
		                    y
		                    ];

		print (x);
		print (y);

		dist[source] = 0;
		prev[source] = null;
		
		// Initialize everything to have INFINITY distance, since
		// we don't know any better right now. Also, it's possible
		// that some nodes CAN'T be reached from the source,
		// which would make INFINITY a reasonable value
		foreach(Node v in graph) {
			if(v != source) {
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}
			if (tilesPos.Contains(new Vector3(v.x,0,v.y))){
				unvisited.Add(v);
			}
			if (tilesListAllAIPlayers.Contains(new Vector3(v.x,0,v.y)) == true){
				unvisited.Remove(v);
			}
			if (AIplayers[currentAIPlayerIndex].GetComponent<AIPlayer>().markedTiles.Contains(new Vector3(v.x,0,v.y)) == true){
				unvisited.Add (v);
			}

			/*if (tilesListAllAIPlayers.Contains(new Vector3(v.x,0,v.y)) == true){
				unvisited.Remove(v);
			}
			if (AIplayers[currentAIPlayerIndex].GetComponent<AIPlayer>().markedTiles.Contains(new Vector3(v.x,0,v.y)) == true){
				unvisited.Add(v);
			}
			if (tilesListBothPlayers.Contains(new Vector3(v.x,0,v.y))){
				unvisited.Remove(v);
			}*/
		}
		
		while(unvisited.Count > 0) {
			// "u" is going to be the unvisited node with the smallest distance.
			Node u = null;
			
			foreach(Node possibleU in unvisited) {
				if(u == null || dist[possibleU] < dist[u]) {
						u = possibleU;
				}
			}
			
			if(u == target) {
				break;	// Exit the while loop!
			}
			
			unvisited.Remove(u);
			
			foreach(Node v in u.neighbours) {
				//Problem here?
				float alt = dist[u] + u.DistanceTo(v);
				//float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
				if( alt < dist[v] ) {
					dist[v] = alt;
					prev[v] = u;
				}
			}
		}
		
		// If we get there, the either we found the shortest route
		// to our target, or there is no route at ALL to our target.
		
		if(prev[target] == null) {
			// No route between our target and the source
			//If distance between target and source is larger than the distance between square to its right and target
			//Square to the right is new target
			return;
			/*if (Vector3.Distance(new Vector3(x+1,0,y), new Vector3(AIPos.x,0,AIPos.y)) < Vector3.Distance(new Vector3(x,0,y), new Vector3(AIPos.x,0,AIPos.y))){
				if (tilesPos.Contains(new Vector3(x+1,0,y))){
					GeneratePathTo(x+1,y, AIPos);
				}
			}
			if (Vector3.Distance(new Vector3(x-1,0,y), new Vector3(AIPos.x,0,AIPos.y)) < Vector3.Distance(new Vector3(x,0,y), new Vector3(AIPos.x,0,AIPos.y))){
				if (tilesPos.Contains(new Vector3(x-1,0,y))){
					GeneratePathTo(x-1,y, AIPos);
				}
			}
			if (Vector3.Distance(new Vector3(x,0,y+1), new Vector3(AIPos.x,0,AIPos.y)) < Vector3.Distance(new Vector3(x,0,y), new Vector3(AIPos.x,0,AIPos.y))){
				if (tilesPos.Contains(new Vector3(x,0,y+1))){
					GeneratePathTo(x,y+1, AIPos);
				}
			}
			if (Vector3.Distance(new Vector3(x,0,y-1), new Vector3(AIPos.x,0,AIPos.y)) < Vector3.Distance(new Vector3(x,0,y), new Vector3(AIPos.x,0,AIPos.y))){
				if (tilesPos.Contains(new Vector3(x,0,y-1))){	
					GeneratePathTo(x,y-1, AIPos);
				}
			}*/

			//FIND A WAY TO MAKE THE NEST BEST THING POSSIBLE ----------------------------------------------------------------------------------!!!!
		}
		
		List<Node> currentPath = new List<Node>();
		
		Node curr = target;
		
		// Step through the "prev" chain and add it to our path
		while(curr != null) {
			currentPath.Add(curr);
			curr = prev[curr];
		}
		
		// Right now, currentPath describes a route from out target to our source
		// So we need to invert it!
		
		currentPath.Reverse();
		
		AIplayers[currentAIPlayerIndex].GetComponent<AIPlayer>().currentPath = currentPath;
	}

	/// <summary>
	/// ..........-----------------------------------------------------------END OF PATHFINDING
	/// </summary>


	void Start (){
		tileBool = false;

		GenerateMapData();
		GeneratePathfindingGraph();

		attackDamage = 0;
		attackHit = false;
		attackPosition = new Vector3 (-1,-1,-1);
		AImoving = false;
		turnChange = false;
		matchStarted = false;
		gamePaused = false;
		spawningPlayer = false;
		switchMade = false;
		AInextProgram = false;
		findGoodTile = false;
		//startingVector1 = new Vector3 (1, 0, 1);
		//startingVector2 = new Vector3 (2, 0, 1);
	
		MetalGUISkin = Resources.Load ("MetalGUISkin") as GUISkin;
		marked = Resources.Load ("Textures/markedTexture") as Texture;
		notMarked = Resources.Load ("Textures/Ground1") as Texture;
		attackTexture = Resources.Load ("Textures/attackTexture") as Texture;
		selected = Resources.Load<Sprite> ("Textures/Bug_selected");
		notSelected = Resources.Load<Sprite> ("Textures/Bug");
		AImarked = Resources.Load ("Textures/AImarked") as Texture;
		movableTile = Resources.Load ("Textures/movableTile") as Texture;
		movableTilePrefab = Resources.Load ("Prefabs/movableTilePrefab") as GameObject;
		//turnTexture = (Texture2D)Resources.Load(".png");
		//(Texture2D)Resources.Load("arrow.png");
	
		//ENEMEY SPAWNER
		AIPlayer aiplayer;
		aiplayer = 
		((GameObject)Instantiate (
			AIPlayerPrefab,
			new Vector3 (9, 0, 3),
			Quaternion.Euler (new Vector3 ()))
		 ).GetComponent<AIPlayer> ();
		
		AIplayers.Add (aiplayer);
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().attackRange = 1.1f;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().attackDamage = 2;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().maxHealth = 3;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().moves = 1;

		aiplayer = 
		((GameObject)Instantiate (
			AIPlayerPrefab,
			new Vector3 (9, 0, 6),
			Quaternion.Euler (new Vector3 ()))
		 ).GetComponent<AIPlayer> ();
	
		AIplayers.Add (aiplayer);
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().attackRange = 1.1f;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().attackDamage = 2;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().maxHealth = 3;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().moves = 1;
	
		currentPlayerIndex = 0;
	
		movingPlayer = false;
	
		round = 0;
	}
	


	public void spawnPlayer (Vector3 spawnerPosition){
		startingVector = spawnerPosition;
		spawningPlayer = true;
	}
	

	// Update is called once per frame
	void Update ()
	{
		//END CHECK FOR AI TURN
		//Act 3 - if the last AI has run its course, the round is ended, and round two begins.
		//if its not the last AI and the current
		AIact = 3;
		if (AInextProgram == true) {
			if (currentAIPlayerIndex >= AIplayers.Count - 1) {
				AInextProgram = false;
				round++;
				currentPlayerIndex = 0;
				currentAIPlayerIndex = 0;
				for (int y = 0; y < players.Count; y++) {
					players [y].GetComponent<UserPlayer> ().moves = 3;
					print ("players given moves");
				}
				for (int y = 0; y < AIplayers.Count; y++) {
					AIplayers [y].GetComponent<AIPlayer> ().moves = 3;
					print ("AI given moves");
				}
				turnChange = false;
				gamePaused = false;
			} else {
				print (currentAIPlayerIndex);
				currentAIPlayerIndex++;
				print ("currentplayerindex increase");
				print (currentAIPlayerIndex);
				AInextProgram = false;
				//And it begins again
				AITurn ();
			}
		}
		/*if (turnChange == false && players[currentPlayerIndex] == ) {
			currentPlayerIndex++;
			if (currentPlayerIndex>players.Count){
				currentPlayerIndex = 0;
			}
		}*/

		if (matchStarted == true && gamePaused == false) {
			/*tilesList = players [0].GetComponent<UserPlayer> ().markedTiles.ToList();
		tilesList2 = players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.ToList ();*/
			//if (tiles.Contains){
			//
			//}

			currentPlayerPosition = players [currentPlayerIndex].transform.position;
			players [currentPlayerIndex].TurnUpdate ();

			currentPlayerPosition = players [currentPlayerIndex].transform.position;
			//if (currentPlayerPosition = tiles.Find (currentPlayerPosition)) {
			//tiles.
			//}
			healthString = players [currentPlayerIndex].GetComponent<UserPlayer> ().health.ToString ();
			currentPlayerAttackRange = players [currentPlayerIndex].GetComponent<UserPlayer> ().attackRange;
		
			/*if (tempCount > 10) {
			tempCount = 0;
		}   tempCount++;
		tempVector = players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.ToList()[tempCount];*/

		}
	}

	void OnMouseDown()
	{
		if (movingPlayer != true)
		{
			//MoveCurrentPlayer();
		}
	}

	public void NextProgram()
	{
		playersThatCanMove.Remove (players [currentPlayerIndex]);
		//HERE IS THE PROBLEM:
		//AS ONE PLAYER IS OUT OF MOVES, THIS THING IS RUN TO TRY TO FIND A PLAYER THAT DOES HAVE MOVES(Unless the player is selected AND out of moves,
		//in which case it is allowed to undo its actions.
		/*if (currentPlayerIndex + 1 < players.Count) {
			currentPlayerIndex++;
		} */
		if (playersThatCanMove.Count > 0) {
				tempPlayer = playersThatCanMove[playersThatCanMove.Count-1];
				currentPlayerIndex = players.IndexOf(tempPlayer);
				print ("PLayerthatcanmove works");
		} else {
			gamePaused = true;
			nextTurn();
		}
		/*for (int b = 0; b < players.Count-1; b++) {
			print (b);
			if (players[b].GetComponent<UserPlayer> ().moves > 0) {
				currentPlayerIndex = b;
				print ("hELOEL");
			}
		}*/
	}

	public void AITurn(){
		print ("AI TURN BEGIN");

		//Act 1 - Finding a target
		AIact = 1;

		tempFloat = 10000;
		for (int g = 0; g < GameManager.instance.tilesListBothPlayers.Count - 1; g++) {
			print ("Search for target begun");
			if (tempFloat > Vector3.Distance (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().transform.position, GameManager.instance.tilesListBothPlayers[g])) {
				//tempFloat = distance between target and AI
				tempFloat = Vector3.Distance (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().transform.position, GameManager.instance.tilesListBothPlayers[g]);
				AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().target = GameManager.instance.tilesListBothPlayers[g];
			}
		}
		print (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().target);
		print ("Target found!");
		//End of act 1 - target found
		AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().targetFound = true;
	}
	
	public void undo(){
		/*if (players [currentPlayerIndex].GetComponent<UserPlayer> ().hasAttacked == false) {
			players [currentPlayerIndex].GetComponent<UserPlayer> ().transform.position = players [currentPlayerIndex].GetComponent<UserPlayer> ().positionLastTurn;
			players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles = players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTilesLastTurn;
			for (int x = 0; x< players[currentPlayerIndex].GetComponent<UserPlayer>().markedTiles.Count; x++) {
				tempVector = players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles [x];
				if (tempVector != players [currentPlayerIndex].GetComponent<UserPlayer> ().positionLastTurn) {
					tilesListBothPlayers.Remove (tempVector);
				}
			}
			players [currentPlayerIndex].GetComponent<UserPlayer> ().moves = 3;
			playersThatCanMove.Add (players[currentPlayerIndex]);
			gamePaused = false;
		}
		//Move player back 
	*/
	}

	public void doNothing(){
	
		players [currentPlayerIndex].GetComponent<UserPlayer> ().moves = 0;
		players [currentPlayerIndex].GetComponent<UserPlayer> ().hasAttacked = true;
	
	}
	//public int healthUpdate(){
	//	return markedTiles.Count;
	//}

	public bool MoveCurrentPlayer(Vector3 tilePosition)
	{

		lastPosition = players [currentPlayerIndex].transform.position;
		players [currentPlayerIndex].transform.position = tilePosition;

		if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Count > 3) {

			if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == false) {

				tilesListBothPlayers.Remove(players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.First());

				if (currentPlayerIndex == 0) {
					playerTiles1.Remove(players[currentPlayerIndex].GetComponent<UserPlayer>().markedTiles.First ());
				}
				if (currentPlayerIndex == 1) {
					playerTiles2.Remove(players[currentPlayerIndex].GetComponent<UserPlayer>().markedTiles.First ());
				}
				players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.RemoveAt(0);
				switchMade = false;
			}
		} 

		if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == true && switchMade == true) {
			tempList = players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles;
			tempIndex1 = tempList.IndexOf (tilePosition);
			tempList.RemoveAt (tempIndex1);
			players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles = tempList;
		}

		if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == false) {
			players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Add (tilePosition);
			
		} 

		else if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == true && switchMade == false) {
			//If you walk on your own square, then-
			tempList = players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles;

			//Index of Tile 2
			tempIndex1 = tempList.IndexOf (tilePosition);
			tempIndex2 = tempList.IndexOf (lastPosition);
			//Take out the vector d
			tempVectorInList1 = tempList [tempIndex1];
			tempVectorInList2 = tempList [tempIndex2];

			tempList [tempIndex1] = tempVectorInList2;
			tempList [tempIndex2] = tempVectorInList1;

			switchMade = true;


			players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles = tempList;

			//players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.RemoveAt(0);

			//players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles = new Queue<Vector3>(tempList);
			//queue = new Queue<T>(list);

			//players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Add (tilePosition);
		}

		if (tilesListBothPlayers.Contains (tilePosition) == false) {
			tilesListBothPlayers.Add (tilePosition);
			if (currentPlayerIndex == 0){
				playerTiles1.Add (tilePosition);
			}
			if (currentPlayerIndex == 1){
				playerTiles2.Add (tilePosition);
			}
		}

		players[currentPlayerIndex].GetComponent<UserPlayer>().moves--;

		return true;

	}
	/*public bool MoveCurrentAIPlayer(Vector3 tilePosition)
	{
		AImoving = false;
		lastPosition = AIplayers [currentAIPlayerIndex].transform.position;
		AIplayers [currentAIPlayerIndex].transform.position = tilePosition;
		
		if (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles.Count > 3) {
			
			if (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles.Contains (tilePosition) == false) {
				
				tilesListAllAIPlayers.Remove(AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles.First());
				
				/*if (currentAIPlayerIndex == 0) {
					playerTiles1.Remove(players[currentPlayerIndex].GetComponent<UserPlayer>().markedTiles.First ());
				}
				if (currentPlayerIndex == 1) {
					playerTiles2.Remove(players[currentPlayerIndex].GetComponent<UserPlayer>().markedTiles.First ());
				}
				AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles.RemoveAt(0);
				switchMade = false;
			}
		} 
		
		if (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles.Contains (tilePosition) == true && switchMade == true) {
			tempList = AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles;
			tempIndex1 = tempList.IndexOf (tilePosition);
			tempList.RemoveAt (tempIndex1);
			AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles = tempList;
		}
		
		if (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles.Contains (tilePosition) == false) {
			AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles.Add (tilePosition);
			
		} 
		
		else if (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles.Contains (tilePosition) == true && switchMade == false) {
			//If you walk on your own square, then-
			tempList = AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles;
			
			//Index of Tile 2
			tempIndex1 = tempList.IndexOf (tilePosition);
			tempIndex2 = tempList.IndexOf (lastPosition);
			//Take out the vector d
			tempVectorInList1 = tempList [tempIndex1];
			tempVectorInList2 = tempList [tempIndex2];
			
			tempList [tempIndex1] = tempVectorInList2;
			tempList [tempIndex2] = tempVectorInList1;
			
			switchMade = true;
			
			
			AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().markedTiles = tempList;
			
			//players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.RemoveAt(0);
			
			//players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles = new Queue<Vector3>(tempList);
			//queue = new Queue<T>(list);
			
			//players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Add (tilePosition);
		}
		
		if (tilesListAllAIPlayers.Contains (tilePosition) == false) {
			tilesListAllAIPlayers.Add (tilePosition);
			/*if (currentAIPlayerIndex == 0){
				playerTiles1.Add (tilePosition);
			}
			if (currentPlayerIndex == 1){
				playerTiles2.Add (tilePosition);
			}
		}
		
		AIplayers[currentAIPlayerIndex].GetComponent<AIPlayer>().moves--;
		
		return true;
		
	}*/

	public void attack()
	{
		players [currentPlayerIndex].GetComponent<UserPlayer> ().attacking = true;
	}
	public void AIattack(){
		AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().attacking = true;
	}
	public void attackDone(Vector3 tilePosition){
		/*for (int h = 0; h < 10; h++){
		 * if (AIplayers[h].GetComponent<AIPlayer>().markedTiles.Contains(tilePosition) == true){
		 * 	AIplayer.[h].GetComponent<AIPlayer>().health = AIplayer.[h].GetComponent<AIPlayer>().health-players[currentPlayerIndex].GetComponent<UserPlayer>().attackDamage;
		 * }
		 */
		players [currentPlayerIndex].GetComponent<UserPlayer> ().moves = 0;
		players [currentPlayerIndex].GetComponent<UserPlayer> ().attacking = false;
		players [currentPlayerIndex].GetComponent<UserPlayer> ().hasAttacked = true;
	}

	public void nextTurn(){
		turnChange = true;
		AITurn ();
	}

	/*void GenerateMap()
	{
		map = new List<List<Tile>>();
		for (int i = 0;i < mapSize; i++){
			List <Tile> row = new List<Tile>();
			for (int j = 0;j < mapSize; j++)
			{
				Tile tile = 
					((GameObject)Instantiate(
						TilePrefab,
						new Vector3(i - Mathf.Floor(mapSize/2),0,j - Mathf.Floor(mapSize/2)),
						Quaternion.Euler(new Vector3()))
					 ).GetComponent<Tile>();
				tile.gridPosition = new Vector2(i, j);
				row.Add(tile);
			}
			map.Add(row);
		}
	}*/

	/*void GeneratePlayers()
	{
		UserPlayer player;

		player = 
			((GameObject)Instantiate(
				UserPlayerPrefab,
				new Vector3(2,0,1),
				Quaternion.Euler(new Vector3()))
			 ).GetComponent<UserPlayer>();

		players.Add(player);

		player = 
			((GameObject)Instantiate(
				UserPlayerPrefab,
				new Vector3(1,0,1),
				Quaternion.Euler(new Vector3()))
			 ).GetComponent<UserPlayer>();
		
		players.Add(player);

		AIPlayer aiplayer = 
			((GameObject)Instantiate(
				AIPlayerPrefab,
				new Vector3(-1,0,7),
				Quaternion.Euler(new Vector3()))
			 ).GetComponent<AIPlayer>();
		
		players.Add(aiplayer);
	}*/
	public void OnGUI() {

		tempRect = new Rect (Screen.width-200, 200, 150, 250);

		GUI.skin = MetalGUISkin;
		
		//Graphics.DrawTexture (tempRect, marked);

		if (turnChange == false && matchStarted == true) {	
		GUILayout.BeginArea (tempRect);
			GUILayout.BeginVertical ("Selected Program", GUI.skin.GetStyle ("box"));

			GUILayout.Label (healthString);
			if (matchStarted == true) {
				if (players [currentPlayerIndex].GetComponent<UserPlayer> ().hasAttacked == false) {
					if (GUILayout.Button ("Attack")) {
						attack ();
					}
				}
			}
			if (GUILayout.Button ("Do Nothing")) {
				doNothing ();
			}
			if (players[currentPlayerIndex].GetComponent<UserPlayer>().hasAttacked == false){
				if (GUILayout.Button ("Undo")) {
					undo ();
				}
			}
			/*if (matchStarted == true && gamePaused == true) {
				if (GUILayout.Button ("Next Turn")) {
					turnChange = true;
					AITurn ();
					round++;
					currentPlayerIndex = 0;
					for (int y = 0; y < players.Count; y++) {
						players [y].GetComponent<UserPlayer> ().moves = 3;
						print ("players given moves works");
					}
					gamePaused = false;
				}

			}*/
		
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}
		if (matchStarted == false) {
			tempRect = new Rect (10, Screen.height - 500, 200, 400);
			GUILayout.BeginArea (tempRect);
			GUILayout.BeginVertical ("Match Setup", GUI.skin.GetStyle ("window"));
			GUILayout.Label (healthString);
			if (spawningPlayer == true){
				if (GUILayout.Button ("Spawn Bug.Alpha")) {
					UserPlayer player;
					player = ((GameObject)Instantiate(
						UserPlayerPrefab,
						startingVector,
						Quaternion.Euler(new Vector3()))
				        	).GetComponent<UserPlayer>();

					players.Add(player);
					players[players.IndexOf(player)].GetComponent<UserPlayer>().attackRange = 1.1f;
					players[players.IndexOf(player)].GetComponent<UserPlayer>().attackDamage = 2;
					players[players.IndexOf(player)].GetComponent<UserPlayer>().maxHealth = 3;
					players[players.IndexOf(player)].GetComponent<UserPlayer>().moves = 3;

					spawningPlayer = false;
				}
			}
			//CHANGE HERE FOR AMOUNT OF PLAYERS IN MATCH
			if (players.Count == 2){
				if (GUILayout.Button ("Start Match")){
					matchStarted = true;
					turnChange = true;
				}
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}
	}
}
