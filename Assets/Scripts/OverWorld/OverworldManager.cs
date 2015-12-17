using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class OverworldManager : MonoBehaviour {
	public static OverworldManager instance;
	/*
	
	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;
	
	
	public int mapSize = 11;
	
	*/

	public float fadeSpeed = 0.5f;

	public static bool sceneStarting = false;

	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	// Use this for initialization
	void Start () {	
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		OverworldPlayer.instance.moveZig();

		if (sceneStarting == true) {
			// ... call the StartScene function.
			StartScene ();
		}

	}
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(GetComponent<GUITexture>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			GetComponent<GUITexture>().color = Color.clear;
			GetComponent<GUITexture>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}

}
