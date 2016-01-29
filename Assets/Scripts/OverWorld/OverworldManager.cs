using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent(typeof(AudioSource))]
public class OverworldManager : MonoBehaviour {

	public static OverworldManager instance;

	//[System.Serializable]
	//public struct PlayerData {
		public int money;
		public int coding;
		
	//}

	public int scene = 0;

	public float fadeSpeed = 0.5f;
	private int textFlash = 0;

	private GUISkin MetalGUISkin;
	private GUISkin DOS2;
	public Texture scene1Background;
	public Texture scene1Background1;
	public Texture scene1Background2;
	public Texture scene2Background;
	public Texture scene3Background;
	public Texture scene4Background;
	public Texture scene5Background;
	public Texture scene6Background;
	public Color lerpingColor;
	public bool fadeToWhite = false;
	public bool fadeToBlack = false;

	public string tempString; 

	public static bool sceneStarting = false;
	public static bool sceneEnding = false;
	public bool menuStarted = true;
	public bool matchStarted = false;
	public bool sceneStarted = false;

	public bool deleteOldText = true;

	//private List<string> Log = new List<string>();
	public string textPrint = "";
	private string logText = "";
	public string text = "";
	public string textForLog = "";

	public Rect GIWindow;
	public float guiAlpha;
	public Color tempColor;

	public AudioSource song1;
	public AudioSource song2;
	public AudioSource song3;
	public AudioSource song4;

	public AudioSource audio;
	public AudioClip flickeringLight;
	public AudioClip click;

	public int i;
	public int x = 0;
	public float y = 0.0f;
	public bool writeText = true;

	public int dialogController = 0;
	public int dialogControllerMenu = 0;

	public int defaultWidth;

	public bool responseWanted = false;

	public string response1;
	public string response2;
	public string response3;
	public string response4;

	public bool dialogReady = false;
	public bool slowText = false;
	public bool nextClicked = false;

	public bool response1picked = false;
	public bool response2picked = false;
	public bool response3picked = false;
	public bool response4picked = false;

	//STORY BOOLEANS
	public bool govermentKnowsName;

	public Vector2 scrollPosition;

	void Awake ()
	{
		DontDestroyOnLoad(transform.gameObject);
		instance = this;
		// Set the texture so that it is the the size of the screen and covers it.
		//GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	// Use this for initialization
	void Start () {	

		defaultWidth = 1080;
		InvokeRepeating("flash", 0, 0.3f);
		InvokeRepeating("textPrinter", 0, 0.02f);
		InvokeRepeating ("fader", 0, 0.01f);
		InvokeRepeating ("backgroundAnimator", 0, 0.01f);

		i = 0;

		money = 6000;

		MetalGUISkin = Resources.Load ("MetalGUISkin") as GUISkin;
		DOS2 = Resources.Load ("DOS2") as GUISkin;

		scene1Background1 = Resources.Load ("Textures/background_scene1_light") as Texture;
		scene1Background2 = Resources.Load ("Textures/background_scene1_nolight") as Texture;
		scene2Background = Resources.Load ("Textures/arrow") as Texture;
		flickeringLight = Resources.Load ("audio/flickering") as AudioClip;
		click = Resources.Load ("audio/click") as AudioClip;

		//song1 = Resources.Load ("audio/Shinjuku_Golden_Street") as AudioClip;
		//song2 = Resources.Load ("audio/Shinjuku_Golden_Street") as AudioSource;
		//song3 = Resources.Load ("audio/Shinjuku_Golden_Street") as AudioSource;
		//song4 = Resources.Load ("audio/Shinjuku_Golden_Street") as AudioSource;

		lerpingColor = Color.black;
		guiAlpha = 0;
		//GIWindow = new Rect (Screen.width / 4 + 20, 10, Screen.width - 30 - Screen.width / 4, Screen.height * 0.75f);

		/*if (sceneStarting == true) {

		}*/
	}
	
	// Update is called once per frame
	void Update () {


		//textPrint is what the player sees
		//logText is the string that the game adds onto
		//text is the text that is added onto textLog
		//Take textLogs length, skip backwards with the lenght of text, and slowly add text to textLogPrinta
		
		scrollPosition.y = Mathf.Infinity;

		if (sceneEnding == true) {
			EndScene();
		}

		if (sceneStarting == true) {
			// ... call the StartScene function.
			StartScene ();
		}

		if (menuStarted == false && Input.GetMouseButtonDown (0)) {
			if (dialogReady == true && responseWanted == false) {
				print ("You pressed!");
				dialogController++;
				if (deleteOldText == true){
					textPrint = "";
					tempString = "";
				} else {
					tempString = tempString + text;
				}
				i = 0;
				dialogReady = false;
				audio.PlayOneShot(click);
			} else if (responseWanted == false) {
				i = text.Length;
				textPrint = tempString + text;

			}
		}

	}

	void textPrinter () {
		if (i < text.Length && menuStarted == false) {

			dialogReady = false;
			textPrint = textPrint + text [i];
			i++;
			
		} else if (responseWanted == false && menuStarted == false) {
			dialogReady = true;
		}
	}

	void flash() {
		textFlash++;
		if (textFlash > 3) {
			textFlash = 0;
		}
	}

	public void playMusic (int song){
		if (song == 1) {
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		}
		if (song == 2) {
		}
		if (song == 3) {
		}
		if (song == 4) {
		}
	}

	void sceneDialog (int response)
	{
		//Dialog between characters in game
		if (response == 1) {
			response1picked = true;
			responseWanted = false;
			dialogController++;
			i = 0;
		}
		if (response == 2) {
			response2picked = true;
			responseWanted = false;
			dialogController++;
			i = 0;
		}
		if (response == 3) {
			response3picked = true;
			responseWanted = false;
			dialogController++;
			i = 0;
		}
		if (response == 4) {
			response4picked = true;
			responseWanted = false;
			dialogController++;
			i = 0;
		}
		textPrint = "";
		response1 = "";
		response2 = "";
		response3 = "";
		response4 = "";
	}
	public IEnumerator pause(float delay) {
		print(Time.time);
		yield return new WaitForSeconds(20);
		print(Time.time);
	}

	//Fade Code here ---------------------------------------------------------------------------------------------------------------
	public void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	public void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}
	public void StartScene ()
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
	public void EndScene ()
	{
		// Fade the texture to clear.
		sceneStarting = true;
		FadeToBlack();
		
		// If the texture is almost clear...
		if(GetComponent<GUITexture>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			GetComponent<GUITexture>().color = Color.clear;
			GetComponent<GUITexture>().enabled = false;
			
			// The scene is no longer starting.

		}
	}
	// ---------------------------------------------------------------------------------------------------------

	/*public void OnMouseDown(){
		nextClicked = true;
	}*/

	public void updateLog(){
		logText = logText + textForLog;
	}
	public void eraseLog(){
		logText = "";
	}
	public void fader(){
		if (fadeToWhite == true) {
			guiAlpha = guiAlpha + 0.005f;
			if (guiAlpha >= 1){
				fadeToWhite = false;
			}
		}
		if (fadeToBlack == true) {
			guiAlpha = guiAlpha - 0.005f;
			if (guiAlpha <= 0){
				fadeToWhite = false;
			}
		}
	}
	public void backgroundAnimator(){
		if (scene == 1 && guiAlpha >= 1) {
			x++;
			if (x >= 2500) {
				x = 0;
			}
			if (x == 5) {
				scene1Background = scene1Background2;
			}
			if (x == 12) {
				scene1Background = scene1Background1;
				audio.PlayOneShot(flickeringLight);
			}
			if (x == 120) {
				scene1Background = scene1Background2;
			}
			if (x == 128) {
				scene1Background = scene1Background1;
				audio.PlayOneShot(flickeringLight);
			}
			if (x == 350) {
				scene1Background = scene1Background2;
			}
			if (x == 355) {
				scene1Background = scene1Background1;
				audio.PlayOneShot(flickeringLight);
			}
			if (x == 358) {
				scene1Background = scene1Background2;
			}
			if (x == 365) {
				scene1Background = scene1Background1;
				audio.PlayOneShot(flickeringLight);
			}
			if (x == 1030) {
				scene1Background = scene1Background2;
			}
			if (x == 1045) {
				scene1Background = scene1Background1;
				audio.PlayOneShot(flickeringLight);
			}
			if (x == 2069) {
				scene1Background = scene1Background2;
			}
			if (x == 2079) {
				scene1Background = scene1Background1;
				audio.PlayOneShot(flickeringLight);
			}
		}
	}
	//Text & Interface code here
	public void OnGUI() {

		GUI.skin = MetalGUISkin;

		//fontSize
		GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (defaultWidth* 1.0f));
		GUI.skin.button.fontSize = Mathf.RoundToInt (18 * Screen.width / (defaultWidth * 1.0f));


		//GUI.color = Color.clear;
		if (scene == 1) {
			tempColor = new Color(guiAlpha, guiAlpha, guiAlpha, guiAlpha);
			GUI.color = tempColor;
			//Background
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), scene1Background);
			//Characters
			GUI.DrawTexture(new Rect(Screen.width-500, 0, Screen.width/4, Screen.height/4), scene2Background);
			GUI.color = Color.white;
		}
		if (scene == -2) {
			tempColor = new Color(guiAlpha, guiAlpha, guiAlpha, guiAlpha);
			GUI.color = tempColor;
			//Background
			//GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), scene1Background);
			//Characters
			//GUI.DrawTexture(new Rect(Screen.width-500, 0, Screen.width/4, Screen.height/4), scene2Background);
			GUI.color = Color.white;
		}





		if (menuStarted == false && scene >= 0) {

			GUILayout.BeginArea (new Rect (Screen.width / 6, Screen.height / 1.5f, Screen.width / 1.5f, Screen.height / 5));
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("window"));

			if (textFlash <= 1 && dialogReady == true) {
				GUILayout.Label (textPrint + "_");
			} else {
				GUILayout.Label (textPrint + " ");
			}

			GUILayout.EndVertical ();
			GUILayout.EndArea ();

		}

		if (responseWanted == true) {

			GUILayout.BeginArea (new Rect (Screen.width / 3, Screen.height / 2, Screen.width / 2, Screen.height / 8));
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("window"));

			//Dialog buttons
			if (responseWanted == true) {
				if (GUILayout.Button (response1)) {
					sceneDialog (1);
				}
				if (GUILayout.Button (response2)) {
					sceneDialog (2);
				}
				if (GUILayout.Button (response3)) {
					sceneDialog (3);
				}
				if (GUILayout.Button (response4)) {
					sceneDialog (4);
				}
			} 
			
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}


		if (menuStarted == true) {

			//Log on interface, all text displayed here
			GUILayout.BeginArea (new Rect (10, 10, Screen.width / 3, Screen.height - 10));
			//GUILayout.BeginVertical ("コンピュータログ", GUI.skin.GetStyle ("window"));
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("box"));
			scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width / 3 - 27), GUILayout.Height (Screen.height - 60));


			if (textFlash <= 1) {
				GUILayout.Label (logText + "_");
			} else {
				GUILayout.Label (logText + " ");
			}


			GUILayout.EndScrollView ();
			GUILayout.EndVertical ();
			GUILayout.EndArea ();

			//Graphical Interface
			GUILayout.BeginArea (new Rect (Screen.width / 3 + 20, 10, Screen.width - 30 - Screen.width / 3, Screen.height * 0.75f));
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("GI"));
			if (menuStarted == true) {
				GUILayout.Label("ICEBREAKER");
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();

			//Buttons on interface
			GUILayout.BeginArea (new Rect (Screen.width / 3 + 20, Screen.height * 0.76f, Screen.width - 30 - Screen.width / 3, Screen.height * 0.238f));
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("box"));

			//MENY, START LOAD OPTIONS EXIT
			if (menuStarted == true) {
				if (GUILayout.Button (">Application.LoadLevel(1);")) {
					scene = 1;
					dialogController = -1;
					menuStarted = false;
					//Application.LoadLevel (2);
					//matchStarted = true;
				}
				if (GUILayout.Button (">File.Open(\"saveFile\");[WIP]")) {
					//Load save files
				}
				if (GUILayout.Button (">Player Settings...[WIP]")) {
					//Options
				}
				if (GUILayout.Button (">Application.Quit();")) {
					Application.Quit ();
				}
			} else {
				if (GUILayout.Button ("Skip")) {
					dialogController = 9;
				}
			}


			GUILayout.EndVertical ();
			GUILayout.EndArea ();

		}
	}
}
