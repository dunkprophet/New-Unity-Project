using UnityEngine;
using System.Collections;

public class IntroScript : MonoBehaviour {
	
	public static IntroScript instance;

	private GUISkin MetalGUISkin;
	private int scene;
	public float fadeSpeed = 0.5f;
	public static bool sceneStarting = false;

	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		instance = this;

	}

	void Start () {
		MetalGUISkin = Resources.Load("MetalGUISkin") as GUISkin;
		scene = 1;
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
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

	void OnGUI()
	{
		
		GUI.skin = MetalGUISkin;
		
		//START
		
		GUILayout.BeginArea (new Rect (Screen.width/4, Screen.height/4, 800, 410));
		
		if (scene == 1) {
			
			GUILayout.BeginVertical ("Memoir", GUI.skin.GetStyle("window"));
			
			GUILayout.Label ("My name is Calvin. \n\n I landed here three years ago, by the oiled-up shores of Chiba City. A week had gone by since I snuck onboard the massive cargo ship on my home island in the pacific, and the pain in my limbs could confirm it. Having spent most of my time hiding inside of a cramped container, watching for crewmen, and silently laying still for hours upon hours, the few mucles I had back then felt as if they had withered and died. As I jumped from the railings towards one of the empty docking stations, the pain made me promise myself to never again set foot upon a boat.");
			GUILayout.Label ("\n The first thing that struck me about the docking station was the smell- the smell of dead rats, and exploded sewer pipes. I had lucked out. The entire area appeared to have been abandoned. The lights placed onto poles around the place were unlit. None of them seemed functional, by either neglect, vandalism, or both. So it was dark, and the ground beneath my feet was sticky. Like someone had spilt out a reserve of artifical sweetener around the entirety of the docking station.");
			GUILayout.Label ("\n All I had to do was find my way to the streets. \n");
			
			if (GUILayout.Button ("Search the area.")) {
				scene = 2;
			}
			
			
			GUILayout.EndVertical ();
		} else if (scene == 2) {
			OverworldManager.sceneStarting = true;
			scene = 0;
			sceneStarting = true;
		}
		
		GUILayout.EndArea ();
	}
}
