using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	//GUI VARIABLES
	private GUISkin MetalGUISkin;
	public Rect tempRect;

	//TEXTURES && SPRITES
	public Texture turnTexture;
	public Texture marked;
	public Texture notMarked;
	public Texture attackTexture;	
	public Texture AImarked;
	public Texture movableTile;
	public Sprite selected;
	public Sprite notSelected;

	//STRINGS
	public string healthString;
	public string winTextDescription;
	public string loseTextDescription;

	//GAMEOBJECTS - LOADED
	public GameObject movableTilePrefab;
	public GameObject attackableTilePrefab;
	public GameObject markedTilePrefab;
	public GameObject markedAITilePrefab;

	//GAMEOBJECTS SELECTED
	public GameObject TilePrefab;
	public GameObject Bug;
	public GameObject GOD;
	public GameObject Golem;

	//Positions of programs
	public Vector3 currentPlayerPosition;
	public Vector3 lastPosition;
	public Vector3 startingVector;

	//Current player info
	public float currentPlayerAttackRange;

	//Bools
	public bool movingPlayer;
	public bool turnChange;
	public bool switchMade;
	public bool spawningPlayer;
	public bool AImoving;
	public bool findGoodTile;
	public bool tileBool;
	public bool AInextProgram;
	public bool matchStarted;
	public bool gamePaused;
	public bool ready;
	public bool canAttack;
	public bool winText = false;
	public bool loseText = false;

	//Useless shit
	public int AIact = 0;
	public List<Vector3> playerTiles1 = new List<Vector3>();
	public List<Vector3> playerTiles2 = new List<Vector3>();
	public Vector3 searcherPosition = new Vector3 (0, 0, 0);

	//List over programs, tiles and objects
	public List<Player> players = new List<Player>();
	public List<Player> playersThatCanMove = new List<Player>();
	public List<AIPlayer> AIplayers = new List<AIPlayer> ();
	public List<Vector3> tilesPos = new List<Vector3> ();
	public List<Tile> tilesObj = new List<Tile> ();

	//List of all colored tiles
	public List<Vector3> tilesListBothPlayers;
	public List<Vector3> tilesListAllAIPlayers;

	//Temp values - Used in various ways	
	public int tempTile;
	public int tempIndex1;
	public int tempIndex2;
	public int tempIndex;
	public bool tempBool;
	public List<Vector3> tempList = new List<Vector3> ();
	public List<Vector3> tempList2 = new List<Vector3> ();
	public Vector3 tempVectorInList1;
	public Vector3 tempVectorInList2;
	public Player tempPlayer;
	public float tempFloat;
	private int tempCount;
	private Vector3 tempVector;
	public Ray ray;
	public RaycastHit hit;

	//Current program attack values
	public Vector3 attackPosition;
	public int attackDamage;
	public bool attackHit;
	public int distanceToTarget;

	//Indexes
	public int currentPlayerIndex = 0;
	public int currentAIPlayerIndex = 0;
	public int lastPlayerIndex;

	//Match info
	public int round;
	public int deadAIplayers = 0;
	public int deadPlayers = 0;

	//Pathfinding stuff O_o
	public TileType[] tileTypes;
	public int[,] tiles;
	Node[,] graph;
	public int mapSizeX = 50;
	public int mapSizeY = 50;

	void Awake ()
	{
		instance = this;

	}


	// -------------------------------------------------------------------------------------------PATHFINDING STUFF
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
	// ..........-----------------------------------------------------------END OF PATHFINDING



	void Start (){
		tileBool = false;

		//Generating the map
		GenerateMapData();
		GeneratePathfindingGraph();

		//Specifying values
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
		ready = true;
		canAttack = false;

		//Loading resources

		//Loading programs
		Golem = Resources.Load ("Prefabs/Golem") as GameObject;
		Bug = Resources.Load ("Prefabs/Bug") as GameObject;
		GOD = Resources.Load ("Prefabs/GOD") as GameObject;

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
	
		//Setting the start conditions
		currentPlayerIndex = 0;
		movingPlayer = false;
		round = 0;
	}

	public IEnumerator attackPause(float delay) {
		ready = false;
		yield return new WaitForSeconds(delay);
		
		//What happens after delay
		players [currentPlayerIndex].GetComponent<UserPlayer> ().moves = 0;
		players [currentPlayerIndex].GetComponent<UserPlayer> ().attacking = false;
		players [currentPlayerIndex].GetComponent<UserPlayer> ().hasAttacked = true;
		
		ready = true;
	}

	public void spawnPlayer (Vector3 spawnerPosition){
		startingVector = spawnerPosition;
		spawningPlayer = true;
	}
	public void spawnEnemy (Vector3 spawnerPosition, float attackRange, int attackDamage, int maxHealth, int maxMoves, int moves, string enemyName){
		//ENEMEY SPAWNER
		AIPlayer aiplayer;
		aiplayer = 
			((GameObject)Instantiate (
				//AIPlayerPrefab,
				Resources.Load ("Prefabs/"+enemyName) as GameObject,
				spawnerPosition,
				Quaternion.Euler (new Vector3 ()))
			 ).GetComponent<AIPlayer> ();
		print ("AIplayer spawned");
		AIplayers.Add (aiplayer);
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().attackRange = attackRange;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().attackDamage = attackDamage;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().maxHealth = maxHealth;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().maxMoves = maxMoves;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().moves = moves;
		AIplayers[AIplayers.IndexOf(aiplayer)].GetComponent<AIPlayer>().indexAI = AIplayers.IndexOf(aiplayer);

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

				for (int y = players.Count-1; y >= 0; y--) {
					if (players [y].GetComponent<UserPlayer> ().dead == false){
						currentPlayerIndex = y;
					}
				}

				for (int y = AIplayers.Count-1; y >= 0; y--) {
					if (AIplayers [y].GetComponent<AIPlayer> ().dead == false){
						currentAIPlayerIndex = y;
					}
				}

				for (int y = 0; y < players.Count; y++) {
					if (players [y].GetComponent<UserPlayer> ().dead == false){
						players [y].GetComponent<UserPlayer> ().moves = players [y].GetComponent<UserPlayer>().maxMoves;
						print ("players given moves");
					}
				}
				for (int y = 0; y < AIplayers.Count; y++) {
					if (AIplayers [y].GetComponent<AIPlayer> ().dead == false){
						AIplayers [y].GetComponent<AIPlayer> ().moves = AIplayers [y].GetComponent<AIPlayer> ().maxMoves;
						print ("AI given moves");
					}
				}
				turnChange = false;
				gamePaused = false;
			} else {

				print (currentAIPlayerIndex);
				currentAIPlayerIndex++;
				AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().canMove = false;
				print ("currentAIplayerindex increase to "+currentAIPlayerIndex);
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
				print ("Playerthatcanmove works");
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
		for (int g = 0; g < GameManager.instance.tilesListBothPlayers.Count; g++) {
			print ("Search for target begun");
			if (tempFloat > Vector3.Distance (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().transform.position, GameManager.instance.tilesListBothPlayers[g])) {
				//tempFloat = distance between target and AI
				tempFloat = Vector3.Distance (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().transform.position, GameManager.instance.tilesListBothPlayers[g]);
				AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().target = GameManager.instance.tilesListBothPlayers[g];

			}
		}
		if (tempFloat < 10000) {
			print (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().target);
			print ("Target found! Or not?");
			//End of act 1 - target found
			AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().targetFound = true;
		} else {
			for (int eq = 0; eq < players.Count; eq++){
				if (tempFloat > Vector3.Distance (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().transform.position, players[eq].GetComponent<UserPlayer>().transform.position)){
					tempFloat = Vector3.Distance (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().transform.position, players[eq].GetComponent<UserPlayer>().transform.position);
					AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().target = players[eq].GetComponent<UserPlayer>().transform.position;
				}
			}
		}
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

		if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Count >= players [currentPlayerIndex].GetComponent<UserPlayer> ().maxHealth) {

			if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == false) {

				tilesListBothPlayers.Remove(players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.First());

				/*for (int o = 0; o < players.Count; o++){
					if (currentPlayerIndex == o) {
						playerTiles1.Remove(players[o].GetComponent<UserPlayer>().markedTiles.First ());
					}
				}*/
				players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.RemoveAt(0);
				switchMade = false;
			}
		} 

		//TILE THAT GETS STEPPED ON IS PUSHED FORWARD ONE STEP, EXCHANGED WITH IT'S INDEX+1
		//OLD SPOT GET TAKEN BY LATEST TILE

		if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == true /*&& switchMade == true*/) {

			tempList = players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles;
			
			tempIndex = tempList.IndexOf (tilePosition);

			tempList.RemoveAt(tempIndex);
			tempList.Add (tilePosition);

			players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles = tempList;


		}
		if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == false) {
			players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Add (tilePosition);
			
		} 

		players[currentPlayerIndex].GetComponent<UserPlayer>().moves--;

		return true;

	}


	public void attack()
	{
		players [currentPlayerIndex].GetComponent<UserPlayer> ().attacking = true;
	}
	public void AIattack(){
		AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().attacking = true;
	}
	public void attackDone(Vector3 tilePosition){

		//Attack delay

		if (ready == true) {
			StartCoroutine (attackPause (0.5f));
		}
	}

	public void nextTurn(){
		//currentAIPlayerIndex = 0;
		turnChange = true;
		AITurn ();
	}


	public void OnGUI() {

		tempRect = new Rect (Screen.width-200, 200, 150, 250);

		GUI.skin = MetalGUISkin;
		
		//Graphics.DrawTexture (tempRect, marked);

		if (deadAIplayers == AIplayers.Count) {
			if (winText == false){
				winText = true;
				OverworldManager.instance.text = "Infiltration Successful";
				OverworldManager.instance.i = 0;
			}
			GUILayout.BeginArea (new Rect (Screen.width*0.2f, Screen.height*0.1f, Screen.width*0.6f, Screen.height*0.8f));
			GUILayout.BeginVertical ("Screen.Victory();", GUI.skin.GetStyle ("netscape"));
			GUI.skin.label.fontSize = Mathf.RoundToInt (36 * Screen.width / (OverworldManager.instance.defaultWidth* 1.0f));
			GUILayout.Label(OverworldManager.instance.textPrint+"\n");
			GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (OverworldManager.instance.defaultWidth* 1.0f));
			GUILayout.Label(winTextDescription+"\n");
			if (GUILayout.Button("[Exit Node]")){
				matchStarted = false;
				OverworldManager.instance.matchStarted = false;
				Application.LoadLevel(1);
			}
			GUILayout.EndVertical();
			GUILayout.EndArea ();
		}
		if (deadPlayers == players.Count && matchStarted == true) {
			if (loseText == false){
				loseText = true;
				OverworldManager.instance.text = "C̶̩o̕n͓̖̳̬͟n̷̜͓̮̳e̥̻̤͇͇ͅc̸̼̪̘̣̗͎ͅţ̝̗i̵͔̩͖͖̹̠o̙͓̞̥̤n̛͕͍͚͇̝ ̗̰̦̥̺͙̥͠L͔̪̥̬̬̱o̡s̶͔̲t͈̼̻";
				OverworldManager.instance.i = 0;
			}
			GUILayout.BeginArea (new Rect (Screen.width*0.2f, Screen.height*0.1f, Screen.width*0.6f, Screen.height*0.8f));
			GUILayout.BeginVertical ("Screen.Loss();", GUI.skin.GetStyle ("netscape"));
			GUI.skin.label.fontSize = Mathf.RoundToInt (36 * Screen.width / (OverworldManager.instance.defaultWidth* 1.0f));
			GUILayout.Label(OverworldManager.instance.textPrint+"\n");
			GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (OverworldManager.instance.defaultWidth* 1.0f));
			GUILayout.Label(loseTextDescription+"\n");
			if (GUILayout.Button("[Exit Node]")){
				matchStarted = false;
				OverworldManager.instance.matchStarted = false;
				Application.LoadLevel(1);
			}
			GUILayout.EndVertical();
			GUILayout.EndArea ();
		}


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

			/*if (GUILayout.Button ("Isometric")) {
				Camera.main.transform.position = new Vector3 (0,0,-9);
				Camera.main.transform.rotation = Quaternion.Euler(30,45,0);
				

			}
			if (GUILayout.Button ("Old-school")) {

				Camera.main.transform.position = new Vector3 (12,5,-9);
				Camera.main.transform.rotation = Quaternion.Euler(90,0,0);
			}*/

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
		if (matchStarted == false && gamePaused == false) {
			tempRect = new Rect (0, 0, Screen.width/5, Screen.height);
			GUILayout.BeginArea (tempRect);
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("netscape"));
			GUILayout.Label (healthString);
			GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (OverworldManager.instance.defaultWidth* 1.0f));
			if (spawningPlayer == true){
				for (int a = 0; a < OverworldManager.instance.programList.Count; a++){
					if (GUILayout.Button("Spawn "+OverworldManager.instance.programList[a])){
						UserPlayer player;
						player = ((GameObject)Instantiate(
							Resources.Load ("Prefabs/"+OverworldManager.instance.programList[a]) as GameObject,
							startingVector,
							Quaternion.Euler(new Vector3()))
						          ).GetComponent<UserPlayer>();
						
						players.Add(player);

						if (OverworldManager.instance.programList[a] == "Bug"){
							players[players.IndexOf(player)].GetComponent<UserPlayer>().attackRange = 1.1f;
							players[players.IndexOf(player)].GetComponent<UserPlayer>().attackDamage = 2;
							players[players.IndexOf(player)].GetComponent<UserPlayer>().maxHealth = 1;
							players[players.IndexOf(player)].GetComponent<UserPlayer>().maxMoves = 4;
							players[players.IndexOf(player)].GetComponent<UserPlayer>().moves = 4;
						}
						if (OverworldManager.instance.programList[a] == "GOD"){
							players[players.IndexOf(player)].GetComponent<UserPlayer>().attackRange = 2.2f;
							players[players.IndexOf(player)].GetComponent<UserPlayer>().attackDamage = 3;
							players[players.IndexOf(player)].GetComponent<UserPlayer>().maxHealth = 8;
							players[players.IndexOf(player)].GetComponent<UserPlayer>().maxMoves = 8;
							players[players.IndexOf(player)].GetComponent<UserPlayer>().moves = 8;
						}
						
						spawningPlayer = false;
					}
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
	}
}
