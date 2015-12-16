using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;

	public int moves = 3;
	public Vector3 currentPlayerPosition; 
	public int mapSize = 11;
	public bool movingPlayer;

	//List <List<Tile>> map = new List<List<Tile>>();
	public List<Player> players = new List<Player>();
	int currentPlayerIndex = 0;

	void Awake ()
	{
		instance = this;

	}
	// Use this for initialization
	void Start ()
	{	
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
		//GenerateMap();
		//GeneratePlayers();
		
		movingPlayer = false;
	}

	// Update is called once per frame
	void Update ()
	{
		currentPlayerPosition = players[currentPlayerIndex].transform.position;
		players[currentPlayerIndex].TurnUpdate();
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
		//movingPlayer = false;
		if (currentPlayerIndex +1 < players.Count) {
			
			movingPlayer = false;
			moves = 3;
			currentPlayerIndex++;

		} else
		{
			currentPlayerIndex = 0;
			movingPlayer = false;
			moves = 3;
		}
	}

	public void MoveCurrentPlayer(Vector3 tilePosition)
	{

		moves = moves - 1;
		//if (movingPlayer = false){
		players[currentPlayerIndex].transform.position = tilePosition;
		//}
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
}
