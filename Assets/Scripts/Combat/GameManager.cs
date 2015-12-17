using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	
	private GUISkin MetalGUISkin;
	public Rect tempRect;

	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;

	//public int moves = 3;
	public Vector3 currentPlayerPosition;
	public bool movingPlayer;

	public List<Player> players = new List<Player>();
	//public List<Vector3> tilesVectors = new List<Vector3>();
	public List<Vector3> tiles = new List<Vector3>();

	public List<Vector3> tilesList;
	
	public List<Vector3> tilesList2;

	public int tempTile;
	public int tempIndex;

	public int currentPlayerIndex = 0;

	void Awake ()
	{
		instance = this;

	}

	void Start ()
	{	
		
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;

		UserPlayer player;
		
		player = ((GameObject)Instantiate(
			UserPlayerPrefab,
			new Vector3(1,0,1),
			Quaternion.Euler(new Vector3()))
		    ).GetComponent<UserPlayer>();
		
		players.Add(player);

		player = ((GameObject)Instantiate(
			UserPlayerPrefab,
			new Vector3(2,0,1),
			Quaternion.Euler(new Vector3()))
		    ).GetComponent<UserPlayer>();
		
		players.Add(player);

		/*AIPlayer aiplayer = 
			((GameObject)Instantiate(
			AIPlayerPrefab,
			new Vector3(-1,0,7),
			Quaternion.Euler(new Vector3()))
			).GetComponent<AIPlayer>();
		
		players.Add(aiplayer);*/

		
		movingPlayer = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Count > 4) {
			players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Dequeue();
		}

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

	}

	void OnMouseDown()
	{
		if (movingPlayer != true)
		{
			//MoveCurrentPlayer();
		}
	}

	public void NextTurn()
	{
		if (currentPlayerIndex + 1 < players.Count) {
			

			currentPlayerIndex++;
			players[currentPlayerIndex].GetComponent<UserPlayer>().moves = 3;

			//if (players[currentPlayerIndex].notAI == false) {

			//}


		} else
		{
			currentPlayerIndex = 0;
			players[currentPlayerIndex].GetComponent<UserPlayer>().moves = 3;
		}
	}

	//public int healthUpdate(){
	//	return markedTiles.Count;
	//}

	public void MoveCurrentPlayer(Vector3 tilePosition)
	{
		players[currentPlayerIndex].GetComponent<UserPlayer>().moves--;
		if (movingPlayer == false) {
			players [currentPlayerIndex].transform.position = tilePosition;
		}
		if (players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Contains (tilePosition) == false) {
			players [currentPlayerIndex].GetComponent<UserPlayer> ().markedTiles.Enqueue (tilePosition);
		}
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
		GUI.skin = MetalGUISkin;
		
		Rect tempRect = new Rect(10, 10, 200, 200);
		
		GUILayout.BeginArea (tempRect);
		GUILayout.BeginVertical ("Health", GUI.skin.GetStyle("box"));
		GUILayout.Label(players[currentPlayerIndex].GetComponents<UserPlayer> ().healthString);
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
		GUILayout.EndArea ();
	}
}
