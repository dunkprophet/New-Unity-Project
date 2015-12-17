using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UserPlayer : Player {

	public int health;
	public int moves;
	//public int moves = 3;
	//int movesPerMove = 1;
	private GUISkin MetalGUISkin;

	public Texture turnTexture;

	public string healthString;

	public Rect tempRect;

	public int tempCount;
	public Vector3 tempVector;

	public bool allowedToMove;

	public float tempPositionX;
	public float tempPositionY;

	public Texture marked;

	public Queue<Vector3> markedTiles;

	//public Vector3 moveDestination;


	// Use this for initialization
	void Start ()
	{

		markedTiles = new Queue<Vector3>();

		moves = 3;
		health = 1;
		allowedToMove = false;
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;
		turnTexture = (Texture2D)Resources.Load("arrow.png");
		marked = (Texture2D)Resources.Load("arrow.png");
		tempCount = 0;

		markedTiles.Enqueue (transform.position);

	}

	// Update is called once per frame
	void Update ()
	{



		if (tempCount > 10) {
			tempCount = 0;
		}   tempCount++;
		tempVector = markedTiles.ToList()[tempCount];
		print (tempVector);


		//if (GameManager.instance.movingPlayer == true){
			//transform.position = Vector3.MoveTowards(transform.position, Tile.instance.transform.position, 0.5f);
		//}

		//tempPositionX = transform.localPosition.x;
		//tempPositionY = transform.localPosition.z;
		healthString = health.ToString ();
	}

	public static bool notAI (){
		return true;
	}

	public override void TurnUpdate ()
	{
		health = markedTiles.Count;
		//transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.red;
		//Graphics.DrawTexture(new Rect(200, 200, 200, 200), turnTexture);
			if (moves <= 0) {

				//transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.white;
				
				GameManager.instance.NextTurn();
			}


		base.TurnUpdate ();
	}

	public void OnGUI() {
		GUI.skin = MetalGUISkin;

		Rect tempRect = new Rect(500, 500, 200, 200);

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

