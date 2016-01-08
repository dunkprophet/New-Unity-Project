using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed =10.0f;

	public Vector3 moveDestination;

	public int moves;
	public int movesPerMove;
	public bool selectable = true;

	void Awake ()
	{
		moveDestination = transform.position;

	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public bool IsSelectable()
	{
		return selectable;
	}

	public virtual void TurnUpdate()
	{


	}
}
