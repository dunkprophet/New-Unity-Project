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
	public Sprite selected;
	public Sprite notSelected;
	public string healthString;

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

	public bool AInextProgram;
	public int AIact = 0;

	public bool matchStarted;
	public bool gamePaused;
	public List<Player> players = new List<Player>();
	public List<Player> playersThatCanMove = new List<Player>();
	public List<AIPlayer> AIplayers = new List<AIPlayer> ();

	public List<Vector3> tiles = new List<Vector3> ();

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
	public Player tempPlayer;
	public float tempFloat;

	public int round;

	public int currentPlayerIndex = 0;
	public int currentAIPlayerIndex = 0;
	public int lastPlayerIndex;

	private int tempCount;
	private Vector3 tempVector;

	public Vector3 startingVector;

	void Awake ()
	{
		instance = this;

	}

	void Start ()
	{	
		AImoving = false;
		turnChange = false;
		matchStarted = false;
		gamePaused = false;
		spawningPlayer = false;
		switchMade = false;
		AInextProgram = false;
		//startingVector1 = new Vector3 (1, 0, 1);
		//startingVector2 = new Vector3 (2, 0, 1);
		
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;
		marked 	= Resources.Load("Textures/markedTexture") as Texture;
		notMarked = Resources.Load("Textures/Ground1") as Texture;
		attackTexture = Resources.Load("Textures/attackTexture") as Texture;
		selected = Resources.Load<Sprite>("Textures/Bug_selected");
		notSelected	= Resources.Load<Sprite>("Textures/Bug");
		AImarked = Resources.Load("Textures/AImarked") as Texture;
		//turnTexture = (Texture2D)Resources.Load(".png");
		//(Texture2D)Resources.Load("arrow.png");

		//ENEMEY SPAWNER
		AIPlayer aiplayer;
		aiplayer = 
			((GameObject)Instantiate(
			AIPlayerPrefab,
			new Vector3(-1,0,5),
			Quaternion.Euler(new Vector3()))
			).GetComponent<AIPlayer>();
		
		AIplayers.Add(aiplayer);

		aiplayer = 
			((GameObject)Instantiate(
				AIPlayerPrefab,
				new Vector3(1,0,5),
				Quaternion.Euler(new Vector3()))
			 ).GetComponent<AIPlayer>();
		
		AIplayers.Add(aiplayer);

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

		if (matchStarted == true && gamePaused == false){
		/*tilesList = players [0].GetComponent<UserPlayer> ().markedTiles.ToList();
		tilesList2 = players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.ToList ();*/
		//if (tiles.Contains){
		//
		//}

		currentPlayerPosition = players[currentPlayerIndex].transform.position;
		players[currentPlayerIndex].TurnUpdate();

		currentPlayerPosition = players[currentPlayerIndex].transform.position;
		//if (currentPlayerPosition = tiles.Find (currentPlayerPosition)) {
			//tiles.
		//}
		healthString = players[currentPlayerIndex].GetComponent<UserPlayer> ().health.ToString();
		currentPlayerAttackRange = players[currentPlayerIndex].GetComponent<UserPlayer>().attackRange;
		
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

		tempFloat = 1000;
		for (int g = 0; g < players.Count-1; g++) {
			print ("Search for target begun");
			if (tempFloat > Vector3.Distance (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().currentAIPlayerPosition, players [g].GetComponent<UserPlayer> ().transform.position)) {
				//tempFloat = distance between target and AI
				tempFloat = Vector3.Distance (AIplayers [currentAIPlayerIndex].GetComponent<AIPlayer> ().currentAIPlayerPosition, players [g].GetComponent<UserPlayer> ().transform.position);
				AIplayers[currentAIPlayerIndex].GetComponent<AIPlayer>().target = players[g].GetComponent<UserPlayer>().transform.position;
				print (AIplayers[currentAIPlayerIndex].GetComponent<AIPlayer>().target);
				print ("Target found!");
			}
		}
		//End of act 1 - target found
		AIplayers[currentAIPlayerIndex].GetComponent<AIPlayer>().targetFound = true;
		

			//Movement
				

				
			
			
			//Attack
			//while (AIhasAttacked == false)
			//AI characters do what they do.
			//IF ENEMY IS IN RANGE OF ATTACK, MOVE ONLY IF MOVEMENT ALLOWS FOR AI TO STAY IN RANGE
			//Movement TOWARDS ENEMY. 
			// If (players.[allIndex].markedTiles == in rangeforattack){
			//attack!
			//}
			
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
	public bool MoveCurrentAIPlayer(Vector3 tilePosition)
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
				}*/
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
			}*/
		}
		
		AIplayers[currentAIPlayerIndex].GetComponent<AIPlayer>().moves--;
		
		return true;
		
	}

	public void attack()
	{
		players [currentPlayerIndex].GetComponent<UserPlayer> ().attacking = true;
		//Attack code here
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

		if (turnChange == false) {	
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
			if (GUILayout.Button ("Undo")) {
				undo ();
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
