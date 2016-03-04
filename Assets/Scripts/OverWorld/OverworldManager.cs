using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//[RequireComponent(typeof(AudioSource))]
public class OverworldManager : MonoBehaviour {

	public static OverworldManager instance;

	//[System.Serializable]
	//public struct PlayerData {
		public int money;
		public int coding;
		public int day;
		public string date;
		
	//}

	public int scene = 0;
	public bool freeMode = false;

	public float fadeSpeed = 0.5f;
	public int textFlash = 0;

	public GUISkin MetalGUISkin;
	public GUISkin DOS2;

	//Textures
	public Texture background;
	public Texture scene1Background1;
	public Texture scene1Background2;
	public Texture scene1Background3;
	public Texture scene2Background;
	public Texture scene3Background;
	public Texture scene4Background;
	public Texture scene5Background;
	public Texture scene6Background;
	public Texture spriteMax;
	public Texture spriteGOD;
	public Texture spriteBug;
	public Texture spriteGolem;
	public Texture spriteBreaker;

	public Texture map;

	//Fade stuff
	public Color lerpingColor;
	public bool fadeToWhite = false;
	public bool fadeToBlack = false;
	public bool fadeInMusic = false;
	public bool fadeOutMusic = false;


	public string tempString; 

	public static bool sceneStarting = false;
	public static bool sceneEnding = false;
	public bool menuStarted = true;
	public bool matchStarted = false;
	public bool sceneStarted = false;

	public bool saveFilesOpen = false;
	public bool loadFilesOpen = false;
	public bool settingsOpen = false;
	public bool newGameOpen = false;
	public bool newGameOpen2 = false;
	public bool saveEraseOpen = false;

	public bool deleteOldText = true;

	public bool once = true;

	public bool isometricBattle = false;

	//private List<string> Log = new List<string>();
	public string textPrint = "";
	private string logText = "";
	public string text = "";
	public string textForLog = "";
	public string computerText = "";
	public bool textBoxShown = false;
	public string messageText = "";
	public string nodeTitle = "";
	public string nodeDescription = "";
	public string programDescription = "";

	public int nodeNumber;
	public bool showNode = false;
	public bool showMaxShop = false;
	public bool grayNode = false;
	public bool nodeHover = false;
	public bool tempBool = false;

	public Vector3 selectedNode;
	public Vector3 cameraPosition;

	public bool showProgramList = false;

	//public List<string> messages = new List<string>();
	//public List<string> messagesTitle = new List<string>();
	//public List<string> sites = new List<string> ();
	public List<string> placesToGo = new List<string> ();
	public List<GameObject> programList = new List<GameObject> ();
	//public List<string> programText = new List<string> ();

	public GameObject Golem;
	public GameObject Bug;
	public GameObject GOD;
	public GameObject Breaker;

	public List<GameObject> mailList = new List<GameObject> ();
	public List<int> netscapeNodes = new List<int> ();

	public List<bool> choices = new List<bool> ();

	public List<GameObject> shopMaxList = new List<GameObject> ();

	public Rect GIWindow;
	public float guiAlpha;
	public Color tempColor;
	public Rect netscapeBoxSize;
	public Rect windowRect = new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2);

	//Audio Stuff
	public AudioSource audio;

	public AudioClip flickeringLight;
	public AudioClip click;
	public AudioClip train;
	
	public AudioClip song1;
	public AudioClip song2;
	public AudioClip song3;
	public AudioClip song4;

	public int i;
	public int x = 0;
	public float y = 0.0f;
	public bool writeText = true;

	public int tempInt = 0;

	public int dialogController = 0;
	public int dialogControllerMenu = 0;
	public float textSpeed = 0.02f;

	public int defaultWidth;

	public bool responseWanted = false;

	public string response1;
	public string response2;
	public string response3;
	public string response4;

	public string userName;
	public string password;

	public int unreadMessages = 0;
	public bool netscapeOpen = false;
	public bool inboxOpen = false;
	public bool mapOpen = false;

	public int selectedItem = -1;
	public GameObject selectedObject;

	public Rect textBoxPosition;
	public string textBoxStyle;
	public float responseBoxSize;

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
	public Vector2 scrollPosition2;
	public Vector2 scrollPosition3;
	public Vector2 scrollPosition4;

	[Serializable]
	public abstract class SaveGame
	{
	}
	[Serializable]
	public class MySaveGame : SaveGame
	{

		public string userName;

		public List<bool> storyChoices;
		public List<GameObject> programList;
		public List<GameObject> mailList;
		public List<int> netscapeNodes;
		public List<string> placesToGo;

		public int money;
		public int coding;
		public int day;
		public string date;

		public int scene;
		public bool freeMode;
		public int dialogController;
		public bool textBoxShown;
		public bool deleteOldText;
		public bool fadeToWhite;
		public bool fadeToBlack;

		public bool netscapeOpen;
		public bool inboxOpen;
		public bool mapOpen;

		public bool responseWanted;
		public bool response1picked;
		public bool response2picked;
		public bool response3picked;
		public bool response4picked;
		
	}
	/*[Serializable]
	public class MySaveGame : SaveGame
	{
		public string playerName = "Calvin";

		
		public int HighScore { get; set; }

	}*/
	public static class SaveGameSystem
	{
		public static bool SaveGame(SaveGame saveGame, string name)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			
			using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Create))
			{
				try
				{
					formatter.Serialize(stream, saveGame);
				}
				catch (Exception)
				{
					return false;
				}
			}
			
			return true;
		}
		
		public static SaveGame LoadGame(string name)
		{
			if (!DoesSaveGameExist(name))
			{
				return null;
			}
			
			BinaryFormatter formatter = new BinaryFormatter();
			
			using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Open))
			{
				try
				{
					return formatter.Deserialize(stream) as SaveGame;
				}
				catch (Exception)
				{
					return null;
				}
			}
		}
		
		public static bool DeleteSaveGame(string name)
		{
			try
			{
				File.Delete(GetSavePath(name));
			}
			catch (Exception)
			{
				return false;
			}
			
			return true;
		}
		
		public static bool DoesSaveGameExist(string name)
		{
			return File.Exists(GetSavePath(name));
		}
		
		private static string GetSavePath(string name)
		{
			return Path.Combine(Application.dataPath, name + ".sav");
		}
	}

	void Awake ()
	{
		DontDestroyOnLoad(transform.gameObject);
		instance = this;
		// Set the texture so that it is the the size of the screen and covers it.
		//GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	// Use this for initialization
	void Start () {

		Application.LoadLevel (1);
		defaultWidth = 1080;
		InvokeRepeating("flash", 0, 0.3f);
		InvokeRepeating("textPrinter", 0, textSpeed);
		InvokeRepeating ("fader", 0, 0.01f);
		InvokeRepeating ("fadeMusic", 0, 0.01f);
		InvokeRepeating ("backgroundAnimator", 0, 0.01f);

		i = 0;

		money = 6000;

		MetalGUISkin = Resources.Load ("MetalGUISkin") as GUISkin;
		DOS2 = Resources.Load ("DOS2") as GUISkin;

		scene1Background1 = Resources.Load ("Textures/background_scene1_light") as Texture;
		scene1Background2 = Resources.Load ("Textures/background_scene1_nolight") as Texture;
		scene1Background3 = Resources.Load ("Textures/background_scene1_streets") as Texture;
		//background = Resources.Load ("Textures/arrow") as Texture;
		spriteMax = Resources.Load ("Textures/KalvinItIsIHackerman") as Texture;
		spriteBreaker = Resources.Load ("Textures/Shield_2_001") as Texture;
		spriteGolem = Resources.Load ("Textures/Golem_1_001") as Texture;
		spriteBug = Resources.Load ("Textures/Bug_1_001") as Texture;
		spriteGOD = Resources.Load ("Textures/Bug_3_001") as Texture;

		GOD = Resources.Load ("Prefabs/GOD") as GameObject;
		Bug = Resources.Load ("Prefabs/Bug") as GameObject;
		Golem = Resources.Load ("Prefabs/Golem") as GameObject;
		Breaker = Resources.Load ("Prefabs/Breaker") as GameObject;




		map = Resources.Load ("Textures/map") as Texture;

		flickeringLight = Resources.Load ("audio/flickering") as AudioClip;
		click = Resources.Load ("audio/click") as AudioClip;
		train = Resources.Load ("audio/train") as AudioClip;
		song1 = Resources.Load ("audio/Stavarsky - Posthuman") as AudioClip;
		song2 = Resources.Load ("audio/Shinjuku_Golden_Street") as AudioClip;

		//song1 = Resources.Load ("audio/Shinjuku_Golden_Street") as AudioClip;
		//song2 = Resources.Load ("audio/Shinjuku_Golden_Street") as AudioSource;
		//song3 = Resources.Load ("audio/Shinjuku_Golden_Street") as AudioSource;
		//song4 = Resources.Load ("audio/Shinjuku_Golden_Street") as AudioSource;

		lerpingColor = Color.black;
		guiAlpha = 0;
		//GIWindow = new Rect (Screen.width / 4 + 20, 10, Screen.width - 30 - Screen.width / 4, Screen.height * 0.75f);

		/*if (sceneStarting == true) {

		}*/
		shopMaxList.Add (GOD);
		shopMaxList.Add (Breaker);
		shopMaxList.Add (Breaker);
		shopMaxList.Add (Bug);
		shopMaxList.Add (Bug);
	}
	
	// Update is called once per frame
	void Update () {


		//textPrint is what the player sees
		//logText is the string that the game adds onto
		//text is the text that is added onto textLog
		//Take textLogs length, skip backwards with the lenght of text, and slowly add text to textLogPrinta
		
		//scrollPosition.y = Mathf.Infinity;

		if (sceneEnding == true) {
			EndScene();
		}

		if (sceneStarting == true) {
			// ... call the StartScene function.
			StartScene ();
		}

		if (menuStarted == false && Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
			if (freeMode == false && dialogReady == true && responseWanted == false) {
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
				GetComponent<AudioSource>().PlayOneShot(click);
			} 
			else if (freeMode == true){
				if (matchStarted == false){
					text = "";
					textPrint = "";
					textBoxShown = false;
				} else {

				}
			} 
			else if (responseWanted == false) {
				i = text.Length;
				textPrint = tempString + text;

			} 
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (menuStarted == false){
				menuStarted = true;
			} else {menuStarted = false;}
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
		GetComponent<AudioSource>().PlayOneShot(click);
	}
	public void nodeClicked (int node, string nodeName, string nodeText, Vector3 nodePosition){

		selectedNode = nodePosition;
		selectedNode = new Vector3 (selectedNode.x - 20, selectedNode.y + 20, selectedNode.z - 20);
		nodeTitle = nodeName;
		nodeDescription = nodeText;
		nodeNumber = node;
		showNode = true;
	}
	public void nodeHovered (string nodeName){
		nodeTitle = nodeName;
		nodeHover = true;
		tempBool = true;

	}
	public void nodeUnHovered (){
		nodeTitle = "";
		nodeHover = false;
	}
	public IEnumerator pause(float delay) {
		print(Time.time);
		yield return new WaitForSeconds(20);
		print(Time.time);
	}
	public void cameraFixed (Vector3 cameraFixedPoint){
		cameraPosition = Camera.main.transform.position;
		CameraNetscape.instance.transform.position = Vector3.Lerp(cameraPosition, cameraFixedPoint, Time.deltaTime * 2.0f);
		//MAKE THIS A LERP ---------------------------------------------------------------------------------------------------------------------- : )
		// new Vector3 (cameraFixedPoint.x - 20, cameraFixedPoint.z + 55, cameraFixedPoint.z - 20);
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
			guiAlpha = guiAlpha + 0.001f;
			if (guiAlpha >= 1){
				fadeToWhite = false;
			}
		}
		if (fadeToBlack == true) {
			guiAlpha = guiAlpha - 0.001f;
			if (guiAlpha <= 0){
				fadeToBlack = false;
			}
		}

	}
	public void fadeMusic(){
		if (fadeOutMusic == true) {
			GetComponent<AudioSource>().volume = GetComponent<AudioSource>().volume - 0.001f;
			if (GetComponent<AudioSource>().volume <= 0){
				GetComponent<AudioSource>().Stop();
				GetComponent<AudioSource>().volume = 0.1f;
				fadeOutMusic = false;
			}
		}
		if (fadeInMusic == true) {
			GetComponent<AudioSource>().volume = GetComponent<AudioSource>().volume - 0.001f;
			if (GetComponent<AudioSource>().volume >= 0.1f){
				GetComponent<AudioSource>().volume = 0.1f;
				fadeInMusic = false;
			}
		}
	}
	public void backgroundAnimator(){
		if (scene == 1 && guiAlpha >= 1 && dialogController >= 30 && dialogController <= 64) {
			x++;
			if (x >= 2500) {
				x = 0;
			}
			if (x == 5) {
				background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 12) {
				background = scene1Background1;
			}
			if (x == 120) {
				background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 128) {
				background = scene1Background1;
			}
			if (x == 350) {
				background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 355) {
				background = scene1Background1;
			}
			if (x == 358) {
				background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 365) {
				background = scene1Background1;
			}
			if (x == 1030) {
				background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 1045) {
				background = scene1Background1;
			}
			if (x == 2069) {
				background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 2079) {
				background = scene1Background1;
			}
		}
		if (scene == 1 && dialogController >= 97 && dialogController <= 123) {
			background = scene1Background3;
		}
	}
	//Text & Interface code here
	public void OnGUI() {

		GUI.skin = MetalGUISkin;

		//fontSize
		GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (defaultWidth * 1.0f));
		GUI.skin.button.fontSize = Mathf.RoundToInt (18 * Screen.width / (defaultWidth * 1.0f));

		tempColor = new Color (guiAlpha, guiAlpha, guiAlpha, guiAlpha);
		GUI.color = tempColor;
		//Background
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), background);
		//Characters
		//GUI.DrawTexture(new Rect(Screen.width-500, 0, Screen.width/4, Screen.height/4), scene2Background);
		GUI.color = Color.white;
		
		//GUI.color = Color.clear;
		if (scene == 1) {

		}

		//COMPUTER SCREEN -----------------------------------------------------------------------------------------------------------------------------------ZZ

		if (scene == 2 && menuStarted == false && freeMode == true) {

			computerText = day.ToString () + "日目 - Day " + day.ToString () + "\n" + date + "\n¥" + money;

			if (netscapeOpen == false) {
				GUILayout.BeginArea (new Rect (Screen.width * 0.05f, Screen.height * 0.05f, Screen.width - Screen.width * 0.1f, Screen.height - Screen.height * 0.1f));

				GUILayout.BeginVertical ("User_Calvin_DOS65 Very.Alpha", GUI.skin.GetStyle ("comp"));
				//Day Shower
				if (textFlash <= 1 && dialogReady == true) {
					GUILayout.Label (computerText + "_");
				} else {
					GUILayout.Label (computerText + " ");
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (netscapeOpen == false && inboxOpen == false && mapOpen == false) {
				GUILayout.BeginArea (new Rect (Screen.width * 0.07f, Screen.height / 3, Screen.width / 6, Screen.height / 4));
				GUILayout.BeginVertical ("Menu", GUI.skin.GetStyle ("comp"));

				if (GUILayout.Button ("Enter Netscape")) {
					netscapeOpen = true;
				}
				for (int k = 0; k < mailList.Count; k++){
					if (mailList[k].GetComponent<Mail>().read == false){
						unreadMessages++;
					}
				
				}
				if (GUILayout.Button ("Inbox (" + unreadMessages + ")")) {
					unreadMessages = 0;
					inboxOpen = true;
				}
				unreadMessages = 0;
				if (GUILayout.Button ("Log Off")) {
					if (placesToGo.Count <= 0) {
						i = 0;
						textBoxShown = true;
						text = "You're kidding, right?";
					} else {
						mapOpen = true;
					}
				}

				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (mapOpen == true) {
				GUILayout.BeginArea (new Rect (Screen.width * 0.07f, Screen.height / 3, Screen.width / 6, Screen.height / 4));
				GUILayout.BeginVertical ("Map", GUI.skin.GetStyle ("comp"));
				for (int d = 0; d < placesToGo.Count; d++) {
					if (GUILayout.Button (placesToGo [d])) {

					}
				}
				if (GUILayout.Button ("Return")) {
					mapOpen = false;
				}

				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (inboxOpen == true) {
				GUILayout.BeginArea (new Rect (Screen.width * 0.07f, Screen.height / 3, Screen.width / 3, Screen.height / 2));
				GUILayout.BeginVertical ("mail.co.com", GUI.skin.GetStyle ("comp"));
				scrollPosition2 = GUILayout.BeginScrollView (scrollPosition2, GUILayout.Width (Screen.width / 3 -24), GUILayout.Height (Screen.height / 2.52f));
				for (int m = mailList.Count-1; m >= 0; m--) {
					if (GUILayout.Button (mailList[m].GetComponent<Mail>().date + " - "+ mailList[m].GetComponent<Mail>().subject)) {
						messageText = mailList[m].GetComponent<Mail>().content;
						tempInt = m;
						mailList[m].GetComponent<Mail>().read = true;
						OverworldManager.instance.netscapeNodes.Add(mailList[m].GetComponent<Mail>().unlockingNode);

					}
				}
				GUILayout.EndScrollView();
				if (GUILayout.Button ("Return")) {
					inboxOpen = false;
					messageText = "";
				}
				//GUILayout.EndScrollView ();
				GUILayout.EndVertical ();
				GUILayout.EndArea ();

				GUILayout.BeginArea (new Rect (Screen.width / 2.3f, Screen.height / 6, Screen.width / 2, Screen.height / 1.5f));

				GUILayout.BeginVertical ("From: "+mailList[tempInt].GetComponent<Mail>().sender, GUI.skin.GetStyle ("comp"));
				scrollPosition3 = GUILayout.BeginScrollView (scrollPosition3, GUI.skin.GetStyle ("empty"), GUILayout.Width (Screen.width / 2 - 24), GUILayout.Height (Screen.height / 1.65f));
				if (messageText != ""){
				GUILayout.Label (mailList[tempInt].GetComponent<Mail>().time+" "+mailList[tempInt].GetComponent<Mail>().subject+"\n"+messageText);
				}
				GUILayout.EndScrollView ();
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (netscapeOpen == true) {

				if (showProgramList == true && matchStarted == false) {
					GUILayout.BeginArea (new Rect (Screen.width / 80, Screen.height / 13, Screen.width / 5, Screen.height / 4));
					GUILayout.BeginVertical ("Program.List", GUI.skin.GetStyle ("netscape"));
					scrollPosition4 = GUILayout.BeginScrollView (scrollPosition4, GUI.skin.GetStyle ("empty"), GUILayout.Width (Screen.width / 5 - 24), GUILayout.Height (Screen.height / 5.1f));
					for (int d = 0; d < programList.Count; d++) {
						if (GUILayout.Button (programList [d].GetComponent<Stats> ().name)) {
							selectedItem = d;
							tempString = "";
						}
					}
					GUILayout.EndScrollView ();
					GUILayout.EndVertical ();
					GUILayout.EndArea ();
					



						GUILayout.BeginArea (new Rect (Screen.width / 80, Screen.height / 70 + Screen.height / 4 + Screen.height / 13, Screen.width / 5, Screen.height *0.6f));
						GUILayout.BeginVertical ("Program.Info", GUI.skin.GetStyle ("netscape"));
						//GUI. (new Rect(0,0,32,32), selectedObject.GetComponent<Stats>().name);
					if (selectedItem != -1) {
						GUI.skin.label.fontSize = Mathf.RoundToInt (22 * Screen.width / (defaultWidth* 1.0f));
						GUILayout.Label (programList [selectedItem].GetComponent<Stats> ().name);
						GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (defaultWidth * 1.0f));
						GUILayout.Label ("SS:    " + programList [selectedItem].GetComponent<Stats> ().maxHealth);
						GUILayout.Label ("Moves: " + programList [selectedItem].GetComponent<Stats> ().maxMoves);
						GUILayout.Label ("Description:\n" + programList [selectedItem].GetComponent<Stats> ().description);
						GUILayout.Label ("Abilities:");
						for (int d = 0; d < programList[selectedItem].GetComponent<UserPlayer>().abilities.Count; d++) {
							if (GUILayout.Button ("-" + programList [selectedItem].GetComponent<UserPlayer> ().abilities [d])) {
								tempString = programList [selectedItem].GetComponent<UserPlayer> ().abilities [d];
							}
						}
						if (tempString == "Slash") {
							GUILayout.Label ("A melee attack that destroys 3 SS.");
						}
						if (tempString == "Glitch") {
							GUILayout.Label ("A melee attack that destroys 2 SS.");
						}
						if (tempString == "Crash") {
							GUILayout.Label ("A strong attack that reaches 2 tiles away from the user, and deals 8 damage.");
						}
					} else {tempString = "";}
						GUILayout.EndVertical ();
						GUILayout.EndArea ();
					
					
				}

				//AREA -----------------------------------------------------------------------------------------------------------
				GUILayout.BeginArea (new Rect (0, 0, Screen.width / 3, Screen.height / 12));
				GUILayout.BeginHorizontal ("", GUI.skin.GetStyle ("empty"));
				if (matchStarted == false) {
					if (GUILayout.Button (" [Return]")) {
					
						netscapeOpen = false;
						showNode = false;
						grayNode = true;
					
					}
					if (showProgramList == false) {
						if (GUILayout.Button (" [Show List]")) {
							showProgramList = true;
							selectedItem = -1;
						}
					} else if (showProgramList == true) {
						if (GUILayout.Button (" [Hide List]")) {
							showProgramList = false;
							selectedItem = -1;
						}
					
					}
					
				}

				GUILayout.EndHorizontal ();
				GUILayout.EndArea ();


				GUILayout.BeginArea (netscapeBoxSize);
				if (showNode == false) {
					netscapeBoxSize = new Rect (Screen.width, Screen.height, 0, 0);
				} else {
					cameraFixed (selectedNode);
					netscapeBoxSize = new Rect (Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.height / 2);
				}

				//VERTICAL -------------------------------------------------------------



				if (showNode == true) {
					GUILayout.BeginVertical ("Node"+nodeTitle, GUI.skin.GetStyle ("netscape"));
					GUI.skin.label.fontSize = Mathf.RoundToInt (30 * Screen.width / (OverworldManager.instance.defaultWidth * 1.0f));
					GUI.skin.label.fontSize = Mathf.RoundToInt (30 * Screen.width / (OverworldManager.instance.defaultWidth * 1.0f));

					GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (OverworldManager.instance.defaultWidth * 1.0f));
					GUI.skin.label.fontSize = Mathf.RoundToInt (30 * Screen.width / (OverworldManager.instance.defaultWidth * 1.0f));
					GUILayout.Label (nodeTitle + "\n");
					GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (OverworldManager.instance.defaultWidth * 1.0f));

					scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUI.skin.GetStyle ("empty"), GUILayout.Width (Screen.width / 2.05f), GUILayout.Height (Screen.height / 3.5f));
					GUILayout.Label (nodeDescription);
					GUILayout.EndScrollView ();

					GUILayout.BeginHorizontal ("", GUI.skin.GetStyle ("empty"));
					if (GUILayout.Button ("[Connect to Node]")) {
						nodeUnHovered ();
						showNode = false;

						loadBattle (nodeNumber);
						//Application.LoadLevel(nodeNumber);
					}
					if (GUILayout.Button ("  [Close]")) {
						nodeUnHovered ();
						showNode = false;
						grayNode = true;
					}
					GUILayout.EndHorizontal ();
					GUILayout.EndVertical ();

				}

				//HORIZONTAL --------------------------------------------------
				
				GUILayout.EndArea ();

				if (showMaxShop == true) {
					GUI.DrawTexture (new Rect (Screen.width / 7, Screen.height / 2.5f, Screen.width / 10, Screen.height / 6), spriteMax);
					GUILayout.BeginArea (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2));
					GUILayout.BeginHorizontal ("", GUI.skin.GetStyle ("netscape"));
					GUI.skin.label.fontSize = Mathf.RoundToInt (32 * Screen.width / (OverworldManager.instance.defaultWidth * 1.0f));
					//GUILayout.Label("\n     "+nodeTitle+"\n");

					GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (OverworldManager.instance.defaultWidth * 1.0f));

					//GUILayout.Label(nodeDescription);


					GUILayout.BeginVertical ("", GUI.skin.GetStyle ("empty"));
					for (int d = 0; d < shopMaxList.Count; d++) {
						/*if (shopMaxList[d] == "GOD"){
							if (GUILayout.Button("GOD")){
								selectedItem = d;
							}
						}
						if (shopMaxList[d] == "Bug"){
							if (GUILayout.Button("Bug")){
								selectedItem = d;
							}
						}
						if (shopMaxList[d] == "Golem"){
							if (GUILayout.Button("Golem")){
								selectedItem = d;
							}
						}
						if (shopMaxList[d] == "Breaker"){
							if (GUILayout.Button("Breaker")){
								selectedItem = d;
							}
						}*/
					}
					
					GUILayout.EndVertical ();
					GUILayout.BeginVertical ("", GUI.skin.GetStyle ("empty"));
					if (selectedItem >= 0) {
						/*if (shopMaxList[selectedItem] == "Bug"){
						GUI.DrawTexture(new Rect (Screen.width*0.7f,0,Screen.width/20,Screen.height/10), spriteBug);
						GUILayout.Label("Bug" +
							"Damage: 2\n" +
							"Moves:  5\n" +
							"Max SS: 1");
					}*/
						/*if (shopMaxList[selectedItem] == "Golem"){
							GUI.DrawTexture(new Rect (Screen.width*0.7f,0,Screen.width/20,Screen.height/10), spriteGolem);
							GUILayout.Label("Golem" +
							                "Damage: 3\n" +
							                "Moves:  1\n" +
							                "Max SS: 5");
					}
					if (shopMaxList[selectedItem] == "Breaker"){
							GUI.DrawTexture(new Rect (Screen.width*0.7f,0,Screen.width/20,Screen.height/10), spriteBreaker);
							GUILayout.Label("Bug" +
							                "Damage: 3\n" +
							                "Moves:  2\n" +
							                "Max SS: 3");
					}
					if (shopMaxList[selectedItem] == "GOD"){
							GUI.DrawTexture(new Rect (Screen.width*0.7f,0,Screen.width/20,Screen.height/10), spriteGOD);
							GUILayout.Label("Bug" +
							                "Damage: 8\n" +
							                "Moves:  8\n" +
							                "Max SS: 8");
					}*/
					}
					GUILayout.EndVertical ();
					
					GUILayout.EndHorizontal ();

					GUILayout.BeginHorizontal ("", GUI.skin.GetStyle ("empty"));
					if (selectedItem >= 0)
					if (GUILayout.Button ("[Buy Program]")) {
						//programList.Add(shopMaxList[selectedItem]);
						//shopMaxList.Remove(shopMaxList[selectedItem]);
						selectedItem = -1;
					}
					if (GUILayout.Button ("[Exit node]")) {
						selectedItem = -1;
						showMaxShop = false;
						nodeUnHovered ();
						grayNode = true;
					}
					GUILayout.EndHorizontal ();
					GUILayout.EndArea ();
				}

				//VERTICAL -------------------------------------------------------------
				//HORIZONTAL --------------------------------------------------

				/*GUILayout.BeginHorizontal ("", GUI.skin.GetStyle ("netscape"));

				if (showProgramList == false && programList.Count > 0){
					if (GUILayout.Button ("Programs")) {
						showProgramList = true;
						showNode = false;
						grayNode = true;
					}
				} else {
					for (int p = 0; p < programList.Count; p++){
						if (GUILayout.Button(programList[p])){
							programDescription = programText[p];
						}
					}
				}
				
				GUILayout.EndHorizontal ();*/


				//AREA -----------------------------------------------------------------------------------------------------------

				/*if (showProgramList == true){
					GUILayout.BeginArea (new Rect (Screen.width/2.5f, Screen.height/8, Screen.width/2, Screen.height/1.2f));
					GUILayout.BeginVertical ("", GUI.skin.GetStyle ("comp"));
					scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width/2 - 24), GUILayout.Height (Screen.height/1.5f));
					GUILayout.Label(programDescription);
					GUILayout.EndScrollView ();
					GUILayout.EndVertical ();
					GUILayout.EndArea ();
				}*/
				
			}
			
		}
	
		/*if (mapOpen == true) {
			GUI.DrawTexture(new Rect(Screen.width/4, Screen.height/16, Screen.width/2, Screen.height/1.2f), map);
		}*/

		//TEXT BOX----------------------------------------------------------------------------------------------------------------------------ZZ

		if (textBoxShown == true && menuStarted == false) {

			if (matchStarted == true){
				textBoxPosition = new Rect (Screen.width / 5, Screen.height - Screen.height / 5, Screen.width - Screen.width / 5, Screen.height / 5);
				textBoxStyle = "netscape";
			} else {
				textBoxPosition = new Rect (Screen.width / 6, Screen.height / 1.5f, Screen.width / 1.5f, Screen.height / 5);
				textBoxStyle = "window";
			}

			GUILayout.BeginArea (textBoxPosition);
			GUILayout.BeginVertical ("", GUI.skin.GetStyle (textBoxStyle));

			if (textFlash <= 1 && dialogReady == true) {
				GUILayout.Label (textPrint + "_");
			} else {
				GUILayout.Label (textPrint + " ");
			}

			GUILayout.EndVertical ();
			GUILayout.EndArea ();

		}

		if (responseWanted == true && menuStarted == false) {
			if (response1.Length > 0){
				responseBoxSize = 0.06f;
			}
			if (response2.Length > 0){
				responseBoxSize = 0.12f;
			}
			if (response3.Length > 0){
				responseBoxSize = 0.18f;
			}
			if (response4.Length > 0){
				responseBoxSize = 0.24f;
			}
			if (matchStarted == true){
				textBoxPosition = new Rect (Screen.width / 5, Screen.height/2, Screen.width / 2, Screen.height*responseBoxSize);
				textBoxStyle = "netscape";
			} else {
				textBoxPosition = new Rect (Screen.width / 3.5f, Screen.height / 2, Screen.width / 2, Screen.height / 8);
				textBoxStyle = "window";
			}

			GUILayout.BeginArea (textBoxPosition);
			GUILayout.BeginVertical ("", GUI.skin.GetStyle (textBoxStyle));

			//Dialog buttons
			if (responseWanted == true) {
				response1picked = false;
				response2picked = false;
				response3picked = false;
				response4picked = false;
				if (response1.Length > 0){
				if (GUILayout.Button (response1)) {
					sceneDialog (1);
				}
				}
				if (response2.Length > 0){
				if (GUILayout.Button (response2)) {
					sceneDialog (2);
				}
				}
				if (response3.Length > 0){
				if (GUILayout.Button (response3)) {
					sceneDialog (3);
				}
				}
				if (response4.Length > 0){
				if (GUILayout.Button (response4)) {
					sceneDialog (4);
				}
				}
			} 
			
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}


		if (menuStarted == true) {

			//Log on interface, all text displayed here
			GUILayout.BeginArea (new Rect (0, 0, Screen.width / 3, Screen.height));
			GUI.skin.label.fontSize = Mathf.RoundToInt (12 * Screen.width / (defaultWidth * 1.0f));
			//GUILayout.BeginVertical ("コンピュータログ", GUI.skin.GetStyle ("window"));
			GUILayout.BeginVertical ("OS_Boot", GUI.skin.GetStyle ("comp"));
			scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width / 3 - 27), GUILayout.Height (Screen.height - 60));


			if (textFlash <= 1) {
				GUILayout.Label (logText + "_");
			} else {
				GUILayout.Label (logText + " ");
			}

			GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (defaultWidth * 1.0f));
			GUILayout.EndScrollView ();
			GUILayout.EndVertical ();
			GUILayout.EndArea ();

			//Graphical Interface
			/*GUILayout.BeginArea (new Rect (Screen.width / 3 + 20, 10, Screen.width - 30 - Screen.width / 3, Screen.height * 0.75f));
			GUILayout.BeginVertical ("----ビデオゲームについての情報----", GUI.skin.GetStyle ("GI"));
			if (menuStarted == true) {
				GUILayout.Label("ICEBREAKER");
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();*/

			//Buttons on interface
			GUILayout.BeginArea (new Rect (Screen.width / 2.8f, Screen.height /6f, Screen.width/2, Screen.height/2));
			//windowRect = GUI.Window(0, windowRect, DoMyWindow, "Main_Menu メニュー", GUI.skin.GetStyle ("comp"));
			GUILayout.BeginVertical ("Main_Menu メニュー", GUI.skin.GetStyle ("comp"));

			//MENY, START LOAD OPTIONS EXIT
			if (menuStarted == true) {
				if (scene > 0){
					if (GUILayout.Button (">Go to main menu")) {
						newGameOpen = true;
						//Application.LoadLevel (2);
						//matchStarted = true;
					}
				} else {
				if (GUILayout.Button (">New Game")) {
					menuStarted = false;
					newGameOpen = true;
					//Application.LoadLevel (2);
					//matchStarted = true;
				}
				}
				if (scene > 0 && matchStarted == false){
					if (GUILayout.Button (">Save gameState...")) {
						//Load save files
						if (saveFilesOpen == true){
							saveFilesOpen = false;
						} else {
							saveFilesOpen = true;
							loadFilesOpen = false;
						}
						/*textBoxShown = true;
					scene = 1;
					dialogController = 143;
					menuStarted = false;*/
					}
				}
				if (GUILayout.Button (">Open Save...")) {
					//Load save files
					if (loadFilesOpen == true){
						loadFilesOpen = false;
					} else {
						saveFilesOpen = false;
						loadFilesOpen = true;
					}
					/*textBoxShown = true;
					scene = 1;
					dialogController = 143;
					menuStarted = false;*/
				}
				if (GUILayout.Button (">Player Settings...")) {
					//Options
					if (settingsOpen == true){
						settingsOpen = false;
					} else {
						settingsOpen = true;
					}

				}
				if (GUILayout.Button (">Quit Application")) {
					Application.Quit ();
				}
			}


			GUILayout.EndVertical ();
			GUILayout.EndArea ();

			if (loadFilesOpen == true){
				GUILayout.BeginArea (new Rect (Screen.width / 2.95f, Screen.height /2.5f, Screen.width/3.1f, Screen.height/1.8f));
				GUILayout.BeginVertical ("File Browser", GUI.skin.GetStyle ("comp"));
				GUILayout.Label("Select file to load...");
				//For loop showing all save files from list
				for (int i = 0; i < 8; i++) {
					if (SaveGameSystem.LoadGame("Save"+i) == null){
						GUILayout.Label("Empty slot");
					} else {
						if (GUILayout.Button("Load Save "+i/* + saveList[i].name*/)){
							//SAVE FILE
							i = 0;
							text = "";
							textPrint = "";
							MySaveGame SaveFile = SaveGameSystem.LoadGame("Save"+i) as MySaveGame;
							userName = SaveFile.userName;
							money = SaveFile.money;
							day = SaveFile.day;
							date = SaveFile.date;
							mailList = SaveFile.mailList;
							netscapeNodes = SaveFile.netscapeNodes;
							placesToGo = SaveFile.placesToGo;
							programList = SaveFile.programList;
							choices = SaveFile.storyChoices;
							coding = SaveFile.coding;
							scene = SaveFile.scene;
							freeMode = SaveFile.freeMode;
							dialogController = SaveFile.dialogController;
							textBoxShown = SaveFile.textBoxShown;
							deleteOldText = SaveFile.deleteOldText;
							fadeToWhite = SaveFile.fadeToWhite;
							fadeToBlack = SaveFile.fadeToBlack;
							
							netscapeOpen = SaveFile.netscapeOpen;
							inboxOpen = SaveFile.inboxOpen;
							mapOpen = SaveFile.mapOpen;
							
							responseWanted = SaveFile.responseWanted;
							response1picked = SaveFile.response1picked;
							response2picked = SaveFile.response2picked;
							response3picked = SaveFile.response3picked;
							response4picked = SaveFile.response4picked;


							fadeOutMusic = true;
							menuStarted = false;
						}
					}
				}
				if (GUILayout.Button("Close Window")){
					loadFilesOpen = false;
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (saveFilesOpen == true){
				GUILayout.BeginArea (new Rect (Screen.width / 2.95f, Screen.height /2.5f, Screen.width/3.1f, Screen.height/1.8f));
				GUILayout.BeginVertical ("File Browser", GUI.skin.GetStyle ("comp"));
				GUILayout.Label("Select save slot...");
				//For loop showing all save files from list
				for (int i = 0; i < 8; i++) {
					if (SaveGameSystem.LoadGame("Save"+i) == null){
						if (GUILayout.Button("Empty slot")){

							// Saving a saved game.
							MySaveGame SaveFile = new MySaveGame();
							SaveFile.userName = userName;
							SaveFile.money = money;
							SaveFile.day = day;
							SaveFile.date = date;
							SaveFile.mailList = mailList;
							SaveFile.netscapeNodes = netscapeNodes;
							SaveFile.placesToGo = placesToGo;
							SaveFile.programList = programList;
							SaveFile.storyChoices = choices;
							SaveFile.coding = coding;
							SaveFile.scene = scene;
							SaveFile.freeMode = freeMode;
							SaveFile.dialogController = dialogController;
							SaveFile.textBoxShown = textBoxShown;
							SaveFile.deleteOldText = deleteOldText;
							SaveFile.fadeToWhite = fadeToWhite;
							SaveFile.fadeToBlack = fadeToBlack;
							
							SaveFile.netscapeOpen = netscapeOpen;
							SaveFile.inboxOpen = inboxOpen;
							SaveFile.mapOpen = mapOpen;
							
							SaveFile.responseWanted = responseWanted;
							SaveFile.response1picked = response1picked;
							SaveFile.response2picked = response2picked;
							SaveFile.response3picked = response3picked;
							SaveFile.response4picked = response4picked;
							SaveGameSystem.SaveGame(SaveFile, "Save"+i); // Saves as MySaveGame.sav
							
						}
					} else {
						if (GUILayout.Button("Save Game "+i/* + saveList[i].name*/)){
							saveEraseOpen = true;
							tempInt = i;
						}
					}
				}
				if (GUILayout.Button("Close Window")){
					saveFilesOpen = false;
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (settingsOpen == true){
				GUILayout.BeginArea (new Rect (Screen.width * 0.666f, Screen.height /3f, Screen.width/3.1f, Screen.height/1.8f));
				GUILayout.BeginVertical ("Settings...", GUI.skin.GetStyle ("comp"));
				//Settings for grafixs, sound, etc.
				if (GUILayout.Button("Close Window")){
					settingsOpen = false;
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (saveEraseOpen == true){
				GUILayout.BeginArea (new Rect (Screen.width * 0.666f, Screen.height /3f, Screen.width/3.1f, Screen.height/1.8f));
				GUILayout.BeginVertical ("Erase File", GUI.skin.GetStyle ("comp"));
				//Settings for grafixs, sound, etc.
				GUILayout.Label("Are you sure you want to overwrite this file? I'm sure you worked hard on it...");
				if (GUILayout.Button("Yes")){
					saveEraseOpen = false;
					SaveGameSystem.DeleteSaveGame("Save"+tempInt);
					// Saving a saved game.
					MySaveGame SaveFile = new MySaveGame();
					SaveFile.userName = userName;
					SaveFile.money = money;
					SaveFile.day = day;
					SaveFile.date = date;
					SaveFile.mailList = mailList;
					SaveFile.netscapeNodes = netscapeNodes;
					SaveFile.placesToGo = placesToGo;
					SaveFile.programList = programList;
					SaveFile.storyChoices = choices;
					SaveFile.coding = coding;
					SaveFile.scene = scene;
					SaveFile.freeMode = freeMode;
					SaveFile.dialogController = dialogController;
					SaveFile.textBoxShown = textBoxShown;
					SaveFile.deleteOldText = deleteOldText;
					SaveFile.fadeToWhite = fadeToWhite;
					SaveFile.fadeToBlack = fadeToBlack;
					
					SaveFile.netscapeOpen = netscapeOpen;
					SaveFile.inboxOpen = inboxOpen;
					SaveFile.mapOpen = mapOpen;
					
					SaveFile.responseWanted = responseWanted;
					SaveFile.response1picked = response1picked;
					SaveFile.response2picked = response2picked;
					SaveFile.response3picked = response3picked;
					SaveFile.response4picked = response4picked;
					SaveGameSystem.SaveGame(SaveFile, "Save"+tempInt); // Saves as MySaveGame.sav
				}
				if (GUILayout.Button("No")){
					saveEraseOpen = false;
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}


		}
		if (newGameOpen == true) {

			GUILayout.BeginArea (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2));
			GUILayout.BeginVertical ("New Game", GUI.skin.GetStyle ("comp"));
			//Settings for grafixs, sound, etc.
			if (scene > 0) {
				GUILayout.Label("Are you sure you want to quit? Any unsaved progress will be eaten by cyberspace demons.");
				if (GUILayout.Button (">Yes")) {
					newGameOpen = false;
					scene = 0;
					inboxOpen = false;
					mapOpen = false;
					netscapeOpen = false;
					//Application.LoadLevel (2);
					//matchStarted = true;
				}
				if (GUILayout.Button (">No")) {
					newGameOpen = false;
					//Application.LoadLevel (2);
					//matchStarted = true;
				}
			} else {
				if (newGameOpen2 == false) {
					GUILayout.Label ("Do you want to start a new game?");
					if (GUILayout.Button ("Yes")) {
						newGameOpen2 = true;
					}
					if (GUILayout.Button ("No")) {
						newGameOpen = false;
						menuStarted = true;
					}
				} else {
					GUILayout.Label ("Enter Username");
					userName = GUI.TextField (new Rect (Screen.width / 100, Screen.height / 10, Screen.width / 10, Screen.height / 9), userName, 15, GUI.skin.GetStyle ("textField"));
					GUILayout.Label ("");
					GUILayout.Label ("");
					//GUILayout.Label("Enter Password");
					/*password = GUI.TextField(new Rect(Screen.width/100, Screen.height/10, Screen.width/10, Screen.height/9), password, 15, GUI.skin.GetStyle ("textField"));
				password = "*";*/

				
					GUILayout.Label ("");
					GUILayout.Label ("");

					if (userName.Length > 0) {

						if (GUILayout.Button ("Done")) {
							settingsOpen = false;
							saveEraseOpen = false;
							saveFilesOpen = false;
							loadFilesOpen = false;
							newGameOpen = false;
							newGameOpen2 = false;
							fadeOutMusic = true;
							scene = 1;
							dialogController = -1;
							menuStarted = false;
							textBoxShown = true;

						}
					}
				}
			}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
		
	}
	void DoMyWindow(int windowID) {
		GUI.DragWindow(new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2));
	}
	
	void loadBattle(int level){
		
		if (level == 9) {//Training Grounds
			matchStarted = true;
			Application.LoadLevel (2);//Tutorial Battle with Aya
		}
		if (level == 10) {//Training Grounds
			matchStarted = true;
			Application.LoadLevel (3);//Tutorial Battle with Aya
		}
		else if (level == 420) {//Training Grounds
			showMaxShop = true;
		} 
		else {
			grayNode = true;
		}

	}
}
