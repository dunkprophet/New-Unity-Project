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
	public Sprite selected;
	public Sprite notSelected;
	public string healthString;

	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;

	//public int moves = 3;
	public Vector3 currentPlayerPosition;
	public Vector3 lastPosition;
	public bool movingPlayer;

	public bool turnChange;

	public bool switchMade;

	public bool spawningPlayer;

	public bool matchStarted;
	public bool gamePaused;
	public List<Player> players = new List<Player>();
	public List<Player> playersThatCanMove = new List<Player>();

	//public List<Vector3> tilesVectors = new List<Vector3>();
	public List<Vector3> tiles = new List<Vector3>();

	public List<Vector3> tilesListBothPlayers;

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

	public int round;

	public int currentPlayerIndex = 0;
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
		turnChange = false;
		matchStarted = false;
		gamePaused = false;
		spawningPlayer = false;
		switchMade = false;
		//startingVector1 = new Vector3 (1, 0, 1);
		//startingVector2 = new Vector3 (2, 0, 1);
		
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;
		marked 	= Resources.Load("Textures/markedTexture") as Texture;
		notMarked = Resources.Load("Textures/Ground1") as Texture;
		selected = Resources.Load<Sprite>("Textures/Bug_selected");
		notSelected	= Resources.Load<Sprite>("Textures/Bug");
		//turnTexture = (Texture2D)Resources.Load(".png");
		//(Texture2D)Resources.Load("arrow.png");

		//UserPlayer player;
		/*
		player = ((GameObject)Instantiate(
			UserPlayerPrefab,
			startingVector1,
			Quaternion.Euler(new Vector3()))
		    ).GetComponent<UserPlayer>();
		
		players.Add(player);

		player = ((GameObject)Instantiate(
			UserPlayerPrefab,
			startingVector2,
			Quaternion.Euler(new Vector3()))
		    ).GetComponent<UserPlayer>();
		
		players.Add(player);

		AIPlayer aiplayer = 
			((GameObject)Instantiate(
			AIPlayerPrefab,
			new Vector3(-1,0,7),
			Quaternion.Euler(new Vector3()))
			).GetComponent<AIPlayer>();
		
		players.Add(aiplayer);*/


		
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

		//AI characters do what they do.

	}

	public void undo(){
		if (players [currentPlayerIndex].GetComponent<UserPlayer> ().hasAttacked == false) {
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

	public void attack()
	{
		//Attack code here
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

		GUILayout.BeginArea (tempRect);
		GUILayout.BeginVertical ("Selected Program", GUI.skin.GetStyle("box"));
		GUILayout.Label(healthString);
		if (matchStarted == true) {
			if (players [currentPlayerIndex].GetComponent<UserPlayer> ().hasAttacked == false) {
				if (GUILayout.Button ("Attack")) {
					attack ();
				}
			}
		}
		if (GUILayout.Button ("Do Nothing")) {
			doNothing();
		}
		if (GUILayout.Button ("Undo")) {
			undo();
		}
		if (matchStarted == true && gamePaused == true) {
			if (GUILayout.Button ("Next Turn")) {
				turnChange = true;
				AITurn ();
				round++;
				currentPlayerIndex = 0;
				for (int y = 0; y < players.Count; y++) {
					players[y].GetComponent<UserPlayer> ().moves = 3;
					print ("players given moves works");
				}
				gamePaused = false;
			}

		}
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
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

					spawningPlayer = false;
				}
			}
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
