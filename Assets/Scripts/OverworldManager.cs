using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverworldManager : MonoBehaviour {
	public static OverworldManager instance;

	/*
	
	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;
	
	
	public int mapSize = 11;
	
	*/

	// Use this for initialization
	void Start () {	

	}
	
	// Update is called once per frame
	void Update () {
		OverworldPlayer.instance.moveZig();
	}

}
