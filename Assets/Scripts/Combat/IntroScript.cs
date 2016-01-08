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
			
			GUILayout.BeginVertical ("ContainerCorp コンテナ 2.43, BLACK ICE DEFENSE", GUI.skin.GetStyle("window"));
			
			GUILayout.Label ("\n \"DO YOU HEAR ME? GOOD. YOU'RE IN. THE ONLY REASON THIS PLACE FRIED ME, WAS THE BLACK ICE PROGRAM INSTALLED ON THE SERVER. YEAH, BLACK ICE. PROGRAMMED TO KILL INTRUDERS. I REALLY DID NOT EXPECT SUCH A PROGRAM TO BE ON A SHITTY LITTLE PANEL AT THE BACK OF THE HARBOR, BUT HEY. WE ALL MAKE MISTAKES.\"" +
				"\n\n \"LUCKILY FOR YOU, I MANAGED TO DESTROY MOST OF IT BEFORE I BURNED. SO ALL THAT IS LEFT NOW, IS TWO TINY SHIELD PROGRAMS. YOU DO KNOW HOW TO FIGHT IN CYBERSPACE, DO YOU?\"" +
			                 "\n\n \"I, uh...\"\n\n \"OF COURSE YOU DON'T. LISTEN, IT'S REAL SIMPLE. JUST CLICK ON THE SPAWNER, AND CHOOSE A PROGRAM. ONLY ONE IS PREINSTALLED AT THE MOMENT. THEN START THE MATCH AND MOVE AROUND THE SERVERSPACE, ONE STEP AT A TIME. EVERY STEP GIVES YOU MORE SERVER CONTROL, WHICH ALSO REPRESENTS THE STRENGHT YOUR PROGRAMS HAS OVER THE SERVERSPACE. YOUR HEALTHBAR, ESSENTIALLY. YOU'VE PLAYED VIDEO GAMES, RIGHT? SURE YOU HAVE. OTHER THAN THAT, YOU JUST HAVE TO ATTACK THE OTHER PROGRAMS. SINCE THEY'RE SPECIFICALLY *BLACK* INTRUSION COUNTERMESURE ELECTRONICS, YOU'LL DIE IF YOU LOSE. SO DON'T LOSE.\"\n");
			
			if (GUILayout.Button ("\"...\"")) {
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
