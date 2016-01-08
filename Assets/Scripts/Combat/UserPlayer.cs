using UnityEngine;
using System.Collections;

public class UserPlayer : Player {

	int curMoves;

	// Use this for initialization
	void Start ()
	{
		this.moves = 3;
		this.movesPerMove = 1;
		curMoves = moves;

	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	public override void TurnUpdate ()
	{
		//transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.red;
		if (Mathf.Abs(transform.position[0] - moveDestination[0]) <= 1 & Mathf.Abs(transform.position[2] - moveDestination[2]) <= 1 ) {
			if (Vector3.Distance(moveDestination, transform.position) > 0.1f) {
				transform.position += (moveDestination -transform.position).normalized * moveSpeed * Time.deltaTime;

				if (Vector3.Distance(moveDestination, transform.position) <= 0.1f) {

					//transform.FindChild("Sprite").transform.GetComponent<Renderer>().material.color = Color.white;
					curMoves--;
					if (curMoves <= 0){
						GameManager.instance.NextTurn();
						transform.position = moveDestination;
					}
				}
			}
		}
		base.TurnUpdate ();

	}
}
