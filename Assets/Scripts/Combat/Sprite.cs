using UnityEngine;
using System.Collections;

public class Sprite : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnMouseEnter()
	{
		transform.GetComponent<Renderer>().material.color = Color.red;

	}
	
	void OnMouseExit()
	{
		transform.GetComponent<Renderer>().material.color = Color.white;
		
	}

}
