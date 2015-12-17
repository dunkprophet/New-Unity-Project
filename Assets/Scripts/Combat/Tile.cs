using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {


	public Vector2 gridPosition = Vector2.zero;
	private bool tileSelected;
	public bool tileMarked;
	public Vector3 tilePosition;
	public bool tileHoldingMark;
	public Vector3 tempVector;

	// Use this for initialization
	void Start ()
	{
		tilePosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance (GameManager.instance.currentPlayerPosition, transform.position) < 1.1f) {
			transform.GetComponent<Renderer> ().material.color = Color.blue;
		}
		/*if (GameManager.instance.markedTiles.Count >= 4) {
			if (GameManager.instance.markedTiles.Peek() == transform.localPosition){
				transform.GetComponent<Renderer>().material.color = Color.white;
			}
			GameManager.instance.markedTiles.Dequeue();
		}*/
		/*if (tileMarked == true) {
			transform.GetComponent<Renderer>().material.color = Color.green;
		}*/

		/*if (GameManager.instance.currentPlayerPosition == tilePosition) {
			if (tileHoldingMark == true){
				GameManager.instance.markedTiles.Enqueue(transform.localPosition);
				tileHoldingMark = false;
			}
			tileMarked = true;
		}*/

		/*if (Vector3.Distance (transform.position, GameManager.instance.currentPlayerPosition) < 1) {
			transform.GetComponent<Renderer>().material.color = Color.green;
		}*/


	}

	void MakeMeWhite(){
		transform.GetComponent<Renderer>().material.color = Color.white;
	}
	void MakeMeGreen(){
		transform.GetComponent<Renderer> ().material.color = Color.green;
	}
	void MakeMeRed(){
		transform.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseEnter()
	{
		if (Vector3.Distance (GameManager.instance.currentPlayerPosition, transform.position) < 1.1f) {
			tileSelected = true;
		}
	}

	void OnMouseExit()
	{
		tileSelected = false;
		if (transform.GetComponent<Renderer> ().material.color == Color.blue) {
			transform.GetComponent<Renderer> ().material.color = Color.white;
		}

	}
	void OnMouseDown (){
		if (tileSelected == true) {
			GameManager.instance.movingPlayer = true;
			GameManager.instance.MoveCurrentPlayer(tilePosition);
			if (transform.GetComponent<Renderer> ().material.color != Color.green){
				tileHoldingMark = true;
			}
		}
	}
}
