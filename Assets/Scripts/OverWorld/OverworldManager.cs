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
	public Texture scene1Background;
	public Texture scene1Background1;
	public Texture scene1Background2;
	public Texture scene2Background;
	public Texture scene3Background;
	public Texture scene4Background;
	public Texture scene5Background;
	public Texture scene6Background;

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

	public bool deleteOldText = true;

	public bool once = true;

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
	public bool grayNode = false;
	public bool nodeHover = false;
	public bool tempBool = false;

	public Vector3 selectedNode;
	public Vector3 cameraPosition;

	public bool showProgramList = false;

	public List<string> messages = new List<string>();
	public List<string> messagesTitle = new List<string>();
	public List<string> sites = new List<string> ();
	public List<string> placesToGo = new List<string> ();
	public List<string> programList = new List<string> ();
	public List<string> programText = new List<string> ();

	public Rect GIWindow;
	public float guiAlpha;
	public Color tempColor;
	public Rect netscapeBoxSize;

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

	public int dialogController = 0;
	public int dialogControllerMenu = 0;
	public float textSpeed = 0.02f;

	public int defaultWidth;

	public bool responseWanted = false;

	public string response1;
	public string response2;
	public string response3;
	public string response4;

	public int unreadMessages = 0;
	public bool netscapeOpen = false;
	public bool inboxOpen = false;
	public bool mapOpen = false;

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
		scene1Background = Resources.Load ("Textures/arrow") as Texture;
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
				text = "";
				textPrint = "";
				textBoxShown = false;
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
				fadeToWhite = false;
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
		if (scene == 1 && guiAlpha >= 1) {
			x++;
			if (x >= 2500) {
				x = 0;
			}
			if (x == 5) {
				scene1Background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 12) {
				scene1Background = scene1Background1;
			}
			if (x == 120) {
				scene1Background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 128) {
				scene1Background = scene1Background1;
			}
			if (x == 350) {
				scene1Background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 355) {
				scene1Background = scene1Background1;
			}
			if (x == 358) {
				scene1Background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 365) {
				scene1Background = scene1Background1;
			}
			if (x == 1030) {
				scene1Background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 1045) {
				scene1Background = scene1Background1;
			}
			if (x == 2069) {
				scene1Background = scene1Background2;
				GetComponent<AudioSource>().PlayOneShot(flickeringLight);
			}
			if (x == 2079) {
				scene1Background = scene1Background1;
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
			//GUI.DrawTexture(new Rect(Screen.width-500, 0, Screen.width/4, Screen.height/4), scene2Background);
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

		//COMPUTER SCREEN -----------------------------------------------------------------------------------------------------------------------------------ZZ

		if (scene == 2 && menuStarted == false && freeMode == true) {

			computerText = day.ToString()+"日目 - Day "+day.ToString()+"\n"+date+"\n¥"+money;

			if (netscapeOpen == false){
			GUILayout.BeginArea (new Rect (Screen.width*0.05f, Screen.height*0.05f, Screen.width-Screen.width*0.1f, Screen.height-Screen.height*0.1f));

			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("comp"));
			//Day Shower
			if (textFlash <= 1 && dialogReady == true) {
				GUILayout.Label (computerText + "_");
			} else {
				GUILayout.Label (computerText + " ");
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
			}
			if (netscapeOpen == false && inboxOpen == false && mapOpen == false){
				GUILayout.BeginArea (new Rect (Screen.width*0.07f, Screen.height/3, Screen.width/6, Screen.height/4));
				GUILayout.BeginVertical ("", GUI.skin.GetStyle ("comp"));

				if (GUILayout.Button ("Enter Netscape")) {
					netscapeOpen = true;
				}
				if (GUILayout.Button ("Inbox ("+unreadMessages+")")) {
					unreadMessages = 0;
					inboxOpen = true;
				}
				if (GUILayout.Button ("Log Off")) {
					if (placesToGo.Count <= 0){
						i = 0;
						textBoxShown = true;
						text = "You're kidding, right?";
					}else {
						mapOpen = true;
					}
				}

				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (mapOpen == true){
				GUILayout.BeginArea (new Rect (Screen.width*0.07f, Screen.height/3, Screen.width/6, Screen.height/4));
				GUILayout.BeginVertical ("", GUI.skin.GetStyle ("comp"));
				for (int d = 0; d < placesToGo.Count; d++){
					if (GUILayout.Button(placesToGo[d])){

					}
				}
				if (GUILayout.Button ("Return")) {
					mapOpen = false;
				}

				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (inboxOpen == true){
				GUILayout.BeginArea (new Rect (Screen.width*0.07f, Screen.height/3, Screen.width/3, Screen.height/2));
				GUILayout.BeginVertical ("", GUI.skin.GetStyle ("comp"));
				//scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (Screen.width/2), GUILayout.Height (Screen.height/6));
				for (int m = messages.Count-1; m >= 0; m--){
					if (GUILayout.Button(messagesTitle[m])){
						messageText = messages[m];
					}
				}
				if (GUILayout.Button ("Return")) {
					inboxOpen = false;
					messageText = "";
				}
				//GUILayout.EndScrollView ();
				GUILayout.EndVertical ();
				GUILayout.EndArea ();

				GUILayout.BeginArea (new Rect (Screen.width/2.3f, Screen.height/6, Screen.width/2, Screen.height/1.5f));

				GUILayout.BeginVertical ("", GUI.skin.GetStyle ("comp"));
				scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUI.skin.GetStyle("empty"), GUILayout.Width (Screen.width/2 - 24), GUILayout.Height (Screen.height/1.65f));
				GUILayout.Label(messageText);
				GUILayout.EndScrollView ();
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (netscapeOpen == true){

				//AREA -----------------------------------------------------------------------------------------------------------
				GUILayout.BeginArea (new Rect (0,0,Screen.width/5,Screen.height/12));
				GUILayout.BeginHorizontal("", GUI.skin.GetStyle("empty"));

				if (GUILayout.Button ("[Return]")) {
					if (showProgramList == false){
						netscapeOpen = false;
						showNode = false;
						grayNode = true;
					}
					else {
						showProgramList = false;
					}
				}
				if (showProgramList == false){
					if (GUILayout.Button ("  [Programs]")) {
						showProgramList = true;
					}
				}

				GUILayout.EndHorizontal();
				GUILayout.EndArea();


				GUILayout.BeginArea (netscapeBoxSize);
				if (showNode == false){
					netscapeBoxSize = new Rect (Screen.width, Screen.height, 0, 0);
				} else {
					cameraFixed(selectedNode);
					netscapeBoxSize = new Rect (Screen.width/4, Screen.height/6, Screen.width/2, Screen.height/2);
				}

				//VERTICAL -------------------------------------------------------------

				GUILayout.BeginVertical ("", GUI.skin.GetStyle ("netscape"));
				GUI.skin.label.fontSize = Mathf.RoundToInt (30 * Screen.width / (OverworldManager.instance.defaultWidth* 1.0f));

				GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (OverworldManager.instance.defaultWidth* 1.0f));

				if (showNode == true){

					GUI.skin.label.fontSize = Mathf.RoundToInt (30 * Screen.width / (OverworldManager.instance.defaultWidth* 1.0f));
					GUILayout.Label(nodeTitle+"\n");
					GUI.skin.label.fontSize = Mathf.RoundToInt (18 * Screen.width / (OverworldManager.instance.defaultWidth* 1.0f));

					scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUI.skin.GetStyle("empty"), GUILayout.Width (Screen.width/2.05f), GUILayout.Height (Screen.height/3.5f));
					GUILayout.Label(nodeDescription);
					GUILayout.EndScrollView ();

					GUILayout.BeginHorizontal("", GUI.skin.GetStyle("empty"));
					if (GUILayout.Button("[Connect to Node]")){
						nodeUnHovered ();
						showNode = false;
						grayNode = true;
						loadBattle(nodeNumber);
						//Application.LoadLevel(nodeNumber);
					}
					if (GUILayout.Button("  [Close]")){
						nodeUnHovered ();
						showNode = false;
						grayNode = true;
					}
					GUILayout.EndHorizontal();


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
				GUILayout.EndVertical ();
				//HORIZONTAL --------------------------------------------------

				GUILayout.EndArea ();

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

		if (responseWanted == true && menuStarted == false) {

			GUILayout.BeginArea (new Rect (Screen.width / 3, Screen.height / 2, Screen.width / 2, Screen.height / 8));
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("window"));

			//Dialog buttons
			if (responseWanted == true) {
				response1picked = false;
				response2picked = false;
				response3picked = false;
				response4picked = false;
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
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("comp"));
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
			GUILayout.BeginVertical ("", GUI.skin.GetStyle ("comp"));

			//MENY, START LOAD OPTIONS EXIT
			if (menuStarted == true) {
				if (GUILayout.Button (">Application.LoadLevel(1);")) {
					fadeOutMusic = true;
					scene = 1;
					dialogController = -1;
					menuStarted = false;
					textBoxShown = true;
					//Application.LoadLevel (2);
					//matchStarted = true;
				}
				if (GUILayout.Button (">File.Open(\"saveFile\");[WIP]")) {
					//Load save files
					textBoxShown = true;
					scene = 1;
					dialogController = 143;
					menuStarted = false;
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
	void loadBattle(int level){
		if (level == 9) {//Training Grounds
			matchStarted = true;
			Application.LoadLevel (1);//Tutorial Battle with Aya
		}
	}
}
