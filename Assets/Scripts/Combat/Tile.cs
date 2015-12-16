using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {


	public Vector2 gridPosition = Vector2.zero;
	private bool tileSelected;
	private bool tileMarked;
	public Vector3 tilePosition;

	// Use this for initialization
	void Start ()
	{
		tilePosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (tileMarked == true) {
			transform.GetComponent<Renderer>().material.color = Color.green;
		}

		if (GameManager.instance.currentPlayerPosition == tilePosition) {
			tileMarked = true;
		}

		if (Vector3.Distance (transform.position, GameManager.instance.currentPlayerPosition) < 1) {
			transform.GetComponent<Renderer>().material.color = Color.green;
		}




	}

	void OnMouseEnter()
	{
		if (Vector3.Distance (GameManager.instance.currentPlayerPosition, transform.position) < 1.1f) {
			tileSelected = true;
		}
		transform.GetComponent<Renderer>().material.color = Color.blue;

	}

	void OnMouseExit()
	{
		tileSelected = false;
		transform.GetComponent<Renderer>().material.color = Color.white;


	}
	void OnMouseDown (){
		if (tileSelected == true) {
			GameManager.instance.movingPlayer = true;
			GameManager.instance.MoveCurrentPlayer(tilePosition);
		}
	}

}
