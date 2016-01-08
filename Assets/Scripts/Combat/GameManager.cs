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
	public GameObject attackableTilePrefab;
	public GameObject markedTilePrefab;
	public GameObject markedAITilePrefab;

	public Vector2 scrollPosition;

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
		attackableTilePrefab = Resources.Load ("Prefabs/attackableTilePrefab") as GameObject;
		markedTilePrefab = Resources.Load ("Prefabs/markedTilePrefab") as GameObject;
		markedAITilePrefab = Resources.Load ("Prefabs/markedAITilePrefab") as GameObject;
		//turnTexture = (Texture2D)Resources.Load(".png");
		//(Texture2D)Resources.Load("arrow.png");
	
		//ENEMEY SPAWNER
		AIPlayer aiplayer;
		aiplayer = 
		((GameObject)Instantiate (
			AIPlayerPrefab,
			new Vector3 (8, 0, 2),
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
			new Vector3 (2, 0, 9),
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
			if (GUILayout.Button ("No Action")) {
				doNothing ();
			}
			if (players[currentPlayerIndex].GetComponent<UserPlayer>().hasAttacked == false){
				if (GUILayout.Button ("Undo[WIP]")) {
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
			if (players.Count >= 1){
				if (GUILayout.Button ("Start Match")){
					matchStarted = true;
					turnChange = true;
				}
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}
		if (round >= 5) {
			tempRect = new Rect (100, 100, 600, 600);
			GUILayout.BeginArea (tempRect);
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("window"));
			GUILayout.Label ("\n\n END OF GAME\n\n Thank you for playing ICEBREAKER\n\n Hopefully the combat system will be completed soon!");
			
			if (GUILayout.Button ("Quit")) {
				Application.Quit();
			}
			if (GUILayout.Button ("Restart")) {
				Application.LoadLevel (0);
			}
			if (GUILayout.Button ("Read backstory")) {
				round = -100;
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}
		if (round < -1 ) {
			matchStarted = false;
			tempRect = new Rect (100, 100, 1200, 600);
			GUILayout.BeginArea (tempRect);
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("window"));
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(1175), GUILayout.Height(490));
			GUILayout.Label("Memoirs" +
			                "\n\n Every hot afternoon we spent there, on that small porch, eating Frostpipes and drinking cool sugary Fruit Adds with the blazing heat hanging over us. I got one hell of a tan that summer, that’s for sure. All seven of us did. Days would often begin with Dan calling me on the line, asking if I wanted to head out. He always got the same answer. But it took us a while to wander into town, so his dad would give us a lift. Not too far, just away from the country and into the more rural areas of the island. If there’s one thing I remember about that place, it’d have to be the rust. Whoever founded that small port town probably did not understand the fact that metal houses white like eggshells would end up looking spoiled and rotten within only a decade or so. But I liked it. There was something melancholy, yet uplifting about the brown corruption creeping its way up the cold plate walls. Or perhaps it’s just my sense of nostalgia." +
			                "\n\n Dan would then go on to talk about his evening, the food he ate, how his parents had bickered and fought over some stupid detail, and what show he’d managed to catch on the stream. I was always a bit jealous of him. We didn’t have a decent connection at the time. It wasn’t even enough to watch the First Channel News, which was the only one still with a low resolution and framerate mode. Dan would then ask me about my night. What I’d eaten, what my parent argued about, if I’d seen that hilarious episode of the Evening Show on FBS. I just made something up. I wanted Dan to think I was the same as him. Guess I didn’t really put much more thought into it than that." +
			                "\n\n Once we got close to the bay area, Dan and I would get on a payphone each and call the others. He took Jerry, Taiki and Tomako. I got on and phoned Magnus. A brutish woman would answer, demanding to know my name. I’d give it to her, and in turn she’d go and give the receiver to her son. Magnus was nice and quiet, never much a of risk taker. Then again, neither was I, so we got along well enough. After telling him to meet us down by the water, he’d try to make up some excuse as to why he shouldn’t go. It was always easy to convince him otherwise. Next I’d call Aya. She’d hear my voice and immediately get excited, quite the opposite of our poor friend Magnus. But she never did speak very long to me over the phone. If I remember correctly, it was due to some inherent hate she carried towards voice-only conversations. Aya was definitely a face-to-face kind of girl back then. That I know for sure." +
			                "\n\n Dan finished his calls around the same time as I did, and we headed down the thick cluster of metal and stone, down to the more open area by the docks. Most of the fishing boats were out at summertime. I can still smell the seawater when it came rolling in on the wind, while calm waves struck at the pier. It didn’t take long for them all to join us. Jerry was the tall one in our little group. He always carried around on a basketball, in the hopes that anyone wanted to play. We sometimes did, but not nearly as often as he’d like. Taiki and Tomako were sibling, twins in fact, but still quite dissimilar. Taiki was heavy, or fat, as we used to call it, and kept a smile on him at all times. Happiest kid I ever met. His sister, on the other hand, was cute as all hell. Pale skin and tall, she was, always being praised for being the most beautiful girl on the entire island, by classmates and adults alike. Too bad she was a total bitch. But we got along okay. After all, some people are just raised the way they are, and it’d just be mean to blame it on the children, when the parents are the ones at fault." +
			                "\n\n Aya would be the first to suggest something for us to do. Sometimes it was exploring the mountains, or going to the beach by Northside, or playing some games on the field outside the Post Office, or perhaps climbing over the school fences and enjoying the empty playground. No matter what we decided on, we usually stayed at it until noon. At that point we’d be exhausted, and hungry. So we’d go get something to eat at Mala’s Store, before spending the rest of the afternoon just hanging about on that small porch; drinking Fruit Adds and eating those ice cold Frostpipes. Remember those? With the long, tube-like cola, or strawberry flavored core, and icy exterior? No? Really? Huh. Your childhood must have been awful." +
			                "\n\n Mala would tell us that we should get ourselves some jobs, and not just sit around all day doing nothing. She told us it was a horrid way to spend a summer. We did not believe her. Or at least, most of us didn’t. If I remember correctly, it was Jerry who got enticed by her sayings, and got his parents to set him up on a fishing trip at the end of the break. He thought he’d get to fish a bunch, you know, with a rod. Poor sod ended up gutting fish for two weeks straight. At least he was good at it. In fact, he was so good at it, that they invited him to join them the next summer. And the summer after that. And the one after that. For all I know, poor Jerry is still on that boat, cruising around in the Pacific. I just hope they finally let him use the net." +
			                "\n\n I was twelve back then, or thirteen perhaps, it’s hard to remember. The long years I spent on that cozy island have gotten so very blurry. However, always that picture in my head turn back to that sunny porch, and the seven of us sitting on it, complaining about all the homework we needed to finish before the start of school. We were thirteen. Yeah, I remember now. Aya and Dan had turned fourteen in the spring, and we had just started our last year of middle school. God, I had such shitty grades back then. A thirty-two in Math, forty-three in English, and seven in Japanese. Seven. I’m not sure which is worst, the fact that I remember my exact grades from when I was thirteen, or that I only knew a few dozen words in the language that half of the island’s population spoke. I can still recall Aya laughing at me when I failed my midterms that year. I tossed away the red marked paper and told her to teach me instead of bullying me. She did. After all, the girl was a genius, having almost one hundred points in every grade, so showing a few tricks to the slow kid in class wouldn’t be too much to ask." +
			                "\n\n We met a few times at her place. Her father was gone on business at the time, I think he worked on some sort of research base on the mainland, and her mother owned one of the shops by the docks, so she was used to being alone during the daytime. But hopefully I wasn’t being a hassle by not understanding even the simplest of kanji, and stealing her precious afternoons. Though soon after we’d started meeting, Tamako and Taiki figured us out and told everyone. After that, there were seven members of our little study group. Jerry was horrible at math, so Magnus mostly helped him with that. Tamako and Taiki both failed their English, so Dan went through some common phrases and words with them. I miss Dan. He moved away with his mom a few weeks before graduation. He was a nice kid, and I hope he did well in that special school overseas his mom put him in. Oh, right. Not a ‘special’ school; a school for talented kids. Like Dan. God, Aya was so jealous back then. Guess I should have seen it coming." +
			                "\n\n But our study sessions continued, with or without Dan. I didn’t want to disturb Aya too much during, since she needed her time as much as any one of us, so I tried cramming by myself for a while. But Aya just ended up convincing me to come over anyways. She always helped me out. Back when the forest on Southside was still standing she actually saved me once, when I fell down a cliff. I had to walk with crutches for weeks, but it would have been much worse had she not dragged me back into town that night. I still can’t remember why I went out there at midnight. Huh. It’s odd how certain memories remain intact, and others simply fade away. That’s just the way time is, I suppose." +
			                "\n\n Unfortunately I have no recollection of what my grades were at the end of that year, but what I do know is that I passed. Barely. Aya cheered when she heard and gave me a big hug, though I’m still not sure if it was because of my success, or the fact that both of us were finally ready for high school. Taiki and Tamako were less enthusiastic. Both were forced by their parent to take over their family business on the island, so higher education was off their radars completely. Jerry told us when we went out to a diner that night that he was planning on making fishing his full time job. We all supported him. At least if you consider name-calling him Fish-face and telling him to marry a mermaid, ‘support’. Now that I write it down, it might sound like we were mean to poor Jerry, but believe me, he dished out twice the abuse to every single one of us on a daily basis. All in good fun, of course." +
			                "\n\n Magnus got in to EDB in F. California, one of the most prestigious private schools on the west coast. Or so he told us. His father had some contacts there, being a Suit and all, so that even with Magnus’ average grades, the path of politics was laid before him like a train track. He wasn’t too stoked about it thought. The quiet boy wasn’t one for hectic matters, let alone the corrupt business that is the American government. But ‘a man’s gotta respect his father’s wishes.’ That’s what he told me. It is the way of things, I suppose, when large quantities of money are involved." +
			                "\n\n I can still remember the sound of Aya’s voice when she told me that she was recommended for a spot on TT. In Tokyo. I was happy for her. It was the perfect place for my genius friend, in a giant tech facility of Nippon fame, where she’d be able to learn and develop all kinds of robots and computers. Whatever she wanted to do, essentially. Wherever. She’d get any job in the market with that amazing brain of hers. The only problem I could think of, that last year of school, on the lonely island in the middle of the Pacific Ocean, was just what in the world I would do with my life. Everyone had dreams and hopes, or at least goals in their lives, something to strive towards, a reason to live and prosper." +
			                "\n\n My name is Calvin, and back then I could not for the life of me choose something for myself. Perhaps if I wanted, I would have been able to enter one of the high schools on the mainland. Probably not a prestigious place like TT, but a smaller work-specialized school would definitely have accepted me. Yet it felt wrong, like all I had done to that point, all that time I’d spent with my seven friends on that porch in the sun had all gone to waste. Like my entire life had been a prequel to some unremarkable movie. But I couldn’t tell her. Why should I have? She was happy, and I was happy for her. So I simply said that I got into a smaller school in Nagoya, and that I’d come visit her whenever I had the time. I was a damn good liar. I even smiled when she floated away across the ocean, too far away to even notice me. But once she disappeared on the horizon, the smile disappeared along with her. Aya didn’t even spend that last summer with us. She had some prep-course to take, and so I, Taiki and Tamako waved her goodbye only a few days after graduation. I was bitter, the most bitter I’ve ever felt in my entire life. Taiki on the other hand, was happy as per usual. He had accepted his responsibility and already learned the ropes at his parent’s business. They sold paper towels, or something, if I remember correctly. Or perhaps it was some sort of linen cloth? Either way, Tamako did not like it. I always enjoyed Wombats. Whenever I saw them during that last summer, all she did was complain. That girl was probably the only person on the island bitterer than me." +
			                "\n\n A few days after Aya left, I was sitting down by the docks around midnight, just watching the moon’s reflection on the calm waters, when I saw a shadow sneaking down the pier. It was Tamako with a big backpack filled to the brim with food, personal belongings, makeup and a large comp-soundset. She jumped when I called out to her, and begged me not to stop her. I didn’t, in fact I didn’t even think about doing it, but I did ask her what I would stop her from doing. She was running away, obviously. Took me a while to realize though, an embarrassingly long time. She was taking one of the family boats and sailing off to Japan. I asked her what the hell she would do there. She flashed me a pretty smile and said, ‘I’m going to sing!’ Well. The girl was brave, I’ll give her that. Going off onto open water all by herself, to chase a foolish dream. I admired her then, and I still do, even if she was a complete asshole most of the time. I watched her sail off with a smile on my face, just like when Aya went off, except this time it was for real. For some reason, seeing that foolish girl roll across shimmering sea made me understand it all. At that moment something came to me. I stopped, worked out everything going on around me, and memorized the thought. This was it. I knew what I had to do." +
			                "\n\n My parents weren’t around much. But I had a house to live in, food to cook, water to drink, and an old computer to play around with, which was well and enough for me. I can’t really remember when I started living like that, almost all by myself on the outskirts of town. It was all that I knew, yet my seven dear friends definitely had it differently. They often heard complaints over parents, sibling, and other such things. I never really understood. So I spend my childhood in front of a computer, playing games, reading, learning, and browsing shitty imageboards. But since my connection was absolute shit-tier, whenever I wanted to stream a video or download something bigger than a few megabytes, I’d just go to a neighbor’s house. Rachel, I think she was called, the old lady living a few thousand meters from my little house. She’d let me borrow her connection in exchange for garden work and favors. I remember how she even let me use her newly bought Vail computer one summer. Nice lady, I wonder if she’s still alive. I never did say goodbye to her when I left that night, on the twenty-first of August, one month after I’d watched Tomako do the same." +
			                "\n\n Every Sunday a large cargo ship would stop by our town’s little bay. Most dockworkers would spend these days loading whatever sea critters they had gotten in the previous week, and let it get shipped off to the mainland to the facilities where it’s become whatever food it needed to become. Most of the fishermen were employed by the corporation handling the boat, and thus it was very important for them to reach a certain quota with the amount of fish. That’s why I had to be careful when tossing out some of it from a large container; I had to make sure the weight I threw out was the exact same as my own weight because they checked carefully before loading it, and I did not want them to detect an anomaly of any kind. //Shivering and shaking, covered in fish guts and slime of a fouler kind. I had heard of it from Jerry, during one of his many rants about the fishing industry. All the fresh fish would be crystal-frozen with an industrial pump once onboard, so I figured that by jumping into one of the large metal boxes via the small door on the top, I’d be able to then sneak out into the cargo hold, and hide there for the 28 hour boat ride. I brought food, money, blankets, and clothes, all in a backpack that I kept inside a thick plastic bag. There was almost a moment that I thought about bringing along the computer, but it would have been too much carrying around that heavy brick, even if the 28 hours in the dark would get really boring. Interestingly enough, it didn’t. \n\n\n\n TO BE CONTINUED...\n");
			GUILayout.EndScrollView();

			
			if (GUILayout.Button ("Quit")) {
				Application.Quit();
			}
			if (GUILayout.Button ("Restart")) {
				Application.LoadLevel (0);
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}
	}
}
