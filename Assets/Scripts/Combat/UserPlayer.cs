using UnityEngine;
using System.Collections;

public class UserPlayer : Player {

	//public int moves = 3;
	//int movesPerMove = 1;

	//public Vector3 moveDestination;


	// Use this for initialization
	void Start ()
	{
	
	}

	// Update is called once per frame
	void Update ()
	{
		//if (GameManager.instance.movingPlayer == true){
			//transform.position = Vector3.MoveTowards(transform.position, Tile.instance.transform.position, 0.5f);
		//}
	}

	public override void TurnUpdate ()
	{
		transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.red;

			if (GameManager.instance.moves <= 0) {

				transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.white;
				
				GameManager.instance.NextTurn();
			}


		base.TurnUpdate ();
	}
}

